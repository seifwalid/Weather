﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Weather.Data;
using Weather.Dtos.User;
using Weather.Interfaces;
using Weather.Mappers;
using Weather.Model;

namespace Weather.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepo;
        private readonly ILimitRepository _limitRepo;
        private readonly IAirQualityRepository _airQualityRepository;
        public UserController(DataContext context, ILimitRepository limitRepo,IUserRepository userRepo,IAirQualityRepository airQualityRepository)
        {
            _context = context;
            _limitRepo = limitRepo;
            _userRepo = userRepo;
            _airQualityRepository = airQualityRepository;

        }

        //View details of a user 
        [HttpGet("{uid}")]
        public async Task<IActionResult> GetById([FromRoute] int uid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.GetUserByIdAsync(uid);

            if(user==null)
            {
                return NotFound();
            }
            
            return Ok(user.ToUserDto());
        }

        //Get all limits of a user
        [HttpGet("{uid}/limits")]
        public async Task<IActionResult> GetLimitsByUserId([FromRoute] int uid)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var limits=await _userRepo.GetLimitsByUserId(uid);
            if (limits == null)
            {
                return NotFound();

            }
            return Ok(limits.ToLimitDto());


        }

        //Register user 
        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userModel = userDto.ToUserFromCreateUserDTO();
            await _userRepo.CreateUserAsync(userModel);
            return Ok(userModel);
        }

        //Register user with google
        //Here user id needs to be string not auto generated 
        //[HttpPost]
        //public IActionResult CreateWithGoogle([FromBody] CreateUserWithGoogle googleUserDto)
        //{
        //    var userModel = googleUserDto.ToUserFromGoogleDto();
        //    _context.Users.Add(userModel);
        //    _context.SaveChanges();
        //    return CreatedAtAction(nameof(GetById), new { id = userModel.UserId },userModel.ToUserDto());
        //}

        //Login
        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepo.GetUserByNameandPassAsync(loginDto.UserName,loginDto.Password);
            if(user== null)
            {
                return NotFound("No user exists with these credentials!");
            }

            return Ok(user.ToUserDto());
        }


        //update user personal details
        [HttpPut]
        [Route("{uid}")]
        public async Task<IActionResult> Update([FromRoute] int uid, [FromBody] UpdateUserDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = await _userRepo.UpdateUserAsync(uid, updateDto.ToUserFromUpdateDTO());
            if (existingUser == null)
            {
                return NotFound("No user found!");
            }
            
            return Ok(existingUser.ToUserDto());
        }

        [HttpGet("CheckAirQuality/{userId}")]
        public async Task<IActionResult> CheckAirQuality(int userId)
        {
            var user = await _context.Users.Include(u => u.UserPermissibleLimits).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            var airQualityData = await _airQualityRepository.GetAirQualityDataAsync();
            Console.WriteLine(JsonConvert.SerializeObject(airQualityData));

            if (airQualityData == null)
            {
                // Handle the case where air quality data couldn't be retrieved
                return StatusCode(500, "Unable to retrieve air quality data.");
            }
            // Compare and create alerts
            CheckAndCreateAlert(user, "PM10", airQualityData.pm10, user.UserPermissibleLimits.MaxPM10);
            CheckAndCreateAlert(user, "PM2_5", airQualityData.pm2_5, user.UserPermissibleLimits.MaxPM2_5);
            CheckAndCreateAlert(user, "Carbon Monoxide", airQualityData.carbon_monoxide, user.UserPermissibleLimits.MaxCarbonMonoxide);
            CheckAndCreateAlert(user, "Nitrogen Dioxide", airQualityData.nitrogen_dioxide, user.UserPermissibleLimits.MaxNitrogenDioxide);
            CheckAndCreateAlert(user, "Sulphur Dioxide", airQualityData.sulphur_dioxide, user.UserPermissibleLimits.MaxSulphurDioxide);
            CheckAndCreateAlert(user, "Ozone", airQualityData.ozone, user.UserPermissibleLimits.MaxOzone);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private void CheckAndCreateAlert(Userr user, string parameterName, double currentValue, double permissibleValue)
        {
            if (currentValue > permissibleValue)
            {
                var alert = new Alert
                {
                    Country = "Egypt", // Replace with actual country if needed
                    UserID = user.UserId,
                    Timestamp = DateTime.UtcNow,
                    ParameterLimit = permissibleValue,
                    CurrentParameterVal = currentValue,
                    Difference = currentValue - permissibleValue
                };
                _context.Alerts.Add(alert);
            }
        }







    }
}
