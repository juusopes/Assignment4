using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;

public class FileRepository : IRepository
{
    string filePath = @"C:\GitHub\Assignment3\GameWebApi\game-dev.txt";
    public Task<Player> Get(Guid id)
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(filePath);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player searchedPlayer = new Player();

        foreach (Player player in players)
        {
            if (player.Id == id)
            {
                searchedPlayer = player;
                return Task.FromResult<Player>(searchedPlayer);
            }
        }

        searchedPlayer.Name = "was not Found";
        return Task.FromResult<Player>(searchedPlayer);
    }

    public Task<Player[]> GetAll()
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(filePath);
        Resolver players = JsonConvert.DeserializeObject<Resolver>(jsonToBeDeserialized);
        Player[] playerBatch = players.playerList;

        return Task.FromResult<Player[]>(playerBatch);
    }

    public Task<Player> Create(Player player)
    {
        Player[] playerBatch;
        string jsonToBeDeserialized = System.IO.File.ReadAllText(filePath);
        if (jsonToBeDeserialized.Length > 0)
        {
            Resolver players = JsonConvert.DeserializeObject<Resolver>(jsonToBeDeserialized);
            playerBatch = new Player[players.playerList.Length + 2];
            var newPlayer = new Player
            {
                Id = player.Id,
                Name = player.Name,
                Score = 0,
                Level = 0,
                IsBanned = false,
                CreationTime = DateTime.Now
            };

            playerBatch = players.playerList;
            playerBatch[players.playerList.Length] = newPlayer;
            Resolver newPlayers = new Resolver(playerBatch);
            string output = JsonConvert.SerializeObject(newPlayers);
            File.WriteAllText(filePath, output);

            return Task.FromResult<Player>(newPlayer);
        }

        else
        {
            playerBatch = new Player[1000];

            var newPlayer = new Player
            {
                Id = player.Id,
                Name = player.Name,
                Score = 0,
                Level = 0,
                IsBanned = false,
                CreationTime = DateTime.Now
            };

            playerBatch[0] = newPlayer;
            Resolver newPlayers = new Resolver(playerBatch);
            string output = JsonConvert.SerializeObject(newPlayers);
            File.WriteAllText(filePath, output);

            return Task.FromResult<Player>(newPlayer);
        }
    }

    public class Resolver
    {
        public Player[] playerList { get; set; }
        public Resolver(Player[] playerListing)
        {
            playerList = playerListing;
        }
    }

    public Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(filePath);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player searchedPlayer = new Player();

        foreach (Player p in players)
        {
            if (p.Id == id)
            {
                p.Score = player.Score;
                string output = JsonConvert.SerializeObject(players);
                File.WriteAllText(filePath, output);
                return Task.FromResult<Player>(p);
            }
        }

        searchedPlayer.Name = "not Found";
        return Task.FromResult<Player>(searchedPlayer);
    }

    public Task<Player> Delete(Guid id)
    {
        string jsonToBeDeserialized = System.IO.File.ReadAllText(filePath);
        List<Player> players = JsonConvert.DeserializeObject<List<Player>>(jsonToBeDeserialized);
        Player searchedPlayer = new Player();

        foreach (Player p in players)
        {
            if (p.Id == id)
            {
                searchedPlayer = p;
                players.Remove(p);
                string output = JsonConvert.SerializeObject(players);
                File.WriteAllText(filePath, output);
                return Task.FromResult<Player>(searchedPlayer);
            }
        }

        searchedPlayer.Name = "not Found";
        return Task.FromResult<Player>(searchedPlayer);
    }

    public Task<Item> CreateItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }
    public Task CreateItem()
    {
        throw new NotImplementedException();
    }

    public Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Task<Item[]> GetAllItems(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Item> UpdateItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Item> DeleteItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }


}