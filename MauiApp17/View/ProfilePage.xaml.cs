using MauiApp17.Service;
using MauiApp17.ViewModel;

namespace MauiApp17.View;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(DatabaseService databaseService)
    {
        InitializeComponent();
        BindingContext = new ProfileViewModel(databaseService);
    }
}
