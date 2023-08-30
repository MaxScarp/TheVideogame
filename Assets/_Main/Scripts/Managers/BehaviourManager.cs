using System.Collections.Generic;

public static class BehaviourManager
{
    private static Dictionary<Unit, BehaviourSystem> behaviourSystemDictionary;

    static BehaviourManager()
    {
        behaviourSystemDictionary = new Dictionary<Unit, BehaviourSystem>();
    }

    public static void AddBehaviourSystem(Unit unit, BehaviourSystem behaviourSystem)
    {
        if (behaviourSystemDictionary.ContainsKey(unit)) return;

        behaviourSystemDictionary.Add(unit, behaviourSystem);
    }

    public static void RemoveBehaviourSystem(Unit unit, BehaviourSystem behaviourSystem)
    {
        if (!behaviourSystemDictionary.ContainsKey(unit)) return;

        behaviourSystemDictionary.Remove(unit);
    }
}
