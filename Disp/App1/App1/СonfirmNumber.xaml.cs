using System;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class СonfirmNumber : ContentPage
    {
        string code;
        string number;

        public string LabelText
        {
            get { return LabelText; }
            set
            {
                LabelText = value;
                OnPropertyChanged(nameof(LabelText)); // Notify that there was a change on this property
            }
        }
        public СonfirmNumber(string phone)
        {
            number = phone;
            InitializeComponent();
            BindingContext = this;
            //nowUser = now;
            //number = nowUser.telephon_number.ToString();
            //LabelText = number;

            Label1.Text = "+7 (" + number[0] + number[1] + number[2] + ") " + number[3] + number[4] + number[5] + "-" + number[6] + number[7] + "-" + number[8] + number[9];
            NavigationPage.SetHasNavigationBar(this, false);
        }
        public void CodeReq(object sender, EventArgs e)
        {

            string content = String.Format(@" ""phone"" : ""{0}""", number);
            content = @"{ ""CodeReq"" :{ " + content + "} }";
            Server.Request(content, "post", "gcode");
            reCode.IsEnabled = false;
            reCode.Text = "Код отправлен повторно";


        }
        public async void ChangeNumber(object sender, EventArgs e)
        { 
            await Navigation.PushAsync(new StartPage());

        }
        protected void Change_code(object sender, EventArgs e)
        {
            this.code = CodeEnter.Text;

        }
        private async void Button_Click(object sender, EventArgs e)
        {
            if (code != null && code.Length == 4)
            {
                HttpClient client = new HttpClient();

                var answer = await client.GetAsync(Server.url + "gcode/?code=" + code + "&phone=" + number);
                var responseBody = await answer.Content.ReadAsStringAsync();
                if (responseBody.Length == 13)
                {
                    var req = await client.GetAsync(Server.url + "ad/" + number);
                    var resp = await req.Content.ReadAsStringAsync();
                    if (resp.Length > 20)
                    {
                        resp = resp.Substring(16, resp.Length - 18);
                        var dict = resp
                            .Split(',')
                            .Select(part => part.Split(':'))
                            .Where(part => part.Length == 2)
                            .ToDictionary(sp => sp[0], sp => sp[1]);
                        foreach (string keyValue in dict.Keys)
                        {
                            Console.WriteLine(keyValue);
                        }
                        if (dict.Count > 2)
                        {
                            Adv adv = new Adv
                            {
                                fioDir = dict["\"name\""].Trim('\"'),
                                city = dict["\"sity\""].Trim('\"'),
                                phone = dict["\"phone\""].Trim('\"'),
                                address = dict["\"pass_date\""].Trim('\"'),
                                name = dict["\"company\""].Trim('\"')
                            };
                            await Navigation.PushAsync(new MainPageAdv(adv));
                        }
                    }

                    req = await client.GetAsync(Server.url + "drivers/" + number);
                    resp = await req.Content.ReadAsStringAsync();
                    resp = resp.Substring(12, resp.Length - 14);
                    var dictionary = resp
                        .Split(',')
                        .Select(part => part.Split(':'))
                        .Where(part => part.Length == 2)
                        .ToDictionary(sp => sp[0], sp => sp[1]);
                    foreach (string keyValue in dictionary.Keys)
                    {
                        Console.WriteLine(keyValue);
                    }
                    if (dictionary.Count > 2)
                    {
                        Driver drv = new Driver
                        {
                            name = dictionary["\"name\""],
                            city = dictionary["\"sity\""],
                            phone = dictionary["\"phone\""]
                        };
                        await Navigation.PushAsync(new MainPageDr(drv));
                    }
                }
                else
                {
                    await Navigation.PushAsync(new DriverAdv(number));
                }
            }
        }
    }
}