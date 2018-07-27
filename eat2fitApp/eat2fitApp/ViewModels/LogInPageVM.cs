using eat2fit.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace eat2fitApp.ViewModels
{
    public class LogInPageVM
    {
		bool isBusy = false;
		public bool IsBusy { get => isBusy; set { isBusy = value; OnConnectClickedCommand.ChangeCanExecute(); } }
		public string Name { get; set; }
		public string Password { get; set; } //todo - encrypt passwords

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
				var mongoService = new MongoService();
				var customer = await mongoService.GetCustomerIfPasswordVerified(Name, Password);
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
					//todo: open the main page as maine page with a new navigation instance so there won't be a back (arrow) button
					await Application.Current.MainPage.Navigation.PushAsync(mainPage);
					IsBusy = false;
				}
			}
		}

		public LogInPageVM()
		{
			OnConnectClickedCommand = new Command(OnConnectClicked,() => !isBusy);
		}
    }
}
