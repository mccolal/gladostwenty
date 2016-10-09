using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using gladostwenty.core.Models;
using Microsoft.WindowsAzure.MobileServices;

namespace gladostwenty.droid.Services {
    class AzureDataServiceDroid : core.Services.IAzureDataService {

        public static MobileServiceClient Client { get; private set; }

        public Task<List<User>> GetUserTable() {

            return Client.GetTable<User>().ToListAsync();
        }

        public MobileServiceClient Initialize() {
            Client = new MobileServiceClient("http://hywglados.azurewebsites.net");
            CurrentPlatform.Init();
            return Client;
        }
    }
}