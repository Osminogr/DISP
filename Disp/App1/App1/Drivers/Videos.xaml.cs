using MediaManager.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Templates;

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
            Reload();
        }

        public async void Request()
        {

            HttpClient client = new HttpClient();
            var i = 0;
            var answer = await client.GetAsync(Server.url + "video/?phone=" + nowUser.person.phone);
            var responseBody = await answer.Content.ReadAsStringAsync();

            var dictionary = responseBody
            .Split(',')
            .ToList<string>();
            foreach (var x in dictionary)
            {

                videos.Children.Add(new VideoTemplate { Name = x, Url = "http://46.101.167.149:8000/media/" + x });
            }
        }

        async void Reload()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();

            request.RequestUri = new Uri(Server.url + "video/?phone=" + nowUser.person.phone);
            request.Method = HttpMethod.Get;

            var answer = await client.SendAsync(request);

            if (answer.StatusCode == System.Net.HttpStatusCode.OK)
            {
                foreach (var x in vid)
                {
                    videos.Children.Clear();

                    videos.Children.Add(x);
                }
            }
            else
            {
                videos.Children.Add(new Label()
                {
                    Text = "Нет проигрываемых видео",
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center
                });
            }
        }
    }
}