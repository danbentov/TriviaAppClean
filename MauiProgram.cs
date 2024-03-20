using Microsoft.Extensions.Logging;
using TriviaAppClean.Services;
using TriviaAppClean.ViewModels;
using TriviaAppClean.Views;
using CommunityToolkit.Maui.MediaElement;
namespace TriviaAppClean;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkitMediaElement()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterDataServices()
            .RegisterPages()
            .RegisterViewModels(); 

#if DEBUG
		builder.Logging.AddDebug();
#endif
		
		return builder.Build();
	}

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginView> ();
        builder.Services.AddTransient<SignUpView> ();
        builder.Services.AddTransient<TriviaGameView>();
        builder.Services.AddTransient<ProfileView>();
        builder.Services.AddTransient<RecordsView>();
        builder.Services.AddTransient<QuestionDetailsView>();
        builder.Services.AddTransient<AllUsersView>();
        builder.Services.AddTransient<AllQuestionView>();
        builder.Services.AddTransient<AddQuestionView>();
        builder.Services.AddTransient<CheckPendingQuestionView>();
        builder.Services.AddTransient<ConnectingToServerView>();

        builder.Services.AddSingleton<GameStartView> ();

        return builder;
    }

    public static MauiAppBuilder RegisterDataServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<TriviaWebAPIProxy>();
        return builder;
    }
    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<SignUpViewModel>();
        builder.Services.AddTransient<TriviaGameViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();
        builder.Services.AddTransient<RecordsViewModel>();
        builder.Services.AddTransient<QuestionDetailsViewModel>();
        builder.Services.AddTransient<AllUsersViewModel>();
        builder.Services.AddTransient<AllQuestionViewModel>();
        builder.Services.AddTransient<AddQuestionViewModel>();
        builder.Services.AddTransient<CheckPendingQuestionViewModel>();
        builder.Services.AddTransient<AppShellViewModel>();

        builder.Services.AddSingleton<GameStartViewModel>();

        return builder;
    }
}
