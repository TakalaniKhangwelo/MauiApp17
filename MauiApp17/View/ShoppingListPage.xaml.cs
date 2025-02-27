using MauiApp17.ViewModel;

namespace MauiApp17.View
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();
            BindingContext = new ShoppingListViewModel();
        }
    }
}
