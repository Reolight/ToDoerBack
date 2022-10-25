using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoer.Models;

namespace ToDoer.Services;

public class TodoService
{
    private readonly IMongoCollection<ToDoList> _todoCollection;

    public TodoService(IOptions<TodoStoreDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);
        var mongoDb = mongoClient.GetDatabase(options.Value.DatabaseName);
        _todoCollection = mongoDb.GetCollection<ToDoList>(options.Value.TodosCollectionName);
    }

    public async Task<List<ToDoList>> GetAsync() =>
        await _todoCollection.Find(_ => true).ToListAsync();

    public async Task<ToDoList?> GetAsync(string id) =>
        await _todoCollection.Find(item => item.Id == id).FirstOrDefaultAsync();

    public async Task<List<ToDoList>> GetByOwnerAsync(string owner) =>
        await _todoCollection.Find(item => item.Owner == owner).ToListAsync();

    public async Task CreateAsync(ToDoList newTodo)
        => await _todoCollection.InsertOneAsync(newTodo);

    public async Task UpdateAsync(string id, ToDoList updatableTodo)
        => await _todoCollection.ReplaceOneAsync(item => item.Id == id, updatableTodo);

    public async Task RemoveAsync(string id)
        => await _todoCollection.DeleteOneAsync(item => item.Id == id);
}