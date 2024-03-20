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
    internal class AddQuestionViewModel:ViewModelBase
    {

        private string errorComment;
        public string ErrorComment
        {
            get
            {
                return errorComment;
            }
            set
            {
                errorComment = value;
                ValidateComment();
                OnPropertyChanged();
            }
        }

        private bool showErrorComment;
        public bool ShowErrorComment
        {
            get
            {
                return showErrorComment;
            }
            set
            {
                showErrorComment = value;
                OnPropertyChanged();
            }
        }

        private bool isAddingEnabled;
        public bool IsAddingEnabled
        {
            get
            {
                return isAddingEnabled;
            }
            set
            {
                isAddingEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool ValidateComment()
        {
            if (((App)Application.Current).LoggedInUser.Rank == 2)
            {
                return false;
            }
            else if (((App)Application.Current).LoggedInUser.Score % 100 == 0 && ((App)Application.Current).LoggedInUser.Questions.Count < ((App)Application.Current).LoggedInUser.Score / 100)
            {
                return false;

            }
            else
            {
                return true;

            }
        }


        private string question;
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        private string rightAnswer;
        public string RightAnswer
        {
            get
            {
                return rightAnswer;
            }
            set
            {
                rightAnswer = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer1;
        public string WrongAnswer1
        {
            get
            {
                return wrongAnswer1;
            }
            set
            {
                wrongAnswer1 = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer2;
        public string WrongAnswer2
        {
            get
            {
                return wrongAnswer2;
            }
            set
            {
                wrongAnswer2 = value;
                OnPropertyChanged();
            }
        }

        private string wrongAnswer3;
        public string WrongAnswer3
        {
            get
            {
                return wrongAnswer3;
            }
            set
            {
                wrongAnswer3 = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
        private ConnectingToServerView connectingToServerView;
        public AddQuestionViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect)
        {
            this.triviaService = service;
            this.connectingToServerView = connect;
            this.ErrorComment = "אינך רשאי להוסיף שאלה";
            this.ShowErrorComment = ValidateComment();
            this.IsAddingEnabled = ValidateComment();
            this.AddQuestionCommand = new Command(OnAddQuestion);
        }

        public ICommand AddQuestionCommand { get; set; }

        private async void OnAddQuestion()
        {
            AmericanQuestion quest = new AmericanQuestion();
            quest.QText = question;
            quest.CorrectAnswer = rightAnswer;
            quest.Bad1 = wrongAnswer1;
            quest.Bad2 = wrongAnswer2;
            quest.Bad3 = wrongAnswer3;
            quest.UserId = ((App)Application.Current).LoggedInUser.Id;
            quest.Status = 0;
            await Shell.Current.Navigation.PushModalAsync(connectingToServerView);
            bool a = await this.triviaService.PostNewQuestion(quest);
            await Shell.Current.Navigation.PopModalAsync();

            if (a == true)
            {
                await Shell.Current.DisplayAlert("Add Qustion", "Add Qustion succeed !!", "ok");
                this.ShowErrorComment = ValidateComment();
                this.IsAddingEnabled = ValidateComment();
                this.Question = "";
                this.RightAnswer = "";
                this.WrongAnswer1 = "";
                this.WrongAnswer2 = "";
                this.WrongAnswer3 = "";
            }
            else
            {
                await Shell.Current.DisplayAlert("Add Qustion", "Add Qustion Failed", "ok");
            }

        }

    }
}
