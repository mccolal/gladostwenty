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
using MvvmCross.Droid.Views;
using gladostwenty.core.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace gladostwenty.droid.Views {

    [Activity(Label ="Contact Details", ParentActivity = typeof(FirstView))]
    public class ContactDetailView : MvxActivity {



        BindableProgress _bindableProgress;

        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            _bindableProgress = new BindableProgress(this);

            var set = this.CreateBindingSet<ContactDetailView, ContactDetailViewModel>();
            set.Bind(_bindableProgress).For(p => p.SendRequestDialogVisible).To(vm => vm.AwaitingRequest);
            set.Apply();

            SetContentView(Resource.Layout.ContactDetailView);
        }
    }
}