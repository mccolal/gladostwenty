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

        public Task<List<user>> GetUserTable() {

            return Client.GetTable<user>().ToListAsync();
        }

        public MobileServiceClient Initialize() {
            Client = new MobileServiceClient("https://gladostelstra.azurewebsites.net");
            CurrentPlatform.Init();
            return Client;
        }
    }
}