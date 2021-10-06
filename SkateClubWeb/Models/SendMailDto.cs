using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkateClubWeb.Models
{
    public class SendMailDto
    {
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
    }
}
