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
    public class QuestionDetailsViewModel:ViewModelBase
    {

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

        //private int QuestionId; //לא בטוח נכון

        public QuestionDetailsViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect /*, AmericanQuestion question*/)
        {
            this.triviaService = service;
            this.connectingToServerView = connect;
            this.UpdateQuestionCommand = new Command(OnUpdateQuestion);
            //this.QuestionId = question.Id; //לא בטוח נכון
        }

        public ICommand UpdateQuestionCommand { get; set; }

        private async void OnUpdateQuestion()
        {
            AmericanQuestion quest = new AmericanQuestion();
            quest.QText = question;
            quest.CorrectAnswer = rightAnswer;
            quest.Bad1 = wrongAnswer1;
            quest.Bad2 = wrongAnswer2;
            quest.Bad3 = wrongAnswer3;
            // איך מגיעים לסטטוס והאיידי של השאלה והמשתמש שרשם אותה
            //quest.UserId = ((App)Application.Current).LoggedInUser.Id;
            //quest.Status = 0;
            //quest.Id = QuestionId; //לא בטוח נכון
            await Shell.Current.Navigation.PushModalAsync(connectingToServerView);
            bool a = await this.triviaService.UpdateQuestion(quest);
            await Shell.Current.Navigation.PopModalAsync();

            if (a == true)
            {
                await Shell.Current.DisplayAlert("Update Qustion", "Question is updated to the game database successfully !!", "ok");
                this.Question = "";
                this.RightAnswer = "";
                this.WrongAnswer1 = "";
                this.WrongAnswer2 = "";
                this.WrongAnswer3 = "";
            }
            else
            {
                await Shell.Current.DisplayAlert("Update Qustion", "Question has failed to update the game database", "ok");
            }

        }

    }
}
