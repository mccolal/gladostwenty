using gladostwenty.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.Models
{
    public class StatusListItem
    {
        public User Contact { get; set; }

        public Status Status { get; set; }

        public imgURL imgUrl { get; set; }
    }
}
