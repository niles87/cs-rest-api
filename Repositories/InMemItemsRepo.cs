﻿using GameServices.Entities;

namespace GameServices.Repositories;

  public class InMemItemsRepo : IInMemItemsRepo {

    private readonly List<Item> items = new() {
      new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreateDate = DateTimeOffset.UtcNow },
      new Item() { Id = Guid.NewGuid(), Name = "Sword", Price = 20, CreateDate = DateTimeOffset.UtcNow },
      new Item() { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreateDate = DateTimeOffset.UtcNow },
    };


    public async Task<IEnumerable<Item>> GetItemsAsync() {
      return await Task.FromResult(items);
    }

    public async Task<Item> GetItemAsync(Guid id) {
      var item = items.SingleOrDefault(item => item.Id == id);
      return await Task.FromResult(item);
    }

    public async Task CreateItemAsync(Item item) {
      items.Add(item);
      await Task.CompletedTask;
    }

    public async Task UpdateItemAsync(Item item) {
      var index = items.FindIndex(exsitingItem => exsitingItem.Id == item.Id);
      items[index] = item;
      await Task.CompletedTask;
    }

    public async Task DeleteItemAsync(Guid id) {
      var index = items.FindIndex((item) => item.Id == id);
      items.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
