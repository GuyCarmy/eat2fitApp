using eat2fit.Models;
using eat2fit.Services;
using eat2fitApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eat2fitApp.ViewModels
{
    class AddMealPageVM : INotifyPropertyChanged
    {
		//todo: get the customer for this meal.
		//todo: upload the meal to the customer diet when its ends.
		private Meal meal = new Meal();
		Customer customer;
		public string Amount { get; set; }
		public string Hrs { get; set; }
		public string Mins { get; set; }
		private ObservableCollection<Food> foodList;
		public ObservableCollection<Food> FoodList { get => foodList; set { foodList = value; OnPropertyChanged(); } }
		private ObservableCollection<Food> mealFoodList;
		public ObservableCollection<Food> MealFoodList { get => mealFoodList; set { mealFoodList = value; OnPropertyChanged(); } }
		public object SelectedFood { get; set; }

		public void SetCustomer(Customer c)
		{
			if (c != null)
			{
				customer = c;
			}
		}

		async void RefreshPage()
		{
			try
			{
				var mongoService = new MongoService();
				var list = await mongoService.GetAllFoods();
				FoodList = new ObservableCollection<Food>(list);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}


		public Command AddFoodClickedCommand { get; }
		void AddFoodClicked()
		{
			if (SelectedFood == null || Amount == null)
			{
				System.Diagnostics.Debug.WriteLine("please insert amount and select food");
				//todo: display alert please fill all fields.
			}
			else
			{
				if (SelectedFood is Food) {
					var f = SelectedFood as Food;
					try
					{
						f.Amount = Convert.ToInt32(Amount);
						meal.Foods.Add(f);
						MealFoodList=(new ObservableCollection<Food>(meal.Foods));
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
					}
				}
			}
		}

		public Command FinishMealClickedCommand { get; }
		async void FinishMealClicked()
		{
			if (Hrs == null | Mins == null)
			{
				System.Diagnostics.Debug.WriteLine("please enter all fields");
				//todo display alert
			}
			else
			{
				try
				{
					var mongoService = new MongoService();
					int hrs = Convert.ToInt32(Hrs);
					int mins = Convert.ToInt32(Mins);
					meal.Time = hrs * 60 + mins;
					customer.AddEatedMeal(meal);
					await mongoService.EditCustomer(customer);
					await Application.Current.MainPage.Navigation.PopAsync();
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
			}
		}

		public AddMealPageVM()
		{
			RefreshPage();
			AddFoodClickedCommand = new Command(AddFoodClicked);
			FinishMealClickedCommand = new Command(FinishMealClicked);
		}
    }

}
