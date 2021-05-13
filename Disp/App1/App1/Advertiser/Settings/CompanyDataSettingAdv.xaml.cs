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
        public EventHandler<string> nameAdvHandler;
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

        private void ShowLoading()
        {
            actInd.IsVisible = true;
            actInd.IsRunning = true;
            gridRoot.Opacity = 0.3;
            gridRoot.IsEnabled = false;
        }

        private void HideLoading()
        {
            actInd.IsVisible = false;
            actInd.IsRunning = false;
            gridRoot.Opacity = 1;
            gridRoot.IsEnabled = true;
        }

        private void OverrideTitleView(string name, string nameAction, int left, int count)
        {
            NavigationPage.SetTitleView(this, TitleView.OverrideGridView(name, nameAction, left, count, new Command(async () =>
            {
                bool v1 = false, v2 = false, v3 = false, v4 = false, v5 = false, v6 = false, v7 = false, v8 = false, v9 = false;

                if (Name.Text != null && Name.Text.Length != 0) v1 = true;
                if (Address.Text != null && Address.Text.Length != 0) v2 = true;
                if (Ogrn.Text != null && Ogrn.Text.Length == 13) v3 = true;
                if (Inn.Text != null && Inn.Text.Length == 10) v4 = true;
                if (Kpp.Text != null && Kpp.Text.Length == 9) v5 = true;
                if (fioDir.Text != null && fioDir.Text.Length != 0) v6 = true;
                if (posDir.Text != null && posDir.Text.Length != 0) v7 = true;
                if (fioManag.Text != null && fioManag.Text.Length != 0) v8 = true;
                if (posManag.Text != null && posManag.Text.Length != 0) v9 = true;

                if (v1 && v2 && v3 && v4 && v5 && v6 && v7 && v8 && v9)
                {
                    nowUser.company.name = Name.Text;
                    nowUser.company.address = Address.Text;
                    nowUser.company.ogrn = Ogrn.Text;
                    nowUser.company.inn = Inn.Text;
                    nowUser.company.kpp = Kpp.Text;

                    ShowLoading();

                    HttpContent answer = await Server.SaveCompany(nowUser.company);
                    string response = await answer.ReadAsStringAsync();

                    if (response == null || (response != null && !response.Contains(nameof(Company))))
                    {
                        HideLoading();
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
                                Server.SaveAuthObject(nowUser, true);
                                nameAdvHandler?.Invoke(this, nowUser.company.name);

                                await Navigation.PopAsync(true);
                            }
                            else
                            {
                                HideLoading();
                                await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                            }
                        }
                        else
                        {
                            HideLoading();
                            await DisplayAlert("Сообщение", "Не удалось выполнить сохранение! Попробуйте позже", "Закрыть");
                        }
                    }
                }
                else
                {
                    HideLoading();
                    await DisplayAlert("Сообщение", "Не все поля заполнены корректно!", "Закрыть");
                }
            })));
        }
    }
}