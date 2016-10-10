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
using System.IO;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace gladostwenty.droid.Services {
    class AzureDataServiceDroid : core.Services.IAzureDataService {

        public static MobileServiceClient Client { get; private set; }

        private string localDbName = "glados.db3";

        private IMobileServiceSyncTable<User> UserTable { get; set; }

        private IMobileServiceSyncTable<Status> StatusTable { get; set; }

        public Task<List<User>> GetUserTable() {

            return UserTable.ToListAsync();
        }

        public async Task<MobileServiceClient> Initialize() {
            Client = new MobileServiceClient("http://hywglados.azurewebsites.net");
            CurrentPlatform.Init();
            
            UserTable = Client.GetSyncTable<User>();
            StatusTable = Client.GetSyncTable<Status>();

            await InitLocalStoreAsync();
            await SyncUsersAsync();
            await SyncStatusAsync();

            return Client;
        }

        public async Task InitLocalStoreAsync() {

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), localDbName);

            if (!File.Exists(path)) {
                File.Create(path).Dispose();
            }

            var store = new MobileServiceSQLiteStore(path);
            store.DefineTable<Status>();
            store.DefineTable<User>();

            await Client.SyncContext.InitializeAsync(store);
        }

        public async Task SyncUsersAsync() {
            try {
                await Client.SyncContext.PushAsync();
                await UserTable.PullAsync("allUsers", UserTable.CreateQuery().Where(u => u.id != CurrentUser.id)) ;
            }catch(Exception e) {
                
            }
        }

        public async Task SyncStatusAsync() {
            try {
                
                await Client.SyncContext.PushAsync();
                await StatusTable.PullAsync("allMyStatuses", StatusTable.CreateQuery());
            } catch(Exception e) {

            }
        }

        public Task<List<Status>> GetStatusTable() {
            return StatusTable.ToListAsync();
        }

    }
}