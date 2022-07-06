using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace PickerDemo {
	public class Student {
		public String name;
		public int grade;
		public Student(String n, int g) {
			name = n;
			grade = g;
		}
		public override string ToString() {
			return String.Format("{0} ({1})", name, grade);
		}
	}

	public class School
	{
		public string Name { get; set; }
		public string WebsiteURL { get; set; }
		public override string ToString()
		{
			return Name;
		}
	}

	public class MainPage : ContentPage {
		Label sizeLabel = new Label { Text = "Medium" };
		Label colorLabel = new Label { Text = "Blue" };
		Label studentLabel = new Label { Text = "n/a" };
		Picker sizePicker;
		Picker colorPicker;
		Picker studentPicker;
		Picker collegePicker;
		WebView webview;
		public ObservableCollection<School> schoolList = new ObservableCollection<School> {
				new School { Name="Miami", WebsiteURL="https://www.miamioh.edu"},
				new School { Name="Ohio State", WebsiteURL="https://www.osu.edu"},
				new School { Name="U. Cincinnati", WebsiteURL="https://www.uc.edu"},
				new School { Name="Ohio", WebsiteURL="https://www.ohio.edu"},
		};

		public MainPage() {
			StackLayout panel = new StackLayout();

			//sizePicker = new Picker();
			//sizePicker.Items.Add("X Small");
			//sizePicker.Items.Add("Small");
			//sizePicker.Items.Add("Medium");
			//sizePicker.Items.Add("Large");
			//sizePicker.Items.Add("X Large");
			//sizePicker.SelectedIndex = 2;
			//sizePicker.SelectedIndexChanged += SizePicker_SelectedIndexChanged;

			//colorPicker = new Picker();
			//colorPicker.Items.Add("Red");
			//colorPicker.Items.Add("Green");
			//colorPicker.Items.Add("Blue");
			//colorPicker.SelectedIndex = 2;
			//colorPicker.SelectedIndexChanged += ColorPicker_SelectedIndexChanged;

			//studentPicker = new Picker();
			//List<Student> students = new List<Student>();
			//students.Add(new Student("Maria", 6));
			//students.Add(new Student("Alberto", 5));
			//students.Add(new Student("Greta", 3));
			//studentPicker.ItemsSource = students;
			//studentPicker.SelectedIndex = 0;
			//studentPicker.SelectedIndexChanged += StudentPicker_SelectedIndexChanged;

			//StackLayout sizePanel = new StackLayout();
			//sizePanel.Children.Add(sizeLabel);
			//sizePanel.Children.Add(sizePicker);

			//StackLayout colorPanel = new StackLayout();
			//colorPanel.Children.Add(colorLabel);
			//colorPanel.Children.Add(colorPicker);

			//StackLayout studentPanel = new StackLayout();
			//studentPanel.Children.Add(studentLabel);
			//studentPanel.Children.Add(studentPicker);

			//Frame sizeFrame = new Frame();
			//sizeFrame.Content = sizePanel;
			//sizeFrame.BorderColor = Color.Black;

			//Frame colorFrame = new Frame();
			//colorFrame.Content = colorPanel;
			//colorFrame.BorderColor = Color.Black;

			//Frame studentFrame = new Frame();
			//studentFrame.Content = studentPanel;
			//studentFrame.BorderColor = Color.Black;

			//panel.Children.Add(sizeFrame);
			//panel.Children.Add(colorFrame);
			//panel.Children.Add(studentFrame);

			//collegePicker = new Picker { HeightRequest = 50, WidthRequest = 250};
			//collegePicker.ItemsSource = schoolList;
			//collegePicker.SelectedIndex = 0;
			//collegePicker.SelectedIndexChanged += CollegePicker_SelectedIndexChanged;
			//webview = new WebView
			//{
			//	Source = new UrlWebViewSource
			//	{
			//		Url = schoolList[0].WebsiteURL
			//	},
			//	VerticalOptions = LayoutOptions.FillAndExpand,
			//	HeightRequest = 300
			//};
			//panel.Children.Add(collegePicker);
			//panel.Children.Add(webview);

			StackLayout vpanel = new StackLayout();
			StackLayout hpanel = new StackLayout
			{

				Orientation = StackOrientation.Horizontal
			};
			Grid grid = new Grid
			{
				RowDefinitions = {
new RowDefinition { Height = GridLength.Auto },
new RowDefinition { Height = GridLength.Auto },
new RowDefinition { Height = GridLength.Auto },
new RowDefinition { Height = GridLength.Auto }
},
				ColumnDefinitions = {
new ColumnDefinition { Width = GridLength.Auto },
new ColumnDefinition { Width = GridLength.Auto },
new ColumnDefinition { Width = GridLength.Auto }
}
			};
			// Some of the grid cells will be empty.
			grid.Children.Add(new Button { Text = "A" }, 0, 0);
			grid.Children.Add(new Button { Text = "B" }, 0, 1);
			grid.Children.Add(new Button { Text = "C" }, 0, 2);
			grid.Children.Add(new Button { Text = "D" }, 1, 0);
			grid.Children.Add(new Button { Text = "E" }, 2, 0);
			grid.Children.Add(new Button { Text = "F" }, 1, 2, 1, 3);
			hpanel.Children.Add(new Button { Text = "G" });
			hpanel.Children.Add(new Button { Text = "H" });
			hpanel.Children.Add(new Button { Text = "I" });
			vpanel.Children.Add(hpanel);
			vpanel.Children.Add(new Frame { Content = new Label { Text = "F" } });
			vpanel.Children.Add(grid);
			this.Content = vpanel;
		//Content = panel;
		}
		private void StudentPicker_SelectedIndexChanged(object sender, EventArgs e) {
			Picker picker = (Picker)sender;
			Student whichStudent = (Student)picker.SelectedItem;
			studentLabel.Text = whichStudent.ToString();
		}
		private void SizePicker_SelectedIndexChanged(object sender, EventArgs e) {
			sizeLabel.Text = (String)sizePicker.SelectedItem;
		}
		private void ColorPicker_SelectedIndexChanged(object sender, EventArgs e) {
			colorLabel.Text = (String)colorPicker.SelectedItem;
		}

		private void CollegePicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			webview.Source = new UrlWebViewSource { Url = ((School)collegePicker.SelectedItem).WebsiteURL };
		}
	}
}
