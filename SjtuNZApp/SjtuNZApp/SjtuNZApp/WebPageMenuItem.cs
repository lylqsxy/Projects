using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SjtuNZApp
{

    public class WebPageMenuItem
    {
        public WebPageMenuItem()
        {
            TargetType = typeof(WebPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}
