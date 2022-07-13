using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ActorUtils
{
    public static Actor Clone(Actor actor) {
        Actor result = new Actor();
        foreach (var condition in actor.Features) {
            result.Features.Add(FeatureUtils.Clone(condition));
        }
        return result;
    }

    public static List<Feature> GetFeaturesWithAttribute(Actor actor, string attributeName) {
        return actor.Features.FindAll(condition => condition.TargetAttributeName.Equals(attributeName));
    }

    public static List<Feature> GetFeaturesWithSource(Actor actor, string SourceName) {
        return actor.Features.FindAll(condition => condition.SourceName.Equals(SourceName));
    }

    public static List<Feature> GetFeaturesBySource(Actor actor, string sourceString) {
        return actor.Features.FindAll(feature => feature.SourceName.Equals(sourceString));
    }

    public static Feature GetFirstFeatureByAttribute(Actor actor, string attributeString) {
        return actor.Features.Find(feature => feature.TargetAttributeName.Equals(attributeString));
    }

    public static List<Feature> GetFeaturesByAttribute(Actor actor, string attributeString) {
        return actor.Features.FindAll(feature => feature.TargetAttributeName.Equals(attributeString));
    }

    public static Vector2Int GetPosition(Actor actor) {
        Feature xFeature = GetFirstFeatureByAttribute(actor, EntityFactory.GenerateTargetAttributeName(EntityFactory.Extension.Name, EntityFactory.Group.Postion, EntityFactory.Attribute.Position.X));

        Feature YFeature = GetFirstFeatureByAttribute(actor, EntityFactory.GenerateTargetAttributeName(EntityFactory.Extension.Name, EntityFactory.Group.Postion, EntityFactory.Attribute.Position.Y));

        return new Vector2Int(xFeature.NumericValue, YFeature.NumericValue);
    }

    public static void SetPosition(Actor actor, Vector2Int position) {
        Feature xFeature = GetFirstFeatureByAttribute(actor, EntityFactory.GenerateTargetAttributeName(EntityFactory.Extension.Name, EntityFactory.Group.Postion, EntityFactory.Attribute.Position.X));

        Feature YFeature = GetFirstFeatureByAttribute(actor, EntityFactory.GenerateTargetAttributeName(EntityFactory.Extension.Name, EntityFactory.Group.Postion, EntityFactory.Attribute.Position.Y));

        xFeature.NumericValue = position.x;
        YFeature.NumericValue = position.y;
    }
}
