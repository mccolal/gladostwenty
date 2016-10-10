using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.Models {
    public class Status {

        public string Id { get; set; }

        public string ToId { get; set; }

        public string FromId { get; set; }

        public string Message { get; set; }

        public bool Read { get; set; }

        public string Lat { get; set; }

        public string Long { get; set; }
    }
}
