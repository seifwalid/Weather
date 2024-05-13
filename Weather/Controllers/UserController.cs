using Microsoft.AspNetCore.Mvc;
using Weather.Data;
using Weather.Dtos.User;
using Weather.Mappers;
using Weather.Model;

namespace Weather.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController:ControllerBase
    {
        private readonly DataContext _context;
        public UserController(DataContext context)
        {
            _context = context;
        }

        //View details of a user 
        [HttpGet("{uid}")]
        public IActionResult GetById([FromRoute] int uid)
        {
            var user = _context.Users.Find(uid);
            if(user==null)
            {
                return NotFound();
            }
            
            return Ok(user.ToUserDto());
        }


        //Register user 
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = userDto.ToUserFromCreateUserDTO();
            _context.Users.Add(userModel);
            _context.SaveChanges();
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


        //update user personal details
        [HttpPut]
        [Route("{uid}")]
        public IActionResult Update([FromRoute] int uid, [FromBody] UpdateUserDto updateDto)
        {
            var userModel = _context.Users.FirstOrDefault(u => u.UserId == uid);
            if (userModel == null)
            {
                return NotFound();
            }
            userModel.UserName = updateDto.UserName;
            userModel.Email = updateDto.Email;
            userModel.Age = updateDto.Age;
            userModel.Gender = updateDto.Gender;
            userModel.Password = updateDto.Password;
            _context.SaveChanges();
            return Ok(userModel.ToUserDto());
        }

        




    }
}
