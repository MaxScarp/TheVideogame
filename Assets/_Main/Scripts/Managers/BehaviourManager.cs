using System.Collections.Generic;

/// <summary>
/// Class that is used for storing data regarding the various BehaviourSystems of all the Units of the game.
/// </summary>
public static class BehaviourManager
{
    public enum State
    {
        IDLE,
        MOVE,
        ATTACK
    }

    private static Dictionary<Unit, BehaviourSystem> behaviourSystemDictionary;

    static BehaviourManager()
    {
        behaviourSystemDictionary = new Dictionary<Unit, BehaviourSystem>();
    }

    /// <summary>
    /// Add a specified BehaviourSystem to the behaviourSystemDictionary.
    /// </summary>
    /// <param name="unit">The Unit to which the specified BehaviourSystem belongs to.</param>
    /// <param name="behaviourSystem">The specified BehaviourSystem.</param>
    public static void AddBehaviourSystem(Unit unit, BehaviourSystem behaviourSystem)
    {
        if (behaviourSystemDictionary.ContainsKey(unit)) return;

        behaviourSystemDictionary.Add(unit, behaviourSystem);
    }

    /// <summary>
    /// Remove a specified BehaviourSystem to the behaviourSystemDictionary.
    /// </summary>
    /// <param name="unit">The Unit to which the specified BehaviourSystem belongs to.</param>
    public static void RemoveBehaviourSystem(Unit unit)
    {
        if (!behaviourSystemDictionary.ContainsKey(unit)) return;

        behaviourSystemDictionary.Remove(unit);
    }
}
