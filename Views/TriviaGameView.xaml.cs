using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class TriviaGameView : ContentPage
{
	public TriviaGameView(TriviaGameViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}