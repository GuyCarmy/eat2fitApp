﻿using eat2fitApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eat2fitApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AppendFoodPage : ContentPage
	{
		public AppendFoodPage ()
		{
			InitializeComponent ();
			BindingContext = new AppendFoodPageVM();
		}
	}
}