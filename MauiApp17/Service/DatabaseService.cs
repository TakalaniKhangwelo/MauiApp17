using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Profile
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}

public class ShoppingItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string ImageUrl { get; set; }
}

public class ShoppingCart
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ProfileId { get; set; }
    public int ShoppingItemId { get; set; }
    public int Quantity { get; set; }
}

public class DatabaseService
{
    private SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Profile>().Wait();
        _database.CreateTableAsync<ShoppingItem>().Wait();
        _database.CreateTableAsync<ShoppingCart>().Wait();
    }

    public Task<List<Profile>> GetProfilesAsync() => _database.Table<Profile>().ToListAsync();

    public Task<Profile> GetProfileAsync(int id) => _database.Table<Profile>().Where(i => i.Id == id).FirstOrDefaultAsync();

    public Task<int> SaveProfileAsync(Profile profile) => profile.Id != 0 ? _database.UpdateAsync(profile) : _database.InsertAsync(profile);

    public Task<List<ShoppingItem>> GetShoppingItemsAsync() => _database.Table<ShoppingItem>().ToListAsync();

    public Task<int> SaveShoppingItemAsync(ShoppingItem item) => item.Id != 0 ? _database.UpdateAsync(item) : _database.InsertAsync(item);

    public Task<List<ShoppingCart>> GetShoppingCartAsync(int profileId) => _database.Table<ShoppingCart>().Where(c => c.ProfileId == profileId).ToListAsync();

    public Task<ShoppingCart> GetShoppingCartItemAsync(int profileId, int itemId) => _database.Table<ShoppingCart>().Where(c => c.ProfileId == profileId && c.ShoppingItemId == itemId).FirstOrDefaultAsync();

    public Task<int> SaveShoppingCartItemAsync(ShoppingCart cartItem) => cartItem.Id != 0 ? _database.UpdateAsync(cartItem) : _database.InsertAsync(cartItem);

    public Task<int> DeleteShoppingCartItemAsync(ShoppingCart cartItem) => _database.DeleteAsync(cartItem);

    public async Task SeedShoppingItemsAsync()
    {
        var items = new List<ShoppingItem>
        {
            new ShoppingItem { Name = "Celphone", Price = 9.99, Quantity = 100, ImageUrl = "iphone.jpg" },
            new ShoppingItem { Name = "Laptop", Price = 19.99, Quantity = 30, ImageUrl = "laptop.jpg" },
            new ShoppingItem { Name = "TV", Price = 29.99, Quantity = 150, ImageUrl = "tv.jpg" },
        };

        foreach (var item in items)
        {
            await SaveShoppingItemAsync(item);
        }
    }
}