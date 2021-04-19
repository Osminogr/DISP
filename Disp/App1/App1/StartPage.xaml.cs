using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        //public User nowUser;
        string number;
        public StartPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        private async void Button_Click(object sender, EventArgs e)
        {
            if (number == "8005553535")
            {
                Driver dr = new Driver();
                dr.phone = "9999998989";
                dr.name = "Иван";
                dr.lastname = "Иванов";
                dr.patronymic = "Иванович";
                await Navigation.PushAsync(new MainPageDr(dr));
            }
            else if (number != null && number.Length == 10)
            {

                string content = String.Format(@" ""phone"" : ""{0}""", number);
                content = @"{ ""CodeReq"" :{ " + content + "} }";

                Server.Request(content, "post", "gcode");

                await Navigation.PushAsync(new СonfirmNumber(number));
            }
        }

        private void Change_number(object sender, EventArgs e)
        {
            number = NumberEnter.Text;
        }
    }
}