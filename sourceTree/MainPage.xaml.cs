using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace sourceTree
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Topic> listTopics = new ObservableCollection<Topic>();
        DatabaseManager dataBaseManager = new DatabaseManager();

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            listTopics = await dataBaseManager.CreateTabel();
            TopicTable.ItemsSource = listTopics;
            base.OnAppearing();

        }

        private async void TopicTable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await this.Navigation.PushAsync(new TopicDisplay(TopicTable.SelectedItem as Topic, dataBaseManager));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Topic newTopic = await TopicEditor.InputBox(this.Navigation, null, false);

            dataBaseManager.InsertTopic(newTopic);
            listTopics.Add(newTopic);
        }
    }
}
