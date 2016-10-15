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
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace gladostwenty.droid.Services {
    class AzureDataServiceDroid : core.Services.IAzureDataService {

        public static MobileServiceClient Client { get; private set; }

        private string localDbName = "glados.db3";

        private IMobileServiceSyncTable<User> UserTable { get; set; }

        private IMobileServiceSyncTable<Status> StatusTable { get; set; }

        public Task<List<User>> GetUserTable() {

            return UserTable.Where(u => u.id != CurrentUser.id).ToListAsync();
        }

        public async Task<MobileServiceClient> Initialize() {
            Client = new MobileServiceClient("http://hywglados.azurewebsites.net");
            CurrentPlatform.Init();
            
            UserTable = Client.GetSyncTable<User>();
            //StatusTable = Client.GetSyncTable<Status>();

            await InitLocalStoreAsync();
            //await SyncStatusAsync();
            await SyncUsersAsync();

            return Client;
        }

        public async Task InitLocalStoreAsync() {

            string path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), localDbName);

            if (!File.Exists(path)) {
                File.Create(path).Dispose();
            }

            var store = new MobileServiceSQLiteStore(path);
            //store.DefineTable<Status>();
            store.DefineTable<User>();

            await Client.SyncContext.InitializeAsync(store);
        }

        public async Task SyncUsersAsync() {
            try {
                await Client.SyncContext.PushAsync();
                await UserTable.PullAsync("AllUsers", UserTable.CreateQuery()) ;
            }catch(Exception e) {
                
            }
        }

        public async Task SyncStatusAsync() {
            try {
                
                await Client.SyncContext.PushAsync();
                await StatusTable.PullAsync("allStatuses", StatusTable.CreateQuery().Where(u => u.ToId == CurrentUser.id));
                var a = StatusTable;
            } catch(Exception e) {

            }
        }

        public async Task<List<Status>> GetStatusTable() {


            return await Client.GetTable<Status>().Where(u => u.ToId == CurrentUser.id).ToListAsync(); ;
            
            //return await StatusTable.ToListAsync();
        }

        public async Task<User> GetUser(string id) {
            return await UserTable.LookupAsync(id);
        }



        public async void SendStatus(string to, string from, string msg, bool request) {
            Dictionary<string, string> param = new Dictionary<string, string>();

            param.Add("to", to);
            param.Add("from", from);
            param.Add("msg", msg);
            param.Add("req", request ? "r" : "s");

            CancellationTokenSource cts = new CancellationTokenSource();
           
            await Client.InvokeApiAsync("StatusAPI", HttpMethod.Post, param, cts.Token);
        }
    }
}