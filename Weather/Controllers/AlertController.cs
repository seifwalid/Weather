using Microsoft.AspNetCore.Mvc;
using Weather.Data;

namespace Weather.Controllers
{
    public class AlertController:ControllerBase
    {
        private readonly DataContext _context;
        public AlertController(DataContext context)
        {
            _context = context;
        }



        //Get all alerts of a user to display in his dashboard
        [HttpGet("{userId}")]
        public IActionResult GetByUserId([FromRoute] int userId )
        {
            var alert = _context.Alerts.SingleOrDefault(a=>a.UserID==userId);
            if(alert == null)
            {
                return NotFound();
            }
            return Ok(alert);
        }














    }
}
