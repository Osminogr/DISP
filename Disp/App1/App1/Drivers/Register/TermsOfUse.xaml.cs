using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermsOfUse : ContentPage
    {
        Driver nowUser;
        public TermsOfUse(Driver now)
        {
            InitializeComponent();
            nowUser = now;
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            string content = @"{ ""Driver"": { ""name"" : ";
            content += @"""" + nowUser.name + " " + nowUser.lastname + " " + nowUser.patronymic + @""",";
            content += @" ""sity"" :""" + nowUser.city + @""",";
            content += @" ""phone"":""" + nowUser.phone + @""",";
            content += @" ""is_online"": ""false "",";
            content += @" ""pass_date"" : """ + nowUser.city + @""",";
            content += @" ""balance"" : ""0"",";
            content += @" ""x"" : ""0"",";
            content += @" ""y"" : ""0"" } }";
            Server.Request(content, "post", "drivers");
            await Navigation.PushAsync(new MainPageDr(nowUser));
        }
    }
}