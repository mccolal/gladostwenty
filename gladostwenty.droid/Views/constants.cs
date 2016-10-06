using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace gladostwenty
{
    public static class Constants
    {
        public const string SenderID = "9870008865"; // Google API Project Number
        public const string ListenConnectionString = "Endpoint=sb://glados.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=sOoceOn3HsLNBz/Q/Wz1YkLEGcP/PA24MOBJZyNolFM=";
        public const string NotificationHubName = "gladosnhub";
    }
}