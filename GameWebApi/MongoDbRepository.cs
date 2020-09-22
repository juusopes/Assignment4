using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbRepository : IRepository
{
    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<BsonDocument> _BsonDocumentCollection;

    public MongoDbRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
        _BsonDocumentCollection = database.GetCollection<BsonDocument>("players");
    }

    public async Task<Player> CreatePlayer(Player player)
    {
        await _playerCollection.InsertOneAsync(player);
        return player;
    }

    public async Task<Player[]> GetAllPlayers()
    {
        var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
        return players.ToArray();
    }

    public Task<Player> GetPlayer(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        return _playerCollection.Find(filter).FirstAsync();
    }

    public Task<Item> CreateItem(Guid id, Item item)
    {
        throw new NotImplementedException();
    }

    public Task CreateItem()
    {
        throw new NotImplementedException();
    }

    public Task<Player> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Item> DeleteItem(Guid id, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Player[]> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Item[]> GetAllItems(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Item> GetItem(Guid id, Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        throw new NotImplementedException();
    }

    public Task<Item> UpdateItem(Guid id, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> Create(Player player)
    {
        throw new NotImplementedException();
    }
}

