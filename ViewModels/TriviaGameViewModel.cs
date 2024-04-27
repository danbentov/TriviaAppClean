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
    public class TriviaGameViewModel : ViewModelBase
    {
        private AmericanQuestion currentQuestion;
        public AmericanQuestion CurrentQuestion
        {
            get
            {
                return currentQuestion;
            }
            set
            {
                currentQuestion = value;
                OnPropertyChanged();
            }
        }

        private TriviaWebAPIProxy triviaService;
        private ConnectingToServerView connectingToServerView;

        public ICommand onFirstAnswerCommand {  get; set; }
        public ICommand onSecondAnswerCommand { get; set; }
        public ICommand onThirdAnswerCommand { get; set; }
        public ICommand onFourthAnswerCommand { get; set; }

        private string qT;
        public string QT
        {
            get
            {
                return qT;
            }
            set
            {
                qT = value;
                OnPropertyChanged();
            }
        }

        private string firstAns;
        public string FirstAns
        {
            get
            {
                return firstAns;
            }
            set
            {
                firstAns = value;
                OnPropertyChanged();
            }
        }
        private string secondAns;
        public string SecondAns
        {
            get
            {
                return secondAns;
            }
            set
            {
                secondAns = value;
                OnPropertyChanged();
            }
        }
        private string thirdAns;
        public string ThirdAns
        {
            get
            {
                return thirdAns;
            }
            set
            {
                thirdAns = value;
                OnPropertyChanged();
            }
        }
        private string fourthAns;
        public string FourthAns
        {
            get
            {
                return fourthAns;
            }
            set
            {
                fourthAns = value;
                OnPropertyChanged();
            }
        }

        private int score;

        public ICommand stopCommand { get; set; }
        private async void onStop()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public TriviaGameViewModel(TriviaWebAPIProxy service, ConnectingToServerView connect)
        {
            triviaService = service;
            connectingToServerView = connect;
            GetQ();
            this.onFirstAnswerCommand = new Command<string>(onSelectingAnswer);
            this.onSecondAnswerCommand = new Command<string>(onSelectingAnswer);
            this.onThirdAnswerCommand = new Command<string>(onSelectingAnswer);
            this.onFourthAnswerCommand = new Command<string>(onSelectingAnswer);
            this.stopCommand = new Command(onStop);
            score = ((App)Application.Current).LoggedInUser.Score;
        }

        public async void GetQ()
        {
            //await Shell.Current.Navigation.PushModalAsync(connectingToServerView);
            this.CurrentQuestion = await triviaService.GetRandomQuestion();
            this.QT = CurrentQuestion.QText;
            Random random = new Random();
            int rnd = random.Next(1, 5);
            if (rnd == 1)
            {
                FirstAns = CurrentQuestion.CorrectAnswer;
                SecondAns = CurrentQuestion.Bad2;
                ThirdAns = CurrentQuestion.Bad3;
                FourthAns = CurrentQuestion.Bad1;
            }
            else if (rnd == 2)
            {
                FirstAns = CurrentQuestion.Bad1;
                SecondAns = CurrentQuestion.CorrectAnswer;
                ThirdAns = CurrentQuestion.Bad3;
                FourthAns = CurrentQuestion.Bad2;
            }
            else if (rnd == 3)
            {
                FirstAns = CurrentQuestion.Bad3;
                SecondAns = CurrentQuestion.Bad2;
                ThirdAns = CurrentQuestion.CorrectAnswer;
                FourthAns = CurrentQuestion.Bad1;
            }
            else 
            {
                FirstAns = CurrentQuestion.Bad1;
                SecondAns = CurrentQuestion.Bad3;
                ThirdAns = CurrentQuestion.Bad2;
                FourthAns = CurrentQuestion.CorrectAnswer;
            }
            //await Shell.Current.Navigation.PopModalAsync();
        }

        private async void onSelectingAnswer(string A)
        {
            string selectedAns;
            bool isCorrect;
            if (A == "1")
                selectedAns = FirstAns;
            else if (A == "2")
                selectedAns = SecondAns;
            else if (A == "3")
                selectedAns = ThirdAns;
            else   
                selectedAns = FourthAns;
            if (currentQuestion.CorrectAnswer == selectedAns)
            {
                isCorrect = true;
                ((App)Application.Current).LoggedInUser.Score += 10;
                score = ((App)Application.Current).LoggedInUser.Score;
                await Shell.Current.DisplayAlert("Correct !!", "plus 10 points", "ok");
                GetQ();
            }
            else
            {
                isCorrect = false;
                if (((App)Application.Current).LoggedInUser.Score % 100 < 5)
                    ((App)Application.Current).LoggedInUser.Score = ((App)Application.Current).LoggedInUser.Score / 100 * 100;
                else
                    ((App)Application.Current).LoggedInUser.Score -= 5;
                score = ((App)Application.Current).LoggedInUser.Score;
                await Shell.Current.DisplayAlert("Wrong !!", "minus 5 points", "ok");
                GetQ();
            }

        }

    }
}
