
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
namespace App1.Drivers
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alerts : ContentPage
    {
        Driver nowUser;
        public Alerts(Driver now)
        {
            InitializeComponent();
            nowUser = now;
            Request();
        }
        public async void Request()
        {

            HttpClient client = new HttpClient();

            var answer = await client.GetAsync(Server.url + "alert/?phone=" + nowUser.phone.Trim('"'));
            var responseBody = await answer.Content.ReadAsStringAsync();
            char[] sym = new char[] { '[', ']', '{', ','};
            foreach (var ch in sym)
            {
                responseBody = responseBody.Replace(ch.ToString(), "");
            }
            responseBody = responseBody.Replace("text\":", " ").Trim(' ');
            Console.WriteLine(responseBody);
            Console.WriteLine(Server.url + "alert/?phone=" + nowUser.phone);
            var dictionary = responseBody.Trim('}').Split('}');
            
            //foreach (var x in dictionary)
            //{
            //    Grid gr = new Grid
            //    {
            //        RowSpacing = 2,
            //        ColumnSpacing = 4,
            //        RowDefinitions =
            //        {
            //            new RowDefinition{Height = 30 },
            //            new RowDefinition{Height = 2},
            //            new RowDefinition{Height = 30 },
            //            new RowDefinition(),
            //            new RowDefinition {Height = 30 }
            //        }
            //    };
            //    Label notLabel = new Label
            //    {
            //        Text = "Уведомление"
            //    };
            //    Grid.SetColumnSpan(notLabel, 3);
            //    Label dateLabel = new Label
            //    {
            //        Text = "3/5/2021"
            //    };
            //    Grid.SetColumn(dateLabel, 3);
            //    BoxView lineBox = new BoxView
            //    {
            //        BackgroundColor = Color.Black,
            //        HeightRequest = 1,
            //        HorizontalOptions = LayoutOptions.Fill,
            //        VerticalOptions = LayoutOptions.Center
            //    };
            //    Grid.SetColumnSpan(lineBox, 4);
            //    Grid.SetRow(lineBox, 1);
            //    Label titleLable = new Label
            //    {
            //        Text = "Не проходит выплата",
            //        FontAttributes = FontAttributes.Bold,
            //        FontSize = 16
            //    };
            //    Grid.SetColumnSpan(titleLable, 4);
            //    Grid.SetRow(titleLable, 2);
            //    Grid.SetColumn(dateLabel, 3);
            //    Label textLabel = new Label
            //    {
            //        Text = x.Trim('"').Trim(' ').Trim('"')
            //    };
            //    Grid.SetColumnSpan(textLabel, 4);
            //    Grid.SetRow(textLabel, 3);
            //    gr.Children.Add(notLabel);
            //    gr.Children.Add(dateLabel);
            //    gr.Children.Add(lineBox);
            //    gr.Children.Add(titleLable);
            //    gr.Children.Add(textLabel);
            //    Frame fr = new Frame { CornerRadius = 10, Margin = new Thickness(10, 20, 10, 10), MinimumHeightRequest = 100, MinimumWidthRequest = 50 };
            //    fr.Content = gr;
            //    Stack.Children.Add(fr);
            //    Console.WriteLine(x);
            //}
        }
        }
} 