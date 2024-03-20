using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class AddQuestionView : ContentPage
{
	public AddQuestionView(AddQuestionViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}