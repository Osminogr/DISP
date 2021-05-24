using Xamarin.Forms;

namespace App1
{
    public class CustomEntry : Entry
    {

        public static readonly BindableProperty showProperty = BindableProperty.Create(nameof(IsShowBorder), typeof(bool), typeof(CustomEntry), false);
        public bool IsShowBorder
        {
            get => (bool)GetValue(CustomEntry.showProperty);
            set => SetValue(CustomEntry.showProperty, value);
        }

        public CustomEntry()
        {
            IsShowBorder = true;
        }
    }
}
