using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class GameStartView : ContentPage
{
	public GameStartView(GameStartViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}