using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActorStorage {

    // this is the root guid of the actor that all other actors will be children of
    private static readonly Guid odinId;

    // used to create store all the actors that exist in the world
    private static Dictionary<Guid, Actor> reference = new Dictionary<Guid, Actor>();

    // used to create new actors and a look up for look up actions on features
    private static Dictionary<string, Actor> lookup = new Dictionary<string, Actor>();


    // TODO: make functions for this and use them
    // used to store battle maps and used to look up position of Actors
    private static Dictionary<string, Dictionary<Vector2Int, List<Actor>>> battleMaps = new Dictionary<string, Dictionary<Vector2Int, List<Actor>>>();

    /// <summary>
    /// Constructor that gets called when the program starts
    /// </summary>
    static ActorStorage() {
        odinId = AddActorToReference(new Actor());
    }

    /// <summary>
    /// Gets the Id of the Odin Actor. All other active Actors should be a child of this Actor.
    /// </summary>
    /// <returns></returns>
    public static Guid GetOdinId() {
        return odinId;
    }

    /// <summary>
    /// Gets the Odin Actor. All other active Actors should be a child of this Actor.
    /// </summary>
    /// <returns></returns>
    public static Actor GetOdinActor() {
        return reference[odinId];
    }

    /// <summary>
    /// Adds an actor the references to look up later
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    public static Guid AddActorToReference(Actor actor) {
        Guid guid = Guid.NewGuid();
        reference.Add(guid, actor);
        return guid;
    }

    /// <summary>
    /// Removes an actor based on its Id
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static bool RemoveActorFromReference(Guid guid) {
        return reference.Remove(guid);
    }

    public static Actor GetActorFromReference(Guid guid) {
        if (reference.ContainsKey(guid)) {
            return reference[guid];
        }
        return null;
    }

    /// <summary>
    /// Adds an Actor to the be looked up later. Throws an error if the name is taken already
    /// </summary>
    /// <param name="name"></param>
    /// <param name="actor"></param>
    /// <exception cref="System.Exception"></exception>
    public static void AddActorToLookUp(string name, Actor actor) {
        if (lookup.ContainsKey(name)) {
            throw new Exception("Actor with name " + name + " already exists in lookup table");
        }
        lookup.Add(name, actor);
    }

    /// <summary>
    /// Returns if the lookup name is taken
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool LookupNameExists(string name) {
        return lookup.ContainsKey(name);
    }

    /// <summary>
    /// Clones an Actor from the lookup table
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public static Actor CloneFromLookup(string name) {
        if (!ActorStorage.lookup.ContainsKey(name)) {
            throw new System.Exception("Actor with name " + name + " does not exists in lookup table");
        }
        return lookup[name].Clone();
    }

    /// <summary>
    /// Creates an actor with a feature that points to a actor in the lookup table
    /// </summary>
    /// <param name="lookupName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Actor CreateActorPointingAtLookup(string lookupName) {
        if(!lookup.ContainsKey(lookupName)) {
            throw new Exception("Unable to find actor with name of " + lookupName);
        }

        Feature lookupFeature = new Feature("Lookup " + lookupName, "lookup", "lookup");
        lookupFeature.Operation = FeatureOperation.Der;
        lookupFeature.StringValue = lookupName;
        
        Actor actor = new Actor();
        actor.Features.Add(lookupFeature);
        return actor;
    }

    /// <summary>
    /// Creates a new battle map. Returns true if it was successfully created.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool AddBattleMap(string name) {
        if (battleMaps.ContainsKey(name)) {
            return false;
        }
        battleMaps.Add(name, new Dictionary<Vector2Int, List<Actor>>());
        return true;
    }

    /// <summary>
    /// Removes a battle map by its name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool RemoveBattleMap(string name) {
        return battleMaps.Remove(name);
    }

    /// <summary>
    /// Adds a new Actor to the battle map at the position passed in
    /// </summary>
    /// <param name="battleMapName"></param>
    /// <param name="actor"></param>
    /// <param name="position"></param>
    /// <exception cref="Exception"></exception>
    public static void AddActorToPosition(string battleMapName, Actor actor, Vector2Int position) {
        if (!battleMaps.ContainsKey(battleMapName)) {
            throw new Exception("Battle map " + battleMapName + " does not exist");
        }
        if (!battleMaps[battleMapName].ContainsKey(position)) {
            battleMaps[battleMapName].Add(position, new List<Actor>());
        }
        battleMaps[battleMapName][position].Add(actor);
    }

    /// <summary>
    /// Removes an Actor from the battle map at the position passed in
    /// </summary>
    /// <param name="battleMapName"></param>
    /// <param name="actor"></param>
    /// <param name="position"></param>
    /// <exception cref="Exception"></exception>
    public static bool RemoveActorFromBattleMaptor(string battleMapName, Actor actor, Vector2Int position) {
        if (!battleMaps.ContainsKey(battleMapName)) {
            throw new Exception("Battle map " + battleMapName + " does not exist");
        }
        if (!battleMaps[battleMapName].ContainsKey(position)) {
            throw new Exception("Actors do not exist at the position passed in");
        }

        bool removed = battleMaps[battleMapName][position].Remove(actor);
        if (battleMaps[battleMapName][position].Count == 0) {
            battleMaps[battleMapName].Remove(position);
        }

        return removed;
    }

    /// <summary>
    /// Gets a list of all the Actors on a position on a battle map. 
    /// return null if none were found and throws an error if the battle map does not exist.
    /// </summary>
    /// <param name="battleMap"></param>
    /// <param name="postion"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<Actor> GetActorsAtPositon(string battleMapName, Vector2Int postion) {
        if (!battleMaps.ContainsKey(battleMapName)) {
            throw new Exception("Battle map " + battleMapName + " does not exist");
        }
        if (battleMaps[battleMapName].ContainsKey(postion)) {
            return battleMaps[battleMapName][postion];
        }
        return null;
    }

    public static List<Feature> AddFeaturesToActor(Actor actor, string lookUpName) {
        return AddFeaturesToActor(actor, lookUpName, true);
    }

    public static List<Feature> AddFeaturesToActor(Actor actor, string lookUpName, bool allowDuplicateAttributes) {
        if (!ActorStorage.lookup.ContainsKey(lookUpName)) {
            throw new System.Exception("Actor with name " + lookUpName + " does not exists in lookup table");
        }

        List<Feature> featuresAdded = new List<Feature>();
        foreach (Feature feature in ActorStorage.lookup[lookUpName].Features) {
            if (allowDuplicateAttributes || actor.GetFeaturesWithAttribute(feature.Attribute).Count == 0) {
                Feature newFeature = FeatureUtils.Clone(feature);
                actor.Features.Add(newFeature);
                featuresAdded.Add(newFeature);
            }
        }
        return featuresAdded;
    }
}
