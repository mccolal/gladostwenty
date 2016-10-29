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

namespace gladostwenty.droid.Views
{
    public class BindableProgress
    {
        private readonly Context _context;

        public BindableProgress(Context context)
        {
            _context = context;
        }

        private ProgressDialog _dialog;
        private ProgressDialog _sendRequestDialog;


        public bool SendRequestDialogVisible
        {
            get { return _sendRequestDialog != null; }
            set
            {
                if (value == SendRequestDialogVisible)
                    return;

                if (value)
                {
                    _sendRequestDialog = new ProgressDialog(_context);
                    _sendRequestDialog.SetTitle("Requesting...");
                    _sendRequestDialog.Show();
                }
                else
                {
                    _sendRequestDialog.Hide();
                    _sendRequestDialog = null;
                }
            }
        }

        public bool Visible
        {
            get { return _dialog != null; }
            set
            {
                if (value == Visible)
                    return;

                if (value)
                {
                    _dialog = new ProgressDialog(_context);
                    _dialog.SetTitle("Sending...");
                    _dialog.Show();
                }
                else
                {
                    _dialog.Hide();
                    _dialog = null;
                }
            }
        }
    }
}