using eat2fit.Models;
using eat2fit.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace eat2fitApp.ViewModels
{
    class AppendFoodPageVM : INotifyPropertyChanged
    {
		private ObservableCollection<Food> foodList;
		public ObservableCollection<Food> FoodList { get => foodList; set { foodList = value; OnPropertyChanged(); } }
		public object SelectedFood { get; set; }
		public Food Food { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public Command AppendClickedCommand { get; }
		async void AppendClicked()
		{
			if (SelectedFood is Food)
			{
				Food = SelectedFood as Food;
			}
			else
			{ //todo raise exception 
			}
			await Application.Current.MainPage.Navigation.PopModalAsync();

		}

		public async void RefreshPage()
		{
			try
			{
				var mongoService = new MongoService();
				var list = await mongoService.GetAllFoods();
				FoodList = new ObservableCollection<Food>(list);
			}catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}


		public AppendFoodPageVM()
		{
			RefreshPage();
			AppendClickedCommand = new Command(AppendClicked);
		}
	}
}
