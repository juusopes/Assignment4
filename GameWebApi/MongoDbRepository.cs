using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using MongoDB.Driver;
using MongoDB.Bson;

public class MongoDbRepository : IRepository
{
    string path = @"C:\GitHub\assignment4\GameWebApi\game-dev.txt";
    List<Player> playerList = new List<Player>();

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
        var newPlayer = new Player
        {
            Id = player.Id,
            Name = player.Name,
            Score = 0,
            Level = 0,
            IsBanned = false,
            CreationTime = DateTime.Now
        };

        string output = JsonConvert.SerializeObject(newPlayer);
        File.AppendAllText(path, output);

        return await Task.FromResult<Player>(newPlayer);

        // await _playerCollection.InsertOneAsync(player);
        // return player;
    }

    public async Task<Player[]> GetAllPlayers()
    {
        var sortDef = Builders<Player>.Sort.Descending(player => player.Score);
        var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
        return players.ToArray();
    }

    public Task<Player> GetPlayer(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        return _playerCollection.Find(filter).FirstAsync();
    }

    public Task<Player> Create(Player player)
    {
        var newPlayer = new Player
        {
            Id = player.Id,
            Name = player.Name,
            Score = 0,
            Level = 0,
            IsBanned = false,
            CreationTime = DateTime.Now
        };

        string output = JsonConvert.SerializeObject(newPlayer);
        File.AppendAllText(path, output);

        return Task.FromResult<Player>(newPlayer);
    }

    public Task<Item> CreateItem(Guid playerId, Item item)
    {
        var newItem = new Item
        {
            Id = item.Id,
            Level = 0,
            Type = 0,
            CreationTime = DateTime.Now
        };

        string jsonToBeDeserialized = System.IO.File.ReadAllText(path);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player foundPlayer = new Player();
        foreach (Player player in players)
        {
            if (player.Id == playerId)
            {
                foundPlayer = player;
            }

        }

        if (foundPlayer.Id == playerId)
        {
            Item[] playerItems = new Item[foundPlayer.Items.Length + 1];
            int count = 0;

            foreach (Item i in foundPlayer.Items)
            {
                playerItems[count] = foundPlayer.Items[count];
                count++;
            }

            foundPlayer.Items = playerItems;
            foundPlayer.Items[playerItems.Length - 1] = item;
            string output = JsonConvert.SerializeObject(foundPlayer);
            File.AppendAllText(path, output);
            return Task.FromResult<Item>(newItem);
        }
        return Task.FromResult<Item>(newItem);
    }

    public async Task<Player> Delete(Guid id)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(player => player.Id, id);
        return await _playerCollection.FindOneAndDeleteAsync(filter);
    }

    public Task<Item> DeleteItem(Guid id, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> Get(Guid id)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, id);
        return _playerCollection.Find(filter).FirstAsync();
    }

    public async Task<Player[]> GetAll()
    {
        var sortDef = Builders<Player>.Sort.Descending(player => player.Score);
        var players = await _playerCollection.Find(new BsonDocument()).Sort(sortDef).ToListAsync();
        return players.ToArray();
    }

    public Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(path);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player foundPlayer = new Player();
        Item foundItem = new Item();

        foreach (Player player in players)
        {
            if (player.Id == playerId)
            {
                foreach (Item i in player.Items)
                {
                    if (i.Id == itemId)
                    {
                        foundItem = i;
                    }
                }
                return Task.FromResult<Item>(foundItem);
            }
        }

        foundPlayer.Name = "not found";
        return Task.FromResult<Item>(foundItem);
    }
    public Task<Item[]> GetAllItems(Guid playerId)
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(path);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player foundPlayer = new Player();
        Item[] foundItems = new Item[0];

        foreach (Player player in players)
        {
            if (player.Id == playerId)
            {
                foundItems = player.Items;
                return Task.FromResult<Item[]>(foundItems);
            }
        }

        foundPlayer.Name = "not found";
        return Task.FromResult<Item[]>(foundItems);
    }

    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(path);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player foundPlayer = new Player();
        foreach (Player p in players)
        {
            if (p.Id == id)
            {
                p.Score = player.Score;
                string output = JsonConvert.SerializeObject(players);
                File.WriteAllText(path, output);
                return Task.FromResult<Player>(p);
            }
        }
        foundPlayer.Name = "not Found";
        return Task.FromResult<Player>(foundPlayer);
    }

    public Task<Item> UpdateItem(Guid id, Item item)
    {
        throw new NotImplementedException();
    }
}

