using eat2fit.Models;
using eat2fit.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace eat2fitApp.ViewModels
{
    public class LogInPageVM
    {
		//todo: work on performance and add a splash screen. 
		//for now it gets all customers while the user is typing instead of getting the specific customer after he clicks connect.
		//getting all customers is not scalable and it also a security issue. need to think of other solution.
		//also part of the problem might not be that. slow startup of the app during to non relevant things running in the background
		bool isBusy = false;
		public bool IsBusy { get => isBusy; set { isBusy = value; OnConnectClickedCommand.ChangeCanExecute(); } }
		public string Name { get; set; }
		public string Password { get; set; } //todo - encrypt passwords
		MongoService mongoService;
		List<Customer> Customers;


		public Command OnConnectClickedCommand { get; }
		async void OnConnectClicked()
		{
			IsBusy = true;
			System.Diagnostics.Debug.WriteLine("button clicked"); //todo delete
			if (Name == null || Password == null)
			{
				System.Diagnostics.Debug.WriteLine("please enter all fields"); //todo make display alert
				IsBusy = false;
			}
			else
			{
				//var customer = await mongoService.GetCustomerIfPasswordVerified(Name, Password);
				var customer = Customers.Find(x => x.Name == Name && x.Password== Password);
				if (customer == null)
				{
					System.Diagnostics.Debug.WriteLine("login failed"); //todo make display alert
					IsBusy = false;
				}
				else
				{
					var mainPage = new MainPage();
					var vm = new MainPageVM();
					vm.SetCustomer(customer);
					mainPage.BindingContext = vm;
					//todo: open the main page as main page with a new navigation instance so there won't be a back (arrow) button
					// or make the MainPage a welcome page and the log in as a Modal
					await Application.Current.MainPage.Navigation.PushAsync(mainPage);
					IsBusy = false;
				}
			}
		}

		async void GetAllCustomers()
		{
			mongoService = new MongoService();
			Customers = await mongoService.GetAllCustomers();
		}

		public LogInPageVM()
		{
			OnConnectClickedCommand = new Command(OnConnectClicked,() => !isBusy);
			GetAllCustomers();


		}
	}
}
