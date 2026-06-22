using MiniRuleEngine;

public static class Program
{
    public static void Main()
    {
        RuleEngine engine = new RuleEngine();

        RegisterRules(engine);

        IEntity[] entities =
        {
            new ConcreteEntityOne(1, 25, true),
            new ConcreteEntityOne(2, 15, false),

            new ConcreteEntityTwo(3, 10, true),
            new ConcreteEntityTwo(4, -5, false)
        };

        Console.WriteLine("=== Fail-Fast Mode ===");

        for (int i = 0; i < entities.Length; ++i)
        {
            try
            {
                engine.ValidateFailFast(entities[i]);

                Console.WriteLine(
                    $"{entities[i].EntityType} #{entities[i].Id} passed validation.");
            }
            catch (RuleViolationException ex)
            {
                Console.WriteLine(
                    $"{entities[i].EntityType} #{entities[i].Id}: {ex.RuleName} - {ex.Message}");
            }
        }

        Console.WriteLine();

        Console.WriteLine("=== Collect-All Mode ===");

        for (int i = 0; i < entities.Length; ++i)
        {
            try
            {
                engine.ValidateCollectAll(entities[i]);

                Console.WriteLine(
                    $"{entities[i].EntityType} #{entities[i].Id} passed validation.");
            }
            catch (EntityValidationException ex)
            {
                Console.WriteLine(ex.Message);

                for (int j = 0; j < ex.Violations.Length; ++j)
                {
                    Console.WriteLine(
                        $"{ex.Violations[j].RuleName}: {ex.Violations[j].Message}");
                }
            }
        }
    }

    private static void RegisterRules(RuleEngine engine)
    {
        engine.AddRule(
            new Rule(
                "Age >= 18",
                "ConcreteEntityOne",
                entity =>
                {
                    ConcreteEntityOne obj =
                        (ConcreteEntityOne)entity;

                    if (obj.Age < 18)
                    {
                        throw new RuleViolationException(
                            "Age >= 18",
                            "Age must be at least 18.");
                    }
                }));

        engine.AddRule(
            new Rule(
                "Must Be Active",
                "ConcreteEntityOne",
                entity =>
                {
                    ConcreteEntityOne obj =
                        (ConcreteEntityOne)entity;

                    if (!obj.IsActive)
                    {
                        throw new RuleViolationException(
                            "Must Be Active",
                            "Entity must be active.");
                    }
                }));

        engine.AddRule(
            new Rule(
                "Count > 0",
                "ConcreteEntityTwo",
                entity =>
                {
                    ConcreteEntityTwo obj =
                        (ConcreteEntityTwo)entity;

                    if (obj.Count <= 0)
                    {
                        throw new RuleViolationException(
                            "Count > 0",
                            "Count must be greater than zero.");
                    }
                }));

        engine.AddRule(
            new Rule(
                "Must Be Alive",
                "ConcreteEntityTwo",
                entity =>
                {
                    ConcreteEntityTwo obj =
                        (ConcreteEntityTwo)entity;

                    if (!obj.IsAlive)
                    {
                        throw new RuleViolationException(
                            "Must Be Alive",
                            "Entity must be alive.");
                    }
                }));
    }
}