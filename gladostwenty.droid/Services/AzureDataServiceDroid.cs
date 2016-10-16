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
            
            StatusTable = Client.GetSyncTable<Status>();
            UserTable = Client.GetSyncTable<User>();

            await InitLocalStoreAsync();
            await SyncStatusAsync();
            await SyncUsersAsync();

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
                await UserTable.PullAsync("AllUsers", UserTable.CreateQuery()) ;
            }catch(Exception e) {
                
            }
        }

        public async Task SyncStatusAsync() {
            try {
                
                //await Client.SyncContext.PushAsync();
                await StatusTable.PullAsync("allStatuses", StatusTable.CreateQuery());
            } catch(Exception e) {
                Toast.MakeText(null, e.ToString(), ToastLength.Long).Show();
            }
        }

        public async Task<List<Status>> GetStatusTable() {


            //return await Client.GetTable<Status>().Where(u => u.ToId == CurrentUser.id).ToListAsync();

            var a = await StatusTable.ToListAsync();

            return a;
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

        /// <summary>
        /// 
        /// Gets the latest status for a user using their id as the search parameter
        /// 
        /// </summary>
        /// <param name="id">The Id of the user</param>
        /// <returns>A status object representing the user's last status</returns>
        public async Task<Status> GetUserStatus(string id) {

            var query = StatusTable.CreateQuery().Where(u => u.FromId == id && u.Request == false);

            var result = await StatusTable.ToListAsync();

            if(result == null || result.Count <= 0) {
                return null;
            }else {
                return result.First();
            }
        }
    }
}