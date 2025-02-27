using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp17.ViewModel
{

    public class ShoppingCartViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private List<ShoppingCart> _shoppingCartItems;

        public ShoppingCartViewModel()
        {
            _databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "shopping.db3"));
            LoadShoppingCartItems();
            RemoveFromCartCommand = new Command<ShoppingCart>(async (cartItem) => await RemoveFromCart(cartItem));
        }

        public List<ShoppingCart> ShoppingCartItems
        {
            get => _shoppingCartItems;
            set
            {
                if (_shoppingCartItems != value)
                {
                    _shoppingCartItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand RemoveFromCartCommand { get; }

        private async void LoadShoppingCartItems()
        {
            ShoppingCartItems = await _databaseService.GetShoppingCartAsync(1); // Use the actual profile ID
            OnPropertyChanged(nameof(ShoppingCartItems));
        }

        private async Task RemoveFromCart(ShoppingCart cartItem)
        {
            await _databaseService.DeleteShoppingCartItemAsync(cartItem);
            LoadShoppingCartItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
