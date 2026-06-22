namespace MiniRuleEngine;

public class ConcreteEntityTwo : IEntity
{
    public string EntityType { get; }
    public int Id { get; }
    public int Count { get; }
    public bool IsAlive { get; }
    public ConcreteEntityTwo(int id, int count, bool isAlive = false)
    {
        EntityType = "ConcreteEntityTwo";
        Id = id;
        Count = count;
        IsAlive = isAlive;
    }
}