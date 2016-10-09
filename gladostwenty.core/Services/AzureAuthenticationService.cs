using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gladostwenty.core.Models;
using System.Net;
using System.IO;

namespace gladostwenty.core.Services {
    class AzureAuthenticationService : IAzureAuthenticationService {

        const string authenticationURI = "http://gladostelstra.azurewebsites.net/api/authenticateapi";

        public async Task<User> Authenticate() {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(authenticationURI));

            request.ContentType = "application/json";
            request.Method = "POST";

            var c = await request.GetResponseAsync();
            var a = c;
            return null;
        }
    }
}
