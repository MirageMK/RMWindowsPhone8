using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RMWindowsPhone8.ViewModels
{
    [Table]
    public class GroupViewModel : INotifyPropertyChanged
    {//"key":"group1","subtitle":"You so want one.","title":"Burgers & Sandwiches"}
        private string _id;
        /// <summary>
        /// Sample ViewModel property; this property is used to identify the object.
        /// </summary>
        /// <returns></returns>
        [Column(IsPrimaryKey = true, CanBeNull = false)]
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                if (value != _id)
                {
                    NotifyPropertyChanging("ID");
                    _id = value;
                    NotifyPropertyChanged("ID");
                }
            }
        }

        private string _backgroundImage;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        [Column]
        public string backgroundImage
        {
            get
            {
                return _backgroundImage;
            }
            set
            {
                if (value != _backgroundImage)
                {
                    NotifyPropertyChanging("backgroundImage");
                    _backgroundImage = value;
                    NotifyPropertyChanged("backgroundImage");
                }
            }
        }

        private string _description;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        [Column]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    NotifyPropertyChanging("description");
                    _description = value;
                    NotifyPropertyChanged("description");
                }
            }
        }

        private string _key;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        [Column]
        public string key
        {
            get
            {
                return _key;
            }
            set
            {
                if (value != _key)
                {
                    NotifyPropertyChanging("key");
                    _key = value;
                    NotifyPropertyChanged("key");
                }
            }
        }

        private string _subtitle;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        [Column]
        public string subtitle
        {
            get
            {
                return _subtitle;
            }
            set
            {
                if (value != _subtitle)
                {
                    NotifyPropertyChanging("subtitle");
                    _subtitle = value;
                    NotifyPropertyChanged("subtitle");
                }
            }
        }

        private string _title;
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding.
        /// </summary>
        /// <returns></returns>
        [Column]
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value != _title)
                {
                    NotifyPropertyChanging("title");
                    _title = value;
                    NotifyPropertyChanged("title");
                }
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

        public event PropertyChangingEventHandler PropertyChanging;
        private void NotifyPropertyChanging(string propertyName)
        {
            PropertyChangingEventHandler handler = PropertyChanging;
            if (null != handler)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }
    }

    public class RootObject
    {
        public List<GroupViewModel> getAllGroupsResult { get; set; }
    }
}