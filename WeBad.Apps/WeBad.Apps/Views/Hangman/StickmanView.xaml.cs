using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeBad.Apps.Views.Hangman
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StickmanView : ContentView
    {
        private SKPaint paint;
        private SKPaint bodyPaint;

        #region Bindable Properties

        public static readonly BindableProperty StepProperty =
            BindableProperty.Create(nameof(Step), typeof(int), typeof(StickmanView), 0);
        public static readonly BindableProperty ConstructionColorProperty =
            BindableProperty.Create(nameof(ConstructionColor), typeof(Color), typeof(StickmanView), Color.Black);
        public static readonly BindableProperty BodyColorProperty =
            BindableProperty.Create(nameof(BodyColor), typeof(Color), typeof(StickmanView), Color.Black);


        public int Step
        {
            get { return (int)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }

        public Color ConstructionColor
        {
            get { return (Color)GetValue(ConstructionColorProperty); }
            set { SetValue(ConstructionColorProperty, value); }
        }

        public Color BodyColor
        {
            get { return (Color)GetValue(BodyColorProperty); }
            set { SetValue(BodyColorProperty, value); }
        }

        #endregion

        public StickmanView()
        {            
            InitializeComponent();

            paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = ConstructionColor.ToSKColor(),
                StrokeWidth = 15
            };

            bodyPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = BodyColor.ToSKColor(),
                StrokeWidth = 10
            };
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName.Equals(nameof(Step)))
            {
                CanvasView.InvalidateSurface();
            }
            else if (propertyName.Equals(nameof(ConstructionColor)))
            {
                paint.Color = ConstructionColor.ToSKColor();
                CanvasView.InvalidateSurface();
            }
            else if (propertyName.Equals(nameof(BodyColor)))
            {
                bodyPaint.Color = BodyColor.ToSKColor();
                CanvasView.InvalidateSurface();
            }

            base.OnPropertyChanged(propertyName);
        }

        private void CanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            int height = info.Height;
            int width = info.Width;
            int startX = 0;
            int startY = 0;

            canvas.Clear();

            //Bottom Line
            canvas.DrawLine(new SKPoint(startX, height - 5), new SKPoint(0.50f * width, height - 5), paint);

            //Vertical Line
            canvas.DrawLine(new SKPoint(0.25f * width, startY), new SKPoint(0.25f * width, height), paint);

            //Top line
            canvas.DrawLine(new SKPoint(0.25f * width, startY + 5), new SKPoint(0.75f * width, startY + 5), paint);

            //Hang line
            canvas.DrawLine(new SKPoint(0.75f * width, startY), new SKPoint(0.75f * width, 0.125f * height), paint);

            //Draw head
            if (Step >= 1)
            {
                canvas.DrawCircle(new SKPoint(0.75f * width, 0.25f * height), 0.125f * height, bodyPaint);
            }
            var h = (3 * height) / 8;
            //Draw body
            if (Step >= 2)
            {
                canvas.DrawLine(new SKPoint(0.75f * width, startY + h), new SKPoint(0.75f * width, h + 0.333f * height), bodyPaint);
            }

            //Draw left hand
            if (Step >= 3)
            {
                canvas.DrawLine(new SKPoint(0.75f * width - 0.125f * width, h + 0.0625f * height + 0.125f * width), new SKPoint(0.75f * width, h + 0.0625f * height), bodyPaint);
            }

            //Draw right hand
            if (Step >= 4)
            {
                canvas.DrawLine(new SKPoint(0.75f * width, h + 0.0625f * height), new SKPoint(0.75f * width + 0.125f * width, h + 0.0625f * height + 0.125f * width), bodyPaint);
            }

            //Draw left leg
            if (Step >= 5)
            {
                canvas.DrawLine(new SKPoint(0.75f * width - 0.125f * width, h + 0.333f * height + 0.125f * width), new SKPoint(0.75f * width, h + 0.333f * height), bodyPaint);
            }

            //Draw right leg
            if (Step >= 6)
            {
                canvas.DrawLine(new SKPoint(0.75f * width, h + 0.333f * height), new SKPoint(0.75f * width + 0.125f * width, h + 0.333f * height + 0.125f * width), bodyPaint);
            }


            Console.WriteLine($"PORUKA: CICA GLISA NACRTAN {DateTime.Now.TimeOfDay.ToString()}");
        }
    }
}