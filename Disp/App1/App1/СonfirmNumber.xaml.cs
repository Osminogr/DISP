using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class СonfirmNumber : ContentPage
    {
        string code;
        string number;

        public string LabelText
        {
            get { return LabelText; }
            set
            {
                LabelText = value;
                OnPropertyChanged(nameof(LabelText));
            }
        }

        private void ShowLoading(bool show)
        {
            actInd.IsRunning = show;
            actInd.IsVisible = show;
            gridRoot.IsEnabled = !show;
        }

        public СonfirmNumber(string phone)
        {
            number = phone;
            InitializeComponent();
            BindingContext = this;

            Label1.Text = "+7 (" + number[0] + number[1] + number[2] + ") " + number[3] + number[4] + number[5] + "-" + number[6] + number[7] + "-" + number[8] + number[9];
            NavigationPage.SetHasNavigationBar(this, false);
        }

        public void CodeReq(object sender, EventArgs e)
        {
            ShowLoading(true);
            Server.CreateCodeReq(number);
            reCode.IsEnabled = false;
            reCode.Text = "Код отправлен повторно";
            ShowLoading(false);
        }

        public async void ChangeNumber(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StartPage());
        }

        protected void Change_code(object sender, EventArgs e)
        {
            this.code = CodeEnter.Text;
        }

        private async void Complete(object sender, EventArgs e)
        {
            if (code != null && code.Length == 4)
            {
                try
                {
                    Entity entity = await Server.GetEntity(code, number);

                    if (entity != null)
                    {
                        if (entity.GetType() == typeof(Adv))
                        {
                            Server.SaveAuthObject(entity, true);
                            await Navigation.PushAsync(new MainPageAdv((Adv)entity));
                        }
                        else
                        {
                            Server.SaveAuthObject(entity, false);
                            await Navigation.PushAsync(new MainPageDr((Driver)entity));
                        }
                    }
                    else
                    {
                        await Navigation.PushAsync(new Register(number));
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Сообщение", "Не удалось выполнить авторизацию! Попробуйте позже.", "Закрыть");
                    await Navigation.PushAsync(new StartPage());
                }
            }
        }
    }
}