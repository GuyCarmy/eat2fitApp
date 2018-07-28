using eat2fit.Models;
using eat2fitApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace eat2fitApp.ViewModels
{
    class AddMealPageVM : INotifyPropertyChanged
    {
		public string Amount { get; set; }
		public string Hrs { get; set; }
		public string Mins { get; set; }
		private ObservableCollection<Food> foodList;
		public ObservableCollection<Food> FoodList { get => foodList; set { foodList = value; OnPropertyChanged(); } } 


		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}


		public Command AddFoodClickedCommand { get; }
		async void AddFoodClicked()
		{
			var p = new AppendFoodPage();
			var vm = p.BindingContext as AppendFoodPageVM;
			
			await Application.Current.MainPage.Navigation.PushModalAsync(new AppendFoodPage());
			//FoodList.Add(vm.Food);
			//todo: get the food from the vm AFTER it poped. 
		}

		public AddMealPageVM()
		{
			AddFoodClickedCommand = new Command(AddFoodClicked);
		}
    }

}
