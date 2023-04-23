using CommunityToolkit.Maui;
using MauiAppToDo.ViewModel;

namespace MauiAppToDo;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Sitka.ttf", "Sitka");
                fonts.AddFont("fa-solid-900.ttf", "FaSolid");
            });
		builder.Services.AddSingleton<ToDoPage>();
        builder.Services.AddSingleton<MainViewModel>();

        return builder.Build();
	}
}
