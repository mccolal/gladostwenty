using gladostwenty.core.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gladostwenty.core.Services {
    public interface IAzureDataService {

        Task<MobileServiceClient> Initialize();

        Task<List<User>> GetUserTable();

        Task<List<Status>> GetStatusTable();

        Task<User> GetUser(string id);

        void SendStatus(string to, string from, string msg, bool request);
    }
}
