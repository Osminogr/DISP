using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using App1;
using App1.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace App1.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                CustomEntry entry = (CustomEntry)e.NewElement;

                if (entry.IsShowBorder)
                {
                    if (this.Control != null)
                    {
                        float[] radii = new float[] { 20, 20, 20, 20, 20, 20, 20, 20 };
                        var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RoundRectShape(radii, null, null));
                        shape.Paint.Color = Xamarin.Forms.Color.Black.ToAndroid();
                        shape.Paint.SetStyle(Paint.Style.Stroke);
                        shape.Paint.StrokeWidth = 3;
                        this.Control.Background = shape;
                        this.Control.SetPadding(10, 0, 0, 0);
                    }
                }
                else
                {
                    if (this.Control != null)
                    {
                        float[] radii = new float[] { 2, 2, 2, 2, 2, 2, 2, 2 };
                        var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RoundRectShape(radii, null, null));
                        shape.Paint.Color = Xamarin.Forms.Color.Transparent.ToAndroid();
                        shape.Paint.SetStyle(Paint.Style.Stroke);
                        shape.Paint.StrokeWidth = 3;
                        this.Control.Background = shape;
                        this.Control.SetPadding(10, 0, 0, 0);
                    }
                }
            }
        }
    }
}