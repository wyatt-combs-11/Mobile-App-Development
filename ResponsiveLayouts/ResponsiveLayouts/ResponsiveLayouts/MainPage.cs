using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ResponsiveLayouts {
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public View land, horz;

		public MainPage()
		{
			InitializeComponent();
            SizeChanged += MainPage_SizeChanged;

			Grid grid = new Grid
			{
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Padding = 50,
				RowDefinitions =
				{
					new RowDefinition {Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition {Height = new GridLength(1, GridUnitType.Star) },
					new RowDefinition {Height = new GridLength(1, GridUnitType.Star) }
				},
				ColumnDefinitions =
				{
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
				}
			};
			grid.Children.Add(new Button { Text = "A", WidthRequest = 30}, 0, 0);
			grid.Children.Add(new Button { Text = "B", WidthRequest = 30 }, 0, 1);
			grid.Children.Add(new Button { Text = "C", WidthRequest = 30 }, 0, 2);
			grid.Children.Add(new Button { Text = "D", WidthRequest = 30 }, 1, 0);
			grid.Children.Add(new Button { Text = "E", WidthRequest = 30 }, 1, 1);
			grid.Children.Add(new Button { Text = "F", WidthRequest = 30 }, 1, 2);

			land = grid;

			StackLayout st = new StackLayout
			{
				HeightRequest = 400,
				WidthRequest = 200,
				Padding = 50
			};
			st.Children.Add(new Button { Text = "A", WidthRequest=30, HeightRequest = 20});
			st.Children.Add(new Button { Text = "B", WidthRequest = 30, HeightRequest = 20 });
			st.Children.Add(new Button { Text = "C", WidthRequest = 30, HeightRequest = 20 });
			st.Children.Add(new Button { Text = "D", WidthRequest = 30, HeightRequest = 20 });
			st.Children.Add(new Button { Text = "E", WidthRequest = 30, HeightRequest = 20 });
			st.Children.Add(new Button { Text = "F", WidthRequest = 30, HeightRequest = 20 });

			horz = st;
		}

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
			bool isNowLandscape = Width > Height;
			if (isNowLandscape)
			{
				Content = land;
			}
			else
			{
				Content = horz;
			}
		}
	}
}
