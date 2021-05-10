using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;

namespace App1.Advertiser.Campaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistic : ContentPage
    {
        public Statistic(Driver driver)
        {
            InitializeComponent();

            OverrideTitleView("Статистика", 70, -1);

            if (driver != null)
            {
                carDriver.Text = String.Format("{0} {1} {2}", driver.car.mark, driver.car.model, driver.car.regNumberCar);
                fioDriver.Text = String.Format("{0} {1} {2}", driver.person.lastName, driver.person.firstName, driver.person.patronymic);
            }
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}