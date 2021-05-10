
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using System;
using System.Collections.Generic;
using App1.Domain;
using App1.Utils;
using App1.Templates;
using System.Threading;

namespace App1.Advs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chat : ContentPage
    {
        Entity nowUser;
        bool running;
        private readonly SynchronizationContext _context;
        List<Message> existsMessage = new List<Message>();
        public Chat(Entity now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Чат", 100, -1);

            _context = SynchronizationContext.Current;
            
            running = true;
            Thread t = new Thread(new ThreadStart(GetMessages));
            t.Start();
        }

        private async void GetMessages()
        {
            while (running)
            {
                try
                {
                    List<Message> messages = await Server.GetMessages(nowUser);

                    if (messages != null && messages.Count != 0)
                    {
                        List<View> templates = new List<View>();
                        foreach (var message in messages)
                        {
                            if (!IsMessageExists(message))
                            {
                                templates.Add(new MessageTemplate(message));
                                existsMessage.Add(message);
                            }   
                        }

                        if (templates.Count > 0)
                        {
                            foreach (var view in templates)
                            {
                                _context.Send(status => Stack.Children.Add(view), null);
                                _context.Send(async status => await sc.ScrollToAsync(Stack, ScrollToPosition.End, true), null);
                            } 
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                Thread.Sleep(1000);
            }
        }

        private bool IsMessageExists(Message message)
        {
            bool exists = false;

            if (existsMessage.Count > 0)
            {
                foreach (var m in existsMessage)
                {
                    if (m.id == message.id) exists = true;
                }
            }

            return exists;
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        public async void SendMsg(object sender, EventArgs e)
        {
            try
            {
                Message message = new Message();
                message.receiver = nowUser.id;
                message.text = textMessage.Text;

                HttpContent answer = await Server.SendMessage(message);
                string response = await answer.ReadAsStringAsync();

                if (response != null && response.Contains(nameof(Message)))
                {
                    textMessage.Text = "";
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}