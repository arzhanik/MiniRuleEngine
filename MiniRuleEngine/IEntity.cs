namespace MiniRuleEngine;

public delegate void RuleCheck(IEntity entity);

public interface IEntity
{
    int Id { get; }
    string EntityType { get; }
}