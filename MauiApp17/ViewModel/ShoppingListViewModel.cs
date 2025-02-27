using MauiApp17.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiApp17.ViewModel
{
    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<ShoppingItem> _shoppingItems;

        public Command<ShoppingItem> AddToCartCommand { get; private set; }

        private bool _isLoading;
        private bool _isEmpty;

        public ShoppingListViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            _shoppingItems = new ObservableCollection<ShoppingItem>();
            AddToCartCommand = new Command<ShoppingItem>(async (item) => await AddToCart(item));
            
            // Load shopping items
            LoadShoppingItemsAsync();
        }
        public ObservableCollection<ShoppingItem> ShoppingItems
        {
            get => _shoppingItems;
            set
            {
                if (_shoppingItems != value)
                {
                    _shoppingItems = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            set
            {
                if (_isEmpty != value)
                {
                    _isEmpty = value;
                    OnPropertyChanged();
                }
            }
        }


        private async void LoadShoppingItemsAsync()
        {
            try
            {
                IsLoading = true;

                // Check if there are any items in the database
                var items = await _databaseService.GetShoppingItemsAsync();

                // If no items exist, seed some sample data
                if (items == null || !items.Any())
                {
                    await _databaseService.SeedShoppingItemsAsync();
                    items = await _databaseService.GetShoppingItemsAsync();
                }

                // Update UI
                ShoppingItems = new ObservableCollection<ShoppingItem>(items);
                IsEmpty = ShoppingItems.Count == 0;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load shopping items: {ex.Message}", "OK");
                IsEmpty = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task AddToCart(ShoppingItem item)
        {
            try
            {
                // Get current profile - in a real app, you would manage the current user profile
                var existingCartItem = await _databaseService.GetShoppingCartItemAsync(1, item.Id);

                if (existingCartItem != null)
                {
                    if (existingCartItem.Quantity < item.Quantity)
                    {
                        existingCartItem.Quantity++;
                        await _databaseService.SaveShoppingCartItemAsync(existingCartItem);
                        await Application.Current.MainPage.DisplayAlert("Success", $"Added another {item.Name} to your cart", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Sorry", $"No more {item.Name} in stock", "OK");
                    }
                }
                else
                {
                    var cartItem = new ShoppingCart
                    {
                        ProfileId = 1, // Use the actual profile ID in a real app
                        ShoppingItemId = item.Id,
                        Quantity = 1
                    };

                    await _databaseService.SaveShoppingCartItemAsync(cartItem);
                    await Application.Current.MainPage.DisplayAlert("Success", $"Added {item.Name} to your cart", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to add item to cart: {ex.Message}", "OK");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}