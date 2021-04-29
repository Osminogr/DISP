using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
            content += @"""" + nowUser.person.firstName + " " + nowUser.person.lastName + " " + nowUser.person.patronymic + @""",";
            content += @" ""sity"" :""" + nowUser.person.city + @""",";
            content += @" ""phone"":""" + nowUser.person.phone + @""",";
            content += @" ""is_online"": ""false "",";
            content += @" ""pass_date"" : """ + nowUser.person.city + @""",";
            content += @" ""balance"" : ""0"",";
            content += @" ""x"" : ""0"",";
            content += @" ""y"" : ""0"" } }";
            Server.Request(content, "post", "drivers");
            await Navigation.PushAsync(new MainPageDr(nowUser));
        }
    }
}