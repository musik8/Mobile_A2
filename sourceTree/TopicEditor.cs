using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;

namespace sourceTree
{
    public class TopicEditor
    {

        
        public ObservableCollection<Topic> getTopic()
        {
            var tasks = new ObservableCollection<Topic>();
            return tasks;
        }
       
        public static Task<Topic> InputBox(INavigation navigation, Topic editTopic, bool rootTopic = true)
        {

           
            if (editTopic == null)
            {
                editTopic = new Topic();
                editTopic.isSub = rootTopic;    
            }

                
           
            var tcs = new TaskCompletionSource<Topic>();

            var lblTitle = new Label { Text = "Title:", HorizontalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, TextType = TextType.Html };
            var lblMessage = new Label { Text = "Add A new Topic:" ,FontAttributes = FontAttributes.Bold };
            var Sinput = new Label { Text = "Enter Source:", FontAttributes = FontAttributes.Bold  };
            var Dinput = new Label { Text = "Enter Description:" , FontAttributes = FontAttributes.Bold };
            var Imginput = new Label { Text = "Image Path" , FontAttributes = FontAttributes.Bold };
            var TitleInput = new CustomEntry { 
                Text = editTopic.Title,
                HorizontalOptions = LayoutOptions.Fill,
              
              
            };
            var SourceInput = new Entry { Text = editTopic.Source };
            var DescInput = new Editor { Text = editTopic.Desc,
                HeightRequest = 200,  
            };
            var imagePath = editTopic.ImagePath;
            var onlineImageLink = editTopic.OnlineImagePath;


            var btnAddImage = new Button
            {
                Text = "Pick Image",
                BackgroundColor = Color.FromRgb(133, 219, 197),
            };

            btnAddImage.Clicked += async (s, e) =>
            {
                (s as Button).IsEnabled = false;
                PhotoManager pm = new PhotoManager();
                imagePath = await pm.AddImageAsync();
                Imginput.Text = imagePath;
                onlineImageLink = "";
                (s as Button).IsEnabled = true;
            };

            var btnOk = new Button
            {
                Text = "Add",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(255, 143, 97),
            };
            btnOk.Clicked += async (s, e) => 
            {
               
                editTopic.Title = TitleInput.Text;
                editTopic.Source = SourceInput.Text;
                editTopic.Desc = DescInput.Text;
                editTopic.ImagePath = imagePath;
                editTopic.OnlineImagePath = onlineImageLink;
                await navigation.PopModalAsync();
                tcs.SetResult(editTopic);
            };

            var btnCancel = new Button
            {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromRgb(255, 143, 97)
            };
            btnCancel.Clicked += async (s, e) =>
            {
              
                await navigation.PopModalAsync();
                tcs.SetResult(null);
            };

            var slButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,

                Children = { btnOk, btnCancel },
            };
            var isUrgentLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { btnAddImage },

            };

            var layout = new StackLayout
            {
                // Padding = new Thickness(0, 40, 0, 0),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Fill,
                Orientation = StackOrientation.Vertical,
                Margin = 10,
                Children = { lblTitle, lblMessage, TitleInput, Sinput, SourceInput, Dinput, DescInput, Imginput, btnAddImage, slButtons },
            };

          
            var page = new ContentPage();
            page.Content = layout;
            navigation.PushModalAsync(page);
          
            return tcs.Task;
        }
    }
}