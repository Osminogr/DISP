using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.Advertiser
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPayMethod : ContentPage
    {
        Adv nowUser;
        public EditPayMethod(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            OverrideTitleView("Изменить", "Готово", 90, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(() => {
                Navigation.PushAsync(new EditPayMethod(nowUser));
            })));
        }
    }
}