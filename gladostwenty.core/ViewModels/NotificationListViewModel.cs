﻿using gladostwenty.core.Models;
using gladostwenty.core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace gladostwenty.core.ViewModels {
    public class NotificationListViewModel : MvxViewModel{

        private ObservableCollection<Status> statuses;

        public ObservableCollection<Status> Statuses {
            get {
                return statuses;
            }
            set {
                SetProperty(ref statuses, value);
                UpdateStatusList();
            }
        }

        private ObservableCollection<StatusListItem> statusList;
        public ObservableCollection<StatusListItem> StatusList {
            get {
                return statusList;
            }
            set {
                SetProperty(ref statusList, value);
            }
        }

        private ObservableCollection<Presented> _presentedStatusList;

        public ObservableCollection<Presented> PresentedStatusList
        {
            get
            {
                return _presentedStatusList;
            }
            set
            {
                SetProperty(ref _presentedStatusList, value);
            }
        }

        private ObservableCollection<Presented> templist;

        public ObservableCollection<Presented> Templist
        {
            get
            {
                return templist;
            }
            set
            {
                SetProperty(ref templist, value);
            }
        }

        public ICommand ReplyToRequest { get; set; }

        public ICommand OpenInbox { get; set; }

        public ICommand OpenInboxxx { get; set; }

        public NotificationListViewModel() {

            ReplyToRequest = new MvxCommand<StatusListItem>((s) =>
            {
                ShowViewModel<OnRequestViewModel>(
                    new OnRequestViewModel.StatusInfo
                    {
                        id = s.Status.id,
                        ToId = s.Status.ToId,
                        FromId = s.Status.FromId,
                        Message = s.Status.Message,
                        Seen = s.Status.Seen,
                        Lat = s.Status.Lat,
                        Long = s.Status.Long,
                        Request = s.Status.Request,
                        Name = s.Contact.FullName
                    });
            });

            OpenInbox = new MvxCommand<StatusListItem>((s) =>
            {
                ShowViewModel<RequestStatusViewModel>(new RequestStatusViewModel.StatusInfo
                {
                    id = s.Status.id,
                    ToId = s.Status.ToId,
                    FromId = s.Status.FromId,
                    Message = s.Status.Message,
                    Seen = s.Status.Seen,
                    Lat = s.Status.Lat,
                    Long = s.Status.Long,
                    Request = s.Status.Request,
                    Name = s.Contact.FullName
                });
            });

            OpenInboxxx = new MvxCommand<StatusListItem>((s) =>
            {
                if (s.Status.Request)
                {
                    ShowViewModel<OnRequestViewModel>(
                    new OnRequestViewModel.StatusInfo
                    {
                        id = s.Status.id,
                        ToId = s.Status.ToId,
                        FromId = s.Status.FromId,
                        Message = s.Status.Message,
                        Seen = s.Status.Seen,
                        Lat = s.Status.Lat,
                        Long = s.Status.Long,
                        Request = s.Status.Request,
                        Name = s.Contact.FullName
                    });
                }
                else
                {
                    ShowViewModel<RequestStatusViewModel>(new RequestStatusViewModel.StatusInfo
                    {
                        id = s.Status.id,
                        ToId = s.Status.ToId,
                        FromId = s.Status.FromId,
                        Message = s.Status.Message,
                        Seen = s.Status.Seen,
                        Lat = s.Status.Lat,
                        Long = s.Status.Long,
                        Request = s.Status.Request,
                        Name = s.Contact.FullName
                    });
                }
            });

            Initialize();


        }

        private async void Initialize() {

            var dataService = Mvx.Resolve<IAzureDataService>();

            Statuses = new ObservableCollection<Status>(await dataService.GetStatusTable());

        }

        private async void UpdateStatusList() {
            bool contains = false;
            StatusList = new ObservableCollection<StatusListItem>();
            PresentedStatusList = new ObservableCollection<Presented>();
            var stats = new List<StatusListItem>();
            
            foreach (Status status in Statuses) {
                stats.Add(new StatusListItem { Status = status, Contact = await Mvx.Resolve<IAzureDataService>().GetUser(status.FromId) });
                
            }
            StatusList = new ObservableCollection<StatusListItem>(stats);

            foreach (StatusListItem t in StatusList)
            {
                foreach (Presented p in PresentedStatusList)
                {
                    if (p.Contact.FullName == t.Contact.FullName)
                    {
                        contains = true;
                        if (t.Status.Request && p.Inbound == null)
                        {
                            p.Inbound = t.Status;
                        }
                        if (!(t.Status.Request) && p.Outbound == null)
                        {
                            p.Outbound = t.Status;
                        }
                    }
                }
                if (!contains)
                {
                    if (t.Status.Request)
                    {
                        PresentedStatusList.Add(new Presented
                        {
                            Contact = t.Contact,
                            Inbound = t.Status,
                            Outbound = null
                        });
                    } else
                    {
                        PresentedStatusList.Add(new Presented
                        {
                            Contact = t.Contact,
                            Inbound = null,
                            Outbound = t.Status
                        });
                    }
                }
                contains = false;
            }
            



        }

        public class StatusListItem{
            public User Contact { get; set; }

            public Status Status { get; set; }
        }

        public class Presented
        {
            public User Contact { get; set; }

            public Status Inbound { get; set; }

            public Status Outbound { get; set; }
        }

    }
}