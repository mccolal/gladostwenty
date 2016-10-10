using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.Models {
    public class CurrentUser {
        public static string id = "107e1121-2bdc-47b5-9acc-df72ea6b5a20";

        private static bool authenticated = false;

        public static bool Authenticated {
            get {
                return authenticated;
            }
            set {
                authenticated = value;
            }
        }
    }
}
