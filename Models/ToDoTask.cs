using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoer.Models;

public class ToDoTask
{
   // public string? Id { get; set; }
    public string? Name { get; set; }
    public bool? IsComplete { get; set; }
}