
namespace MauiApp17.Service
{
    internal class SQLiteAsyncConnection
    {
        private string dbPath;

        public SQLiteAsyncConnection(string dbPath)
        {
            this.dbPath = dbPath;
        }

        internal object CreateTableAsync<T>() => throw new NotImplementedException();
        internal async Task<int> DeleteAsync(ShoppingCart cartItem) => throw new NotImplementedException();
        internal async Task<int> InsertAsync(ShoppingCart cartItem) => throw new NotImplementedException();
        internal async Task<int> InsertAsync(Profile profile) => throw new NotImplementedException();
        internal async Task<int> InsertAsync(ShoppingItem item) => throw new NotImplementedException();
        internal object Table<T>() => throw new NotImplementedException();
        internal async Task<int> UpdateAsync(ShoppingCart cartItem) => throw new NotImplementedException();
        internal async Task<int> UpdateAsync(Profile profile) => throw new NotImplementedException();
        internal async Task<int> UpdateAsync(ShoppingItem item) => throw new NotImplementedException();
    }
}