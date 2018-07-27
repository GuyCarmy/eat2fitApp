﻿using eat2fit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace eat2fitApp.ViewModels
{
    class MainPageVM : INotifyPropertyChanged
    {
		private Customer customer;
		private ObservableCollection<Meal> mealList;
		public ObservableCollection<Meal> MealList { get => mealList; set { mealList = value; OnPropertyChanged(); } }

		public event PropertyChangedEventHandler PropertyChanged;
		void OnPropertyChanged([CallerMemberName] string name = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public void SetCustomer(Customer c)
		{
			if (c != null)
			{
				customer = c;
			}
		}
		
		public Command MyDietClickedCommand { get; }
		async void MyDietClicked()
		{
			System.Diagnostics.Debug.WriteLine("My Diet Clicked");
			MealList = new ObservableCollection<Meal>(customer.SuggestedDiet);
		}

		public Command MyEatingLogClickedCommand { get; }
		async void MyEatingLogClicked()
		{
			MealList = new ObservableCollection<Meal>(customer.EatedDiet);
		}

		public MainPageVM()
		{
			MyDietClickedCommand = new Command(MyDietClicked);
			MyEatingLogClickedCommand = new Command(MyEatingLogClicked);
		}

	}
}