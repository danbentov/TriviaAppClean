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
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
            this.IntoSignUpCommand = new Command(OnSignUpSelect);
        }

        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call
            
            await Shell.Current.GoToAsync("connectingToServer");
            User u  = await this.triviaService.LoginAsync(Email, Password);
            await Shell.Current.Navigation.PopModalAsync();
            
            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                await Shell.Current.DisplayAlert("Login", "Login Faild!", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Login", $"Login Succeed! for {u.Name} with {u.Questions.Count} Questions", "ok");
                Application.Current.MainPage = new AppShell();
            }
        }

        public ICommand IntoSignUpCommand { get; set; }
        private async void OnSignUpSelect()
        {
            await Shell.Current.GoToAsync("signUp");
        }

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
                OnPropertyChanged();
            }
        }


    }
}
