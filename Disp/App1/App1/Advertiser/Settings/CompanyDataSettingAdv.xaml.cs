using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Domain;
using App1.Utils;
using System.Net.Http;

namespace App1.Advertiser.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyDataSettingAdv : ContentPage
    {
        Adv nowUser;
        public CompanyDataSettingAdv(Adv now)
        {
            nowUser = now;
            InitializeComponent();

            Name.Text = nowUser.company.name;
            Address.Text = nowUser.company.address;
            Ogrn.Text = nowUser.company.ogrn;
            Inn.Text = nowUser.company.inn;
            Kpp.Text = nowUser.company.kpp;

            fioDir.Text = nowUser.company.director.firstName;
            posDir.Text = nowUser.company.director.position;

            fioManag.Text = nowUser.company.manager.firstName;
            posManag.Text = nowUser.company.manager.position;

            OverrideTitleView("Настройки", "Сохранить", 90, -1);
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                nowUser.company.name = Name.Text;
                nowUser.company.address = Address.Text;
                nowUser.company.ogrn = Ogrn.Text;
                nowUser.company.inn = Inn.Text;
                nowUser.company.kpp = Kpp.Text;

                HttpContent answer = await Server.SaveCompany(nowUser.company);
                string response = await answer.ReadAsStringAsync();

                if (response == null || (response != null && !response.Contains(nameof(Company))))
                {
                    await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                }
                else
                {
                    nowUser.company.director.firstName = fioDir.Text;
                    nowUser.company.director.position = posDir.Text;

                    answer = await Server.SavePerson(nowUser.company.director);
                    response = await answer.ReadAsStringAsync();

                    if (response != null && response.Contains(nameof(Person)))
                    {
                        nowUser.company.manager.firstName = fioManag.Text;
                        nowUser.company.manager.position = posManag.Text;

                        answer = await Server.SavePerson(nowUser.company.manager);
                        response = await answer.ReadAsStringAsync();

                        if (response != null && response.Contains(nameof(Person)))
                        {
                            Server.SaveAuthObject(nowUser, nowUser.isCompany);
                        }
                        else
                        {
                            await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                    }
                }

                await Navigation.PopAsync();
            })));
        }
    }
}