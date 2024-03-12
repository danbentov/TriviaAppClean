using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        public LoginViewModel(TriviaWebAPIProxy service) 
        {
            InServerCall = false;
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
        }

        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            InServerCall=true;
            //await Shell.Current.GoToAsync("connectingToServer");
            User u  = await this.triviaService.LoginAsync("ofer@ofer.com", "1234");
            //await Shell.Current.Navigation.PopModalAsync();
            InServerCall = false;

            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                
                await Shell.Current.DisplayAlert("Login", "Login Faild!", "ok");
            }
            else
            {
                Application.Current.MainPage = new AppShell();
                await Shell.Current.DisplayAlert("Login", $"Login Succeed! for {u.Name} with {u.Questions.Count} Questions", "ok");
            }
        }

        private bool inServerCall;
        public bool InServerCall
        {
            get
            {
                return this.inServerCall;
            }
            set
            {
                this.inServerCall = value;
                OnPropertyChanged("NotInServerCall");
                OnPropertyChanged("InServerCall");
            }
        }

        public bool NotInServerCall
        {
            get
            {
                return !this.InServerCall;
            }
        }
    }
}
