using Messages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        public MessagesController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public DataContext DataContext { get; }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DataContext.Messages.ToList());
        }
        [HttpPost]
        public IActionResult Post(Message message)
        {
            DataContext.Messages.Add(message);
            DataContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var message = DataContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            DataContext.Messages.Remove(message);
            DataContext.SaveChanges();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }
            DataContext.Entry(message).State = EntityState.Modified;
            DataContext.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var message = DataContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

    }
}
