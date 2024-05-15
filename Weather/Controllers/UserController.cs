using Microsoft.AspNetCore.Identity;
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
        private readonly IWeatherRepository _weatherRepository;
        public UserController(DataContext context, ILimitRepository limitRepo,IUserRepository userRepo,IAirQualityRepository airQualityRepository,IWeatherRepository weatherRepository)
        {
            _context = context;
            _limitRepo = limitRepo;
            _userRepo = userRepo;
            _airQualityRepository = airQualityRepository;
            _weatherRepository = weatherRepository;

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

            var currentAirQuality = await _airQualityRepository.GetAirQualityDataAsync();
            if (currentAirQuality == null)
            {
                return StatusCode(500, "Unable to retrieve air quality data.");
            }

            var airQualityResponse = new AirQualityResponse
            {
                Latitude = 52.52,
                Longitude = 13.41,
                Timestamp = DateTime.UtcNow,
                Ozone = CreateAirQualityElement(currentAirQuality.Ozone, user.UserPermissibleLimits.MaxOzone),
                Pm10 = CreateAirQualityElement(currentAirQuality.Pm10, user.UserPermissibleLimits.MaxPM10),
                Pm2_5 = CreateAirQualityElement(currentAirQuality.Pm2_5, user.UserPermissibleLimits.MaxPM2_5),
                CarbonMonoxide = CreateAirQualityElement(currentAirQuality.CarbonMonoxide, user.UserPermissibleLimits.MaxCarbonMonoxide),
                NitrogenDioxide = CreateAirQualityElement(currentAirQuality.NitrogenDioxide, user.UserPermissibleLimits.MaxNitrogenDioxide),
                SulphurDioxide = CreateAirQualityElement(currentAirQuality.SulphurDioxide, user.UserPermissibleLimits.MaxSulphurDioxide)
            };

            await CheckAndCreateAlert(user, "Ozone", currentAirQuality.Ozone, user.UserPermissibleLimits.MaxOzone);
            await CheckAndCreateAlert(user, "PM10", currentAirQuality.Pm10, user.UserPermissibleLimits.MaxPM10);
            await CheckAndCreateAlert(user, "PM2_5", currentAirQuality.Pm2_5, user.UserPermissibleLimits.MaxPM2_5);
            await CheckAndCreateAlert(user, "Carbon Monoxide", currentAirQuality.CarbonMonoxide, user.UserPermissibleLimits.MaxCarbonMonoxide);
            await CheckAndCreateAlert(user, "Nitrogen Dioxide", currentAirQuality.NitrogenDioxide, user.UserPermissibleLimits.MaxNitrogenDioxide);
            await CheckAndCreateAlert(user, "Sulphur Dioxide", currentAirQuality.SulphurDioxide, user.UserPermissibleLimits.MaxSulphurDioxide);

            await _context.SaveChangesAsync();
            return Ok(airQualityResponse);
        }

        private AirQualityElement CreateAirQualityElement(double currentValue, double limit)
        {
            return new AirQualityElement
            {
                Current = currentValue,
                Limit = limit,
                Difference = currentValue - limit
            };
        }



        private async Task CheckAndCreateAlert(Userr user, string parameterName, double currentValue, double permissibleValue)
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

                await _airQualityRepository.CreateAlertAsync(alert);
            }
        }


        [HttpGet("CheckWeather/{userId}")]
        public async Task<IActionResult> CheckWeather(int userId)
        {
            var user = await _context.Users.Include(u => u.UserPermissibleLimits).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            var weatherData = await _weatherRepository.GetWeatherDataAsync();
            if (weatherData == null)
            {
                return StatusCode(500, "Unable to retrieve weather data.");
            }

            var weatherResponse = new WeatherResponse
            {
                Latitude = 52.52,
                Longitude = 13.41,
                Timestamp = DateTime.UtcNow,
                Temperature = CreateWeatherElement(weatherData.Temp, user.UserPermissibleLimits.MaxTemperature),
                Humidity = CreateWeatherElement(weatherData.Humidity, user.UserPermissibleLimits.MaxHumidity)
            };

            // Save any alerts created
            await _context.SaveChangesAsync();

            return Ok(weatherResponse);
        }


        private AirQualityElement CreateWeatherElement(double currentValue, double limit)
        {
            return new AirQualityElement
            {
                Current = currentValue,
                Limit = limit,
                Difference = currentValue - limit
            };
        }










    }
}
