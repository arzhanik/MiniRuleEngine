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
}