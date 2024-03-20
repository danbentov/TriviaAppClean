using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;
using TriviaAppClean.Views;

namespace TriviaAppClean.ViewModels
{
    public class SignUpViewModel:ViewModelBase
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

        private string passwordApprove;
        public string PasswordApprove
        {
            get
            {
                return passwordApprove;
            }
            set
            {
                passwordApprove = value;
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
        private ConnectingToServerView connectingToServerView;
        public SignUpViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect)
        {
            this.triviaService = service;
            this.connectingToServerView = connect;
            this.SignUpCommand = new Command(OnSignUp);
        }

        public ICommand SignUpCommand { get; set; }

        private async void OnSignUp()
        {
            User u = new User();
            u.Email = Email;
            u.Password = Password;
            u.Name = Name;
            u.Score = 0;
            u.Rank = 0;
            await Application.Current.MainPage.Navigation.PushModalAsync(connectingToServerView);
            bool a = await this.triviaService.RegisterUser(u);
            await Application.Current.MainPage.Navigation.PopModalAsync();

            if (a== true)
            {
                await Application.Current.MainPage.DisplayAlert("Sign Up", $"Sign Up succeed !! for {u.Name}", "ok");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Sign Up", "Sign Up Failed", "ok");
            }

        }

    }
}
