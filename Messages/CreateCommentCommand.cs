using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages
{
    public class CreateCommentCommand
    {
        public string Text { get; set; }
        public string Author { get; set; }

    }
}