using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Advertiser.Campaign;

namespace App1.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(true)]
    public partial class DriversTemplate: ContentView
    {
        public DriversTemplate(Driver driver)
        {
            InitializeComponent();

            if (driver != null)
            {
                selfPhotoDriver.Source = driver.person.urlSelfPhoto;
                carDriver.Text = String.Format("{0} {1}", driver.car.mark, driver.car.model);
                fioDriver.Text = String.Format("{0} {1} {2}", driver.person.lastName, driver.person.firstName, driver.person.patronymic);
            }
        }
    }
}