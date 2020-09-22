using System;
using System.Threading.Tasks;
using System.Net.Http;
// using Newtonsoft.Json;
public interface IRepository
{
    Task<Player> Get(Guid id);
    Task<Player[]> GetAll();
    Task<Player> Create(Player player);
    Task<Player> Modify(Guid id, ModifiedPlayer player);
    Task<Player> Delete(Guid id);

    Task<Item> CreateItem(Guid id, Item item);
    Task<Item> GetItem(Guid id, Guid itemId);
    Task<Item[]> GetAllItems(Guid id);
    Task<Item> UpdateItem(Guid id, Item item);
    Task<Item> DeleteItem(Guid id, Item item);
    Task CreateItem();
}