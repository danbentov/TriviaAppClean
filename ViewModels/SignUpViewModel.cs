using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    internal class SignUpViewModel:ViewModelBase
    {

        private string email;
        public string Email
        {
            get 
            { 
                return email; 
            } 
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string passwordAprove;
        public string PasswordAprove
        {
            get
            {
                return passwordAprove;
            }
            set
            {
                passwordAprove = value;
                OnPropertyChanged();
            }
        }

        private int id;
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }



        private TriviaWebAPIProxy triviaService;
        public SignUpViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            this.SignUpCommand = new Command(OnSignUp);
        }

        public ICommand SignUpCommand { get; set; }
        // בפעולת רג'יסטר יש לזכור לדאוג לאפס את הניקוד והדרגה לפי המבוקש.
        private async void OnSignUp()
        {
            User u = new User();
            u.Email = Email;
            u.Password = Password;
            u.Name = Name;
            u.Id = Id;
            await Shell.Current.GoToAsync("connectingToServer");
            bool a = await this.triviaService.RegisterUser(u);
            await Shell.Current.Navigation.PopModalAsync();

            if (a== true)
            {
                ((App)Application.Current).LoggedInUser = u;
                await Shell.Current.DisplayAlert("Sign Up", $"Sign Up succeed !! for {u.Name}", "ok");
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await Shell.Current.DisplayAlert("Sign Up", "Sign Up Failed", "ok");
            }

        }

    }
}
