namespace MiniRuleEngine;

public class RuleEngine
{
    internal Rule[] _rules;
    private int _count;
    
    public RuleEngine(int capacity = 4)
    {
        if (capacity <= 0)
        {
            capacity = 4;
        }
        _rules = new Rule[capacity];
    }

    public void AddRule(Rule rule)
    {
        if (_count == _rules.Length)
        {
            Array.Resize(ref _rules, _rules.Length * 2);
        }

        _rules[_count++] = rule;
    }

    public void ValidateFailFast(IEntity entity)
    {
        for (int i = 0; i < _count; ++i)
        {
            if (!_rules[i].AppliesTo(entity))
            {
                continue;
            }

            try
            {
                _rules[i].Check(entity);
            }
            catch (RuleViolationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RuleViolationException(_rules[i].Name, $"Unexpected rule violation: {ex.Message}");
            }
        }
    }
    public void ValidateCollectAll(IEntity entity)
    {
        int ViolationsCount = 0;
        RuleViolationException[] violations = new RuleViolationException[_count];
        for (int i = 0; i < _count; ++i)
        {
            if (!_rules[i].AppliesTo(entity))
            {
                continue;
            }

            try
            {
                _rules[i].Check(entity);
            }
            catch (RuleViolationException violation)
            {
                violations[ViolationsCount++] = violation;
            }
            catch (Exception ex)
            {
                violations[ViolationsCount++] = new RuleViolationException(_rules[i].Name, $"Unexpected rule error: {ex.Message}");
            }
        }

        if (ViolationsCount > 0)
        {
            RuleViolationException[] exact = new RuleViolationException[ViolationsCount];
            for (int i = 0; i < ViolationsCount; i++)
            {
                exact[i] = violations[i];
            }
            throw new EntityValidationException(entity, exact);
        }
    }
}