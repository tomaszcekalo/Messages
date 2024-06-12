using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("Message")]
        public int MessageID { get; set; }

        public virtual Message Message { get; set; }
    }
}