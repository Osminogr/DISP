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
                    adv.date = "20.02.2021";
                    registerDate.Text = String.Format("Начало сотрудничества с DISP: {0}", adv.date);

                    List<Compaign> compaigns = await Server.GetCompaigns(adv.id);
                    int countActive = 0;
                    int countCompleted = 0;
                    int showsAll = 0;
                    DateTime startDate = DateTime.Now;

                    if (compaigns != null && compaigns.Count != 0)
                    {
                        foreach (var compaign in compaigns) compaign.date = DateTime.Now.ToShortDateString();

                        startDate = DateTime.Parse(compaigns[0].date);
                        foreach (var compaign in compaigns)
                        {
                            if (compaign.active) countActive++;
                            else countCompleted++;

                            if (startDate > DateTime.Parse(compaign.date)) startDate = DateTime.Parse(compaign.date);
                        }
                    }
                    compaignsActiveCount.Text = String.Format("Количество активных рекламных компаний: {0}", countActive);
                    compaignsCompletedCount.Text = String.Format("Количество завершенных рекламных компаний: {0}", countCompleted);
                    startDateCompaign.Text = String.Format("Начало участия в рекламной компании: {0}", startDate.Date.ToShortDateString());

                    showsCountAll.Text = String.Format("Количество показов на данный момент всего: {0}", showsAll);
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