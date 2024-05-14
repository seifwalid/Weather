using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weather.Data;
using Weather.Dtos.Limit;
using Weather.Dtos.User;
using Weather.Interfaces;
using Weather.Mappers;

namespace Weather.Controllers
{
    [Route("api/limit")]
    [ApiController]
    public class LimitsController:ControllerBase
    {
        private readonly ILimitRepository _limitRepo;
        private readonly IUserRepository _userRepo;

        private readonly DataContext _context;
        public LimitsController(DataContext context,ILimitRepository limitRepo, IUserRepository userRepo)
        {
            _context = context;
            _limitRepo = limitRepo;
            _userRepo = userRepo;

        }


        //Get all limits in db
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var limits = await _limitRepo.GetAllLimitsAsync();
            var limitsDto = limits.Select(l => l.ToLimitDto());
            return Ok(limitsDto);
        }

        

        //Get Limits by LimitId
        [HttpGet("{limitid}")]
        public async Task<IActionResult> GetById([FromRoute] int limitid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var limit= await _limitRepo.GetLimitByIdAsync(limitid);
            if (limit == null)
            {
                return NotFound();
            }
            return Ok(limit.ToLimitDto());
        }

        //Create or Set new Thresholds
        [HttpPost("{userId}")]
        public async Task<ActionResult> CreateLimits([FromRoute] int userId, [FromBody] CreateLimitDto limitDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!await _userRepo.UserExists(userId))
            {
                return BadRequest("No user existswith this id");
            }
            var limitModel=limitDto.ToLimitFromCreateLimitDTO();
            await _limitRepo.CreateLimitAsync(limitModel);
            return CreatedAtAction(nameof(GetById), new { id = limitModel.Id }, limitModel.ToLimitDto());
        }



        
        //Update parameter limits (thresholds)
        [HttpPut]
        [Route("{id}")]
        public async  Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLimitDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingLimits = await _limitRepo.UpdateLimitAsync(id, updateDto.ToLimitFromUpdateDTO());
            
            if (existingLimits==null)
            {
                return NotFound("No Permissible Limits were found with this Id!");
            }

            return Ok(existingLimits.ToLimitDto());
        }

        //[HttpDelete]
        //[Route("{limitId}")]
        //public async Task<IActionResult> Delete([FromRoute] int limitId)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var existingLimit=await _limitRepo.DeleteLimitAsync(limitId);

        //    if (existingLimit == null)
        //    {
        //        return NotFound("No limits were found!");
        //    }
        //    return Ok("Limits Deleted");

        //}






    }
}
