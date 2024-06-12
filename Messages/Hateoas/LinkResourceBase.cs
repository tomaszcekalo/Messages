using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Hateoas
{
    public class LinkResourceBase
    {
        public LinkResourceBase()
        {
        }

        public List<LinkItem> Links { get; set; } = new List<LinkItem>();
    }
}