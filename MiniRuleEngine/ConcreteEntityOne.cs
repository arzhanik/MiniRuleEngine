namespace MiniRuleEngine;

public class ConcreteEntityOne : IEntity
{
    public string EntityType { get; }
    public int Id { get; }
    public int Age { get; }
    public bool IsActive { get; }
    public ConcreteEntityOne(int id, int age, bool isActive = false)
    {
        EntityType = "ConcreteEntityOne";
        Id = id;
        Age = age;
        IsActive = isActive;
    }
}