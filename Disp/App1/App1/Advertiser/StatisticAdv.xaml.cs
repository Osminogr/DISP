using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using App1.Templates;

namespace App1.Advertiser.Campaign
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticAdv : ContentPage
    {
        Adv adv;
        public StatisticAdv(Adv entity)
        {
            adv = entity;
            InitializeComponent();

            OverrideTitleView("Статистика", 70, -1);

            Request();
        }

        private void OverrideTitleView(string name, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideView(name, left, count));
        }

        public async void Request()
        {
            compaignAct.Children.Clear();
            bool loaded = false;

            List<Compaign> list = await Server.GetCompaigns(adv.id);

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (item.active && item.paid)
                    {
                        compaignAct.Children.Add(new CompaignTemplate(item));
                        loaded = true;
                    }
                }
            }

            if (!loaded)
            {
                Label label = new Label();
                label.Text = "У Вас нет рекламных компаний";
                label.HorizontalOptions = LayoutOptions.Center;
                label.VerticalOptions = LayoutOptions.Center;

                compaignAct.Children.Add(label);
            }
        }
    }
}