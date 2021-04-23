using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;

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
            await Navigation.PushAsync(new MainPageAdv(nowUser));
        }
    }
}