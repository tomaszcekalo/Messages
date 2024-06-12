using Messages.Hateoas;
using Messages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    internal class GetMessageResponse : Message
    {
        public GetMessageResponse(Message message)
        {
            this.Text = message.Text;
            this.Author = message.Author;
            this.Created = message.Created;
            this.Id = message.Id;
            this.Comments = message.Comments;
        }

        public List<LinkItem> Links { get; set; } = new List<LinkItem>();
        public int RequestNumber { get; internal set; }
    }
}