using Microsoft.Extensions.Logging;
using MauiApp17.Service;
using MauiApp17.View;
using System.IO;

namespace MauiApp17;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register services
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "shop.db3");
        builder.Services.AddSingleton(s => new DatabaseService(dbPath));

        // Register pages
        builder.Services.AddTransient<ProfilePage>();

        return builder.Build();
    }
}