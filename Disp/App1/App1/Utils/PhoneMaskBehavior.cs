using System;
using Xamarin.Forms;

namespace App1.Utils
{
    public class PhoneMaskBehavior : Behavior<Entry>
    {
        public static PhoneMaskBehavior Instance = new PhoneMaskBehavior();

        ///  
        /// Attaches when the page is first created.  
        ///   

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        ///  
        /// Detaches when the page is destroyed.  
        ///   

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                // If the new value is longer than the old value, the user is  
                if (args.OldTextValue != null && args.NewTextValue.Length < args.OldTextValue.Length)
                    return;

                var value = args.NewTextValue;

                if (value.Length == 1)
                {
                    ((Entry)sender).Text = String.Format("+7 ({0}", ((Entry)sender).Text);
                    return;
                }

                if (value.Length == 7)
                {
                    ((Entry)sender).Text += ") ";
                    return;
                }

                if (value.Length == 12 || value.Length == 15)
                {
                    ((Entry)sender).Text += "-";
                    return;
                }

                ((Entry)sender).Text = args.NewTextValue;
            }
        }
    }
}
