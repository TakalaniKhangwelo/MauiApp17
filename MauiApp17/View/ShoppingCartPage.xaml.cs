
namespace MauiApp17.View
{
    public partial class ShoppingCartPage : ContentPage
    {
        public ShoppingCartPage()
        {
            InitializeComponent();
            BindingContext = new ShoppingCartViewModel();
        }
    }
}
