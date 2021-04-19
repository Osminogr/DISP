using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.RegisterAdvPh
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermOfUse : ContentPage
    {
        Adv nowUser;
        public TermOfUse(Adv now)
        {
            nowUser = now;
            InitializeComponent();
        }

        private async void Button_Click(object sender, EventArgs e)
        {
            string content = @"{ ""Advertiser"": { ""name"" : ";
            content += @" """ + nowUser.fioDir + @""",";
            content += @" ""sity"" :""" + nowUser.city + @""",";
            content += @" ""phone"" :""" + nowUser.phone + @""",";
            content += @" ""is_online"": ""false"",";
            content += @" ""pass_date"" : """ + nowUser.address + " " + nowUser.BankAddress + " " + nowUser.BankName + " " + nowUser.BIK + " " + nowUser.BunkNumberK + " " + nowUser.BunkNumberR + " " + nowUser.city + " " + nowUser.contPost + " " + nowUser.dirPost + " " + nowUser.fioCont + " " + nowUser.inn + " " + nowUser.kpp + " " + nowUser.ogrn + @""",";
            content += @" ""balance"" : ""0"",";
            content += @" ""tarif"" : ""0"",";
            content += @" ""company"" : """ + nowUser.name + @"""} }";
            Server.Request(content, "post", "ad");

            await Navigation.PushAsync(new MainPageAdv(nowUser));
        }
    }
}