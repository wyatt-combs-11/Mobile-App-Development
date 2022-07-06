using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;

namespace Graphics {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
		}

		void OnCanvas1ViewPaintSurface(object sender, SKPaintSurfaceEventArgs args) {
			SKImageInfo info = args.Info;
			SKSurface surface = args.Surface;
			SKCanvas canvas = surface.Canvas;

			canvas.Clear();
			SKPaint paint = new SKPaint {
				Style = SKPaintStyle.Stroke,
				Color = Color.Red.ToSKColor(),
				StrokeWidth = 3,
				IsStroke = false
			};

			int redNum = Int32.Parse(Red.Text);
			int blueNum = Int32.Parse(Blue.Text);
			float perRed = (redNum / (1.0f * (redNum+blueNum))) * info.Height;
			float perBlue = info.Height - perRed;

			canvas.DrawRect(info.Width * 0.2f, info.Height - perRed, info.Width * 0.2f, perRed, paint);
			paint.Color = Color.Blue.ToSKColor();
			canvas.DrawRect(info.Width * 0.6f, info.Height - perBlue, info.Width * 0.2f, perBlue, paint);
		}

        private void OnEntryChanged(object sender, TextChangedEventArgs e)
        {
			view1.InvalidateSurface();
        }
    }
}
