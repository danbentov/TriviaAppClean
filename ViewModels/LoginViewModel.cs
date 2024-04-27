//using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class LoginViewModel:ViewModelBase
    {
        private TriviaWebAPIProxy triviaService;
        private SignUpView signUpView;
        private ConnectingToServerView connectingToServerView;
        public LoginViewModel(TriviaWebAPIProxy service, SignUpView signUpView, ConnectingToServerView connectingToServerView) 
        {
            this.triviaService = service;
            this.LoginCommand = new Command(OnLogin);
            this.IntoSignUpCommand = new Command(OnSignUpSelect);
            this.signUpView = signUpView;
            this.connectingToServerView = connectingToServerView;
        }

        public ICommand LoginCommand { get; set; }
        private async void OnLogin()
        {
            //Choose the way you want to blobk the page while indicating a server call

            await Application.Current.MainPage.Navigation.PushAsync(connectingToServerView);
            User u  = await this.triviaService.LoginAsync(Email, Password);
            await Application.Current.MainPage.Navigation.PopAsync();
            
            //Set the application logged in user to be whatever user returned (null or real user)
            ((App)Application.Current).LoggedInUser = u;
            if (u == null)
            {
                await Application.Current.MainPage.DisplayAlert("Login", "Login Faild!", "ok");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login", $"Login Succeed! for {u.Name} with {u.Questions.Count} Questions", "ok");
                Application.Current.MainPage = new AppShell();
            }
        }

        public ICommand IntoSignUpCommand { get; set; }
        private async void OnSignUpSelect()
        {
            await Application.Current.MainPage.Navigation.PushAsync(signUpView);
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
