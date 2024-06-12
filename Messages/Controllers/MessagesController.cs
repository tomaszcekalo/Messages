using Messages.Hateoas;
using Messages.Models;
using Messages.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Messages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ICounterService _counterService;

        public MessagesController(DataContext dataContext, ICounterService counterService)
        {
            DataContext = dataContext;
            this._counterService = counterService;
        }

        public DataContext DataContext { get; }

        [HttpGet]
        public IActionResult GetMessages()
        {
            return Ok(DataContext.Messages.ToList());
        }

        [HttpGet("{messageId}/comments")]
        public IActionResult GetComments(int messageId)
        {
            return Ok(DataContext.Comments.Where(x => x.MessageID == messageId).ToList());
        }

        [HttpPost]
        public IActionResult PostMessage(Message message)
        {
            DataContext.Messages.Add(message);
            DataContext.SaveChanges();
            Response.Headers.Add("Location", $"{Request.Host.Value}/api/messages/{message.Id}");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("{messageId}/comments")]
        public IActionResult PostComment(int messageId, CreateCommentCommand comment)
        {
            DataContext.Comments.Add(new Comment()
            {
                Author = comment.Author,
                Created = DateTime.UtcNow,
                MessageID = messageId,
                Text = comment.Text
            });
            DataContext.SaveChanges();
            //Response.Headers.Add("Location", $"{Request.Host.Value}/api/messages/{messageId}");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
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

        [HttpDelete("{messageId}/messages/{commentId}")]
        public IActionResult DeleteComment(int messageId, int commentId)
        {
            var comment = DataContext.Comments.Find(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            DataContext.Comments.Remove(comment);
            DataContext.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult PutMessage(int id, Message message)
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
        public IActionResult GetMessage(int id)
        {
            var number = _counterService.Increment();
            var message = DataContext.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }
            var result = new GetMessageResponse(message);
            result.RequestNumber = number;
            result.Links.Add(new LinkItem()
            {
                Rel = "self",
                Link = $"{Request.Host.Value}/api/messages/{message.Id}"
            });
            result.Links.Add(new LinkItem()
            {
                Rel = "comments",
                Link = $"{Request.Host.Value}/api/messages/{message.Id}/comments"
            });
            return Ok(result);
        }
    }
}