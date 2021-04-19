using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideosAct : ContentPage
    {
        List<WebView> vid = new List<WebView>();
        Adv nowUser;
        public VideosAct(Adv now)
        {
            nowUser = now;
            InitializeComponent();
            Request();
        }

        public async void Request()
        {
            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.url + "video/?phone=" + nowUser.phone);
            var responseBody = await answer.Content.ReadAsStringAsync();
            char[] sym = new char[] { '[', ']', '{', '}', '"', ',' };
            foreach (var ch in sym)
            {
                responseBody = responseBody.Replace(ch.ToString(), "");
            }
            responseBody = responseBody.Replace("name:", " ").Trim(' ');
            Console.WriteLine(responseBody);
            var dictionary = responseBody.Split(' ');
            videos.Children.Clear();
            if (dictionary.Count() == 0)
            {
                Label l = new Label();
                l.Text = "У Вас нет загруженных видеороликов";
                l.HorizontalOptions = LayoutOptions.Center;
                l.VerticalOptions = LayoutOptions.Center;
                videos.Children.Add(new Label());
            }
            else
            {
                foreach (var x in dictionary) {
                    Button btn = new Button { Text = x };
                    btn.Clicked += async (sender, args) => await Navigation.PushAsync(new Page1("http://46.101.167.149:8003/api/?name=Pexels_Videos_2313069_iwOTG0H.mp4"));
                    videos.Children.Add(btn);


                    //Button btn1 = new Button { Text = "Водители" };
                    //Grid.SetRow(btn1, 3);
                    //btn.Clicked += async (sender, args) => await Navigation.PushAsync(new Drivers(nowUser));
                    //videos1.Children.Add(btn1);


                    //Grid bodygr = new Grid
                    //{
                    //    BackgroundColor = Color.FromHex("FAFAFA")
                    //};
                    //Grid headgr = new Grid
                    //{
                    //    BackgroundColor = Color.White
                    //};
                    //Grid.SetColumnSpan(headgr, 4);
                    //Button upbtn = new Button
                    //{
                    //    BackgroundColor = Color.FromHex("00000000"),
                    //    Text = "Загруженные",
                    //    TextColor = Color.FromHex("F39F26")
                    //};
                    //Grid.SetColumn(upbtn,0);
                    //Button okbtn = new Button
                    //{
                    //    BackgroundColor = Color.FromHex("0B0000AA"),
                    //    Text = "Одобренные",
                    //    TextColor = Color.FromHex("BCBCBC")
                    //};
                    //Grid.SetColumn(okbtn,1);
                    //okbtn.Clicked += async (sender, args) => await Navigation.PushAsync(new VideoAppr(nowUser));
                    //DatePicker dt = new DatePicker
                    //{
                    //    MinimumDate = new DateTime(2021, 3, 14),
                    //    MaximumDate = new DateTime(2021, 12, 31),
                    //    Date = new DateTime(2021, 3, 14),
                    //    BackgroundColor = Color.FromHex("FFFFFF"),
                    //    TextColor = Color.FromHex("F39F26")
                    //};
                    //Grid.SetRow(dt, 1);
                    //Grid.SetColumnSpan(dt, 4);
                    //StackLayout sl = new StackLayout
                    //{

                    //    Children = { new WebView { Source = "http://46.101.167.149:8003/api/?name=Pexels_Videos_2313069_iwOTG0H.mp4" } }
                    //};
                    //ScrollView sv = new ScrollView
                    //{
                    //    Content = sl
                    //};

                    //Frame frvid = new Frame
                    //{
                    //    Margin = new Thickness(10, 10, 10, 10),
                    //    Content = sv
                    //};
                    //Grid.SetRow(frvid, 2);
                    //Grid.SetColumnSpan(frvid, 4);
                    //Frame fr = new Frame
                    //{
                    //    BackgroundColor = Color.FromHex("F39F26"),
                    //    CornerRadius = 100,
                    //    VerticalOptions = LayoutOptions.Center,
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    HeightRequest = 20,
                    //    WidthRequest = 20
                    //};
                    //Grid.SetRowSpan(fr, 2);
                    //Grid.SetRow(fr, 8);
                    //Grid.SetColumn(fr, 3);
                    //Label lb = new Label
                    //{ 
                    //    Text = "+",
                    //    BackgroundColor = Color.FromHex("F39F26"),
                    //    TextColor = Color.White,
                    //    FontSize = 40,
                    //    VerticalOptions = LayoutOptions.Center,
                    //    HorizontalOptions = LayoutOptions.Center
                    //};
                    //Grid.SetRowSpan(lb, 2);
                    //Grid.SetColumn(lb, 3);
                    //Grid.SetRow(lb, 8);
                    //Button bttn = new Button
                    //{ 
                    //    VerticalOptions = LayoutOptions.Center,
                    //    HorizontalOptions = LayoutOptions.Center,
                    //    HeightRequest = 60,
                    //    WidthRequest = 60,
                    //    BackgroundColor = Color.FromHex("00000000")
                    //};
                    //bttn.Clicked += async (sender, args) => await Navigation.PushAsync(new Loader(nowUser));
                    //Grid.SetColumn(bttn, 3);
                    //Grid.SetRow(bttn, 8);
                    //Grid.SetRowSpan(bttn, 2);
                    //headgr.Children.Add(upbtn);
                    //headgr.Children.Add(okbtn);
                    //bodygr.Children.Add(headgr);
                    //bodygr.Children.Add(dt);
                    //bodygr.Children.Add(frvid);
                    //bodygr.Children.Add(fr);
                    //bodygr.Children.Add(lb);
                    //bodygr.Children.Add(bttn);
                    //stack.Children.Add(bodygr);
                }
            }
        }

        private async void ToAppr(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VideoAppr(nowUser));
            videos.Children.Clear();
        }

        private async void NewVideo(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Loader(nowUser));
            videos.Children.Clear();
        }

        

    }
}