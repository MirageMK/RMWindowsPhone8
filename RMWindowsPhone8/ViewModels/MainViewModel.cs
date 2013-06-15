using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RMWindowsPhone8.Resources;
using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Linq;
using Newtonsoft.Json;
using Microsoft.Phone.Shell;
using RMWindowsPhone8.Database;
using System.Collections.Generic;

namespace RMWindowsPhone8.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private static string webServiceURL = "http://rmservice.apphb.com/Service1.svc/getAllGroups";

        public MainViewModel()
        {
            this.Groups = new ObservableCollection<GroupViewModel>();
        }


        public ObservableCollection<GroupViewModel> _groups;
        /// <summary>
        /// A collection for GroupViewModel objects.
        /// </summary>
        public ObservableCollection<GroupViewModel> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                if (value != _groups)
                {
                    _groups = value;
                    NotifyPropertyChanged("Groups");
                }
            }
        }

        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadDataFromWS()
        {
            SystemTray.ProgressIndicator.Text = "Downloading data form web service...";
            var client = new WebClient();
            client.DownloadStringCompleted += client_DownloadStringCompleted;
            client.DownloadStringAsync(new Uri(webServiceURL));
        }

        public void LoadDataFromDB()
        {
            SystemTray.ProgressIndicator.Text = "Downloading data form database...";
            using (DatabaseDataContext DBDC = new DatabaseDataContext(DatabaseDataContext.DBConnectionString))
            {
                var query = from g in DBDC.groups select g;
                this.Groups = new ObservableCollection<GroupViewModel>(query);
            }
            this.IsDataLoaded = true;
            SystemTray.ProgressIndicator.IsVisible = false;
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var textData = (string)e.Result;

            var rootObject = JsonConvert.DeserializeObject<RootObject>(textData);

            foreach (GroupViewModel b in rootObject.getAllGroupsResult)
            {
                this.Groups.Add(b);
            }

            this.IsDataLoaded = true;
            SystemTray.ProgressIndicator.IsVisible = false;
            RefreshDB();
        }

        public void InsertGroup(GroupViewModel groupToInsert)
        {
            using (DatabaseDataContext DBDC = new DatabaseDataContext(DatabaseDataContext.DBConnectionString))
            {
                DBDC.groups.InsertOnSubmit(groupToInsert);
                DBDC.SubmitChanges();
            }
        }

        public void DeleteGroup(GroupViewModel groupToDelete)
        {
            this.Groups.Remove(groupToDelete);
            using (DatabaseDataContext DBDC = new DatabaseDataContext(DatabaseDataContext.DBConnectionString))
            {
                IQueryable<GroupViewModel> seriequery = from g in DBDC.groups where g.ID == groupToDelete.ID select g;
                GroupViewModel returnedGroup = seriequery.FirstOrDefault();
                DBDC.groups.DeleteOnSubmit(returnedGroup);
                DBDC.SubmitChanges();
            }
        }

        public void DeleteAllGroups()
        {
            using (DatabaseDataContext DBDC = new DatabaseDataContext(DatabaseDataContext.DBConnectionString))
            {
                IEnumerable<GroupViewModel> groups = (from g in DBDC.groups select g).ToList();
                DBDC.groups.DeleteAllOnSubmit(groups);
                DBDC.SubmitChanges();
            }
        }

        public void RefreshDB()
        {
            DeleteAllGroups();
            foreach (GroupViewModel group in this.Groups)
            {
                InsertGroup(group);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}