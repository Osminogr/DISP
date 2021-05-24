using MediaManager.Forms;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Templates;
using App1.Utils;

namespace App1.Drivers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Videos : ContentPage
    {
        
        Driver nowUser; List<VideoView> vid = new  List<VideoView>();
        public Videos(Driver now)
        {
            nowUser = now;
            
            BindingContext = vid;
            InitializeComponent();

            Request();
        }

        public async void Request()
        {
            List<Video> list = await Server.GetVideos(nowUser, true);

            videos.Children.Clear();

            bool loaded = false;

            if (list != null && list.Count != 0)
            {
                loaded = true;

                foreach (var item in list)
                {
                    videos.Children.Add(new VideoTemplate() { Name = item.name, Url = item.url, Margin = new Thickness(0, 10, 0, 0) });
                }

                OverrideTitleView("Видеоролики", videos.Children.Count);
            }

            if (!loaded)
            {
                Label label = new Label();
                label.Text = "У Вас нет видеороликов";
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;

                videos.Children.Add(label);

                OverrideTitleView("Видеоролики(0)", -1);
            }
        }

        private void OverrideTitleView(string name, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, count));
        }
    }
}