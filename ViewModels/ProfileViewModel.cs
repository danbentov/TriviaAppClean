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
    public class ProfileViewModel:ViewModelBase
    {

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

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
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

        private ObservableCollection<AmericanQuestion> questions;
        public ObservableCollection<AmericanQuestion> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
                OnPropertyChanged();
            }
        }
        private int rank;
        private int score;

        private TriviaWebAPIProxy triviaService;
        public ProfileViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            this.UpdateProfileCommand = new Command(OnUpdateProfile);
            Questions = new ObservableCollection<AmericanQuestion>();
            List<AmericanQuestion> list = ((App)Application.Current).LoggedInUser.Questions;
            foreach (AmericanQuestion question in list)
            {
                Questions.Add(question);
            }
            rank = ((App)Application.Current).LoggedInUser.Rank;
            score = ((App)Application.Current).LoggedInUser.Score;
            this.Email = ((App)Application.Current).LoggedInUser.Email;
            this.Password = ((App)Application.Current).LoggedInUser.Password;
            this.Name = ((App)Application.Current).LoggedInUser.Name;
        }

        public ICommand UpdateProfileCommand { get; set; }
        private async void OnUpdateProfile()
        {
            User neuser = ((App)Application.Current).LoggedInUser;
            neuser.Email = this.Email;
            neuser.Name = this.Name;
            neuser.Password = this.Password;
            await Shell.Current.GoToAsync("connectingToServer");
            bool a = await this.triviaService.UpdateUser(neuser);
            await Shell.Current.Navigation.PopModalAsync();

            if (a == true)
            {
                ((App)Application.Current).LoggedInUser = neuser;
                await Shell.Current.DisplayAlert("Update", $"Update succeed !! for {neuser.Name}", "ok");
            }
            else
            {
                await Shell.Current.DisplayAlert("Update", "Update Failed", "ok");
            }
        }



    }
}
