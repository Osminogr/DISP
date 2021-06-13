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
        Driver driverInner;
        Compaign compaignInner;
        public Statistic(Driver driver, Compaign compaign)
        {
            InitializeComponent();

            driverInner = driver;
            compaignInner = compaign;

            OverrideTitleView("Статистика", 70, -1);

            if (driver != null)
            {
                carDriver.Text = String.Format("{0} {1} {2}", driver.car.mark, driver.car.model, driver.car.carNumber);
                fioDriver.Text = String.Format("{0} {1} {2}", driver.person.lastName, driver.person.firstName, driver.person.patronymic);
                selfPhoto.Source = driver.person.urlSelfPhoto;
            }

            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                Adv adv = Server.GetAuthObject().adv;

                if (adv != null)
                {
                    registerDate.Text = String.Format("Начало сотрудничества с DISP: {0}", DateTime.Parse(driverInner.date).ToString("dd.MM.yyyy"));

                    List<Compaign> compaigns = await Server.GetCompaignsByDriver(driverInner.id);
                    int countActive = 0;
                    int countCompleted = 0;
                    
                    if (compaigns != null && compaigns.Count != 0)
                    {
                        foreach (var compaign in compaigns)
                        {
                            if (compaign.active) countActive++;
                            else countCompleted++;
                        }
                    }

                    compaignsActiveCount.Text = String.Format("Количество активных рекламных компаний: {0}", countActive);
                    compaignsCompletedCount.Text = String.Format("Количество завершенных рекламных компаний: {0}", countCompleted);
                    startDateCompaign.Text = String.Format("Начало участия в рекламной компании: {0}", DateTime.Parse(compaignInner.date).ToString("dd.MM.yyyy"));

                    int showsAll = 0;
                    int days = (int)(DateTime.Now - DateTime.Parse(compaignInner.date).AddDays(1)).TotalDays;
                    showsAll = showsAll + days * 12 * 60 * 60 / 15;
                    showsCountAll.Text = String.Format("Количество показов на данный момент: {0}", showsAll);

                    int showsCurrent = 0;
                    int hours = DateTime.Now.Hour - 10;
                    if (DateTime.Now.Hour > 22) hours = 12;
                    showsCurrent = hours * 60 * 60 / 15;
                    showsCountCurrent.Text = String.Format("Количество показов сегодня: {0}", showsCurrent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }
    }
}