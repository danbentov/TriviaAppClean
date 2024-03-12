using TriviaAppClean.Views;

namespace TriviaAppClean;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
	}

	private void RegisterRoutes()
	{
        Routing.RegisterRoute("connectingToServer", typeof(ConnectingToServerView));
		Routing.RegisterRoute("triviaGame", typeof(TriviaGameView));
        Routing.RegisterRoute("signUp", typeof(SignUpView));
        Routing.RegisterRoute("questionDetails", typeof(QuestionDetailsView));
    }
}
