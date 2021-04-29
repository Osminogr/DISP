
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
using App1.Domain;
using App1.Utils;
using App1.Templates;

namespace App1.Advs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chat : ContentPage
    {
        Entity nowUser;
        public Chat(Entity now)
        {
            nowUser = now;
            InitializeComponent();

            Adv owner = new Adv();
            owner.id = 1;
            owner.name = "Администратор";

            Message message = new Message();
            message.receiver = nowUser;
            message.owner = nowUser;
            message.status = 1;
            message.text = "Привет! Как дела?";
            message.time = DateTime.Now.ToShortTimeString();

            createMessage(message);

            message = new Message();
            message.receiver = nowUser;
            message.owner = owner;
            message.status = 1;
            message.text = "Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела?";
            message.time = DateTime.Now.ToShortTimeString();

            createMessage(message);

            message = new Message();
            message.receiver = nowUser;
            message.owner = owner;
            message.status = 2;
            message.text = "Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела?";
            message.time = DateTime.Now.ToShortTimeString();

            createMessage(message);

            message = new Message();
            message.receiver = nowUser;
            message.owner = nowUser;
            message.status = 2;
            message.text = "Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела?";
            message.time = DateTime.Now.ToShortTimeString();

            createMessage(message);

            message = new Message();
            message.receiver = nowUser;
            message.owner = owner;
            message.status = 3;
            message.text = "Привет! Как дела? Привет! Как дела? Привет! Как дела? Привет! Как дела?";
            message.time = DateTime.Now.ToShortTimeString();

            createMessage(message);

            OverrideTitleView("Чат", 100, -1);
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        private void createMessage(Message message)
        {
            Stack.Children.Add(new MessageTemplate(message));
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
                Text = message.Text
            };
            Grid.SetColumnSpan(textLabel, 4);
            Grid.SetRow(textLabel, 2);
            gr.Children.Add(notLabel);
            gr.Children.Add(textLabel);
            Frame fr = new Frame { BackgroundColor = Color.FromHex("F39F26"), CornerRadius = 10, Margin = new Thickness(10, 20, 10, 10), MinimumHeightRequest = 100, MinimumWidthRequest = 50 };
            fr.Content = gr;
            Stack.Children.Add(fr);
        }

        public void SendMsg(object sender, EventArgs e)
        {
            string content = @"{""msg"":{""author"":";
            content += "";
            content += @",""text"":""";
            content += message.Text;
            content += @"""}}";
            Console.WriteLine(content);
            Server.Request(content, "post", "msg");
            message.Text = "";
        }
    }
}