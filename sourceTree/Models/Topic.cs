using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace sourceTree
{
    public class Topic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        private string _Title;
        private string _Desc;
        private string _Source;
        private string _ImagePath;
        private string _OnlineImagePath;
        private bool _isSub;



[MaxLength(150)]
        public string Title
        {
            get { return _Title; }
            set
            {
                if (value == _Title)
                    return;
                _Title = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Title)));
                }
            }
        }
        public string ImagePath
        {
            get { return _ImagePath; }
            set
            {
                if (value == _ImagePath)
                    return;
                _ImagePath = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_ImagePath)));
                }
            }
        }

        public string Source
        {
            get { return _Source; }
            set
            {
                if (value == _Source)
                    return;
                _Source = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Source)));
                }
            }
        }
        public string OnlineImagePath
        {
            get { return _OnlineImagePath; }
            set
            {
                if (value == _OnlineImagePath)
                    return;
                _OnlineImagePath = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_OnlineImagePath)));
                }
            }
        }

        public string Desc
        {
            get { return _Desc; }
            set
            {
                if (value == _Desc)
                    return;
                _Desc = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_Desc)));
                }
            }
        }

        public bool isSub
        {
            get { return _isSub; }
            set
            {
                if (value == _isSub)
                    return;
                _isSub = value;

                if (PropertyChanged != null)
                {

                    PropertyChanged(this, new PropertyChangedEventArgs(nameof(_isSub)));
                }
            }
        }

    

        [OneToMany(inverseProperty: "ParentFile", CascadeOperations = CascadeOperation.All)]
        public List<Topic> subTopics { get; set; }

       

        [ForeignKey(typeof(Topic))]
        public int parentId { get; set; }
      


    }
}
