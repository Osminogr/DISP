
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
using App1.Domain;

namespace App1.Drivers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chat : ContentPage
    {
        Driver nowUser = new Driver();
        public Chat(Driver now)
        {
            nowUser = now;
            InitializeComponent();
            Request();
        }
        public async void Request()
        {

            HttpClient client = new HttpClient();
            Stack.Children.Clear();
            var answer = await client.GetAsync(Server.url + "msg/?phone=" + nowUser.person.phone.Trim('"'));
            var responseBody = await answer.Content.ReadAsStringAsync();
            char[] sym = new char[] { '[', ']', '{', ',' };
            foreach (var ch in sym)
            {
                responseBody = responseBody.Replace(ch.ToString(), "");
            }
            responseBody = responseBody.Replace("text\":", " ").Trim(' ');
            Console.WriteLine(responseBody);
            Console.WriteLine(Server.url + "msg/?phone=" + nowUser.person.phone);
            var dictionary = responseBody.Trim('}').Split('}');
            foreach (var x in dictionary)
            {
                Grid gr = new Grid
                {
                    RowSpacing = 2,
                    ColumnSpacing = 4,
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    }
                };
                Label notLabel = new Label
                {
                    Text = "Сообщение"
                };
                Grid.SetColumnSpan(notLabel, 3);
                Label textLabel = new Label
                {
                    Text = x.Trim('"').Trim(' ').Trim('"')
                };
                Grid.SetColumnSpan(textLabel, 4);
                Grid.SetRow(textLabel, 2);
                gr.Children.Add(notLabel);
                gr.Children.Add(textLabel);
                Frame fr = new Frame { BackgroundColor=Color.LightGray, CornerRadius = 10, Margin = new Thickness(10, 20, 10, 10), MinimumHeightRequest = 100, MinimumWidthRequest = 50 };
                fr.Content = gr;
                Stack.Children.Add(fr);
                Console.WriteLine(x);
                await sc.ScrollToAsync(0, sc.Height,false);
            }
        }
            async void NewMes()
            {
                
                Grid gr = new Grid
                {
                    RowSpacing = 2,
                    ColumnSpacing = 4,
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    }
                };
                Label notLabel = new Label
                {
                    Text = "Сообщение"
                };
                Grid.SetColumnSpan(notLabel, 3);
                Label textLabel = new Label
                {
                    Text = messege.Text
                };
                Grid.SetColumnSpan(textLabel, 4);
                Grid.SetRow(textLabel, 2);
                gr.Children.Add(notLabel);
                gr.Children.Add(textLabel);
                Frame fr = new Frame {  BackgroundColor = Color.FromHex("F39F26"), CornerRadius = 10, Margin = new Thickness(10, 20, 10, 10), MinimumHeightRequest = 100, MinimumWidthRequest = 50 };
                fr.Content = gr;
                Stack.Children.Add(fr);
            }
        public void SendMsg(object sender, EventArgs e) {
            string content = @"{""msg"":{""author"":";
            content += nowUser.person.phone ;
            content += @",""text"":""";
            content += messege.Text;
            content += @"""}}";
            Console.WriteLine(content);
            Server.Request(content, "post", "msg");
            Request();
            messege.Text = "";
        }
    }
}