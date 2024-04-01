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
    public class AllUsersViewModel : ViewModelBase
    {

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                return this.users;
            }
            set
            {
                this.users = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
        public AllUsersViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            Users = new ObservableCollection<User>();
            ReadUser();

        }

        public async void ReadUser()
        {
            List<User> list = await triviaService.GetAllUsers();
            this.Users = new ObservableCollection<User>(list);
        }



        public ICommand SearchByName {  get; set; }

        private string nameText;
        public string NameText
        {
            get
            {
                return this.nameText;
            }
            set
            {
                nameText = value;
                OnPropertyChanged();
            }
        }



    }
}
