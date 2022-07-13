using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActorLookup {
    private static Dictionary<string, Actor> lookup = new Dictionary<string, Actor>();

    public static void AddActor(string name, Actor actor) {
        if (lookup.ContainsKey(name)) {
            throw new System.Exception("Actor with name " + name + " already exists in lookup table");
        }

        lookup.Add(name, actor);
    }

    public static bool NameExists(string name) {
        return lookup.ContainsKey(name);
    }

    public static Actor Clone(string name) {
        if (!ActorLookup.lookup.ContainsKey(name)) {
            throw new System.Exception("Actor with name " + name + " does not exists in lookup table");
        }
        return ActorUtils.Clone(lookup[name]);
    }

    public static List<Feature> AddFeaturesToActor(Actor actor, string lookUpName) {
        return AddFeaturesToActor(actor, lookUpName, true);
    }

    public static List<Feature> AddFeaturesToActor(Actor actor, string lookUpName, bool allowDuplicateAttributes) {
        if (!ActorLookup.lookup.ContainsKey(lookUpName)) {
            throw new System.Exception("Actor with name " + lookUpName + " does not exists in lookup table");
        }

        List<Feature> featuresAdded = new List<Feature>();
        foreach (Feature feature in ActorLookup.lookup[lookUpName].Features) {
            if(allowDuplicateAttributes ||  ActorUtils.GetFeaturesWithAttribute(actor, feature.TargetAttributeName).Count == 0) {
                Feature newFeature = FeatureUtils.Clone(feature);
                actor.Features.Add(newFeature);
                featuresAdded.Add(newFeature);
            }
        }
        return featuresAdded;
    }
}
