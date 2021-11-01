using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace sourceTree
{
   
    public partial class TopicDisplay : ContentPage
    {

      
        PhotoManager pm = new PhotoManager();
         DatabaseManager db;
        Topic currTopic;
        ObservableCollection<Topic> subTopics;

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged(); }
        }


        public TopicDisplay(Topic mTopic, DatabaseManager db)
        {


            this.currTopic = mTopic;
            this.db = db;
            this.BindingContext = this;
           
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
           
            Title = this.currTopic.Title;
            onlineUrl.Text = this.currTopic.OnlineImagePath;
            imageDisplay.Source = this.currTopic.ImagePath;
            TitleText.Text = this.currTopic.Title;
            SourceText.Text = this.currTopic.Source;
            DescriptionText.Text = this.currTopic.Desc;

            currTopic = await db.getTopic(this.currTopic.Id);
            subTopics = new ObservableCollection<Topic>(currTopic.subTopics);
            SubTopicTable.ItemsSource = subTopics;
            base.OnAppearing();

        }

        private void addSubTopic(object sender, EventArgs e)
        {

        }
     
        private async void Button_Clicked(object sender, EventArgs e)
        {

            (sender as Button).IsEnabled = false;

            var updatedTopic = await pm.UploadImage(currTopic);
            onlineUrl.Text = updatedTopic;
            currTopic.OnlineImagePath = updatedTopic;
            db.updateTopic(currTopic);
            (sender as Button).IsEnabled = true;
        }

        private async void  SubTopicTable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await this.Navigation.PushAsync(new TopicDisplay(SubTopicTable.SelectedItem as Topic, this.db));
        }
        //public void RefreshData()
        //{
        //    listItems.Add("Xamarin Monkeys");
        //    SubTopicTable.ItemsSource = null;
        //    SubTopicTable.ItemsSource
        //    listPlatforms.ItemsSource = listItems;
        //    currTopic = await db.getTopic(this.currTopic.Id);
        //    subTopics = new ObservableCollection<Topic>(currTopic.subTopics)
        //}
        private async void Add_Clicked(object sender, EventArgs e)
        {
            Topic newTopic = await TopicEditor.InputBox(this.Navigation, null);
            db.InsertSubTopic(newTopic, currTopic);
          
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            Topic newTopic = await TopicEditor.InputBox(this.Navigation, currTopic);
            db.updateTopic(newTopic);
        }

        private async void Share_Clicked(object sender, EventArgs e)
        {

            string ClipBoard = "";
            string UrlLink;
            if (currTopic.OnlineImagePath == "")
            {
                UrlLink = await pm.UploadImage(currTopic);
                currTopic.OnlineImagePath = UrlLink;
                onlineUrl.Text = UrlLink;
                db.updateTopic(currTopic);

            }

            ClipBoard = currTopic.Title + "\n" + currTopic.OnlineImagePath + "\n" + "Source: " + currTopic.Source + "\n" + currTopic.Desc;

            await Clipboard.SetTextAsync(ClipBoard);
        }



    }
}