﻿namespace MauiApp17
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ShoppingListViewModel();
        }
    }
}
