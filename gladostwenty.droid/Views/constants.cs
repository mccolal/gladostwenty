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
        public const string ListenConnectionString = "Endpoint=sb://gladosnotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=gksLz5YnUj9cxN/ublR0D2IBfD5q29Untg51a9WVa78=";
        public const string NotificationHubName = "gladosnotificationhub";
    }
}