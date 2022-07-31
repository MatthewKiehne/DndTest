using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor {
    public List<Feature> Features { get; }

    public Actor() {
        Features = new List<Feature>();
    }

    public Actor Clone() {
        Actor result = new Actor();
        foreach (var condition in Features) {
            result.Features.Add(FeatureUtils.Clone(condition));
        }
        return result;
    }


    public List<Feature> GetFeaturesWithAttribute(string attributeName) {
        return Features.FindAll(condition => condition.Attribute.Equals(attributeName));
    }

    public Vector2Int GetPosition() {
        Feature xFeature = GetImideateFeatureByAttribute(Names.CreateAttributeName(Names.Group.Postion, Names.Attribute.Position.X));

        Feature YFeature = GetImideateFeatureByAttribute(Names.CreateAttributeName(Names.Group.Postion, Names.Attribute.Position.Y));

        return new Vector2Int(xFeature.NumericValue, YFeature.NumericValue);
    }

    public void SetPosition(Vector2Int position) {
        Feature xFeature = GetImideateFeatureByAttribute(Names.CreateAttributeName(Names.Group.Postion, Names.Attribute.Position.X));

        Feature YFeature = GetImideateFeatureByAttribute(Names.CreateAttributeName(Names.Group.Postion, Names.Attribute.Position.Y));

        xFeature.NumericValue = position.x;
        YFeature.NumericValue = position.y;
    }

    public Feature GetImideateFeatureByAttribute(string attributeName) {
        return Features.Find(feature => feature.Attribute.Equals(attributeName));
    }

    public List<Feature> GetImidateFeaturesByAttribute(string attributeName) {
        return Features.FindAll(feature => feature.Attribute.Equals(attributeName));
    }

    /// <summary>
    /// Finds featurs that match the attribute name and recursivly looks through lookups to ind additional features
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="attributeName"></param>
    /// <returns></returns>
    public List<Feature> GetFeaturesByAttribute(string attributeName) {
        List<Feature> result = new List<Feature>();
        foreach (Feature feature in Features) {

            // look up actors in the lookup table
            if (feature.Operation == FeatureOperation.Der) {
                Actor lookupActor = ActorStorage.CloneFromLookup(feature.StringValue);
                result.AddRange(lookupActor.GetFeaturesByAttribute(attributeName));

                // add feature to the result if it matches the attribute name
            } else if (feature.Attribute.Equals(attributeName)) {
                result.Add(feature);
            }
        }
        return result;
    }

    public Dictionary<string, List<Feature>> GetMultipleFeaturesByAttribute(HashSet<string> attributeNames) {
        Dictionary<string, List<Feature>> result = new Dictionary<string, List<Feature>>();
        foreach (Feature feature in Features) {

            // Lookup actors in lookup table
            if (feature.Operation == FeatureOperation.Der) {

                // clone actor and find featurs
                Actor lookupActor = ActorStorage.CloneFromLookup(feature.StringValue);
                Dictionary<string, List<Feature>> lookupResult = lookupActor.GetMultipleFeaturesByAttribute(attributeNames);

                // combine found features into current result
                foreach (KeyValuePair<string, List<Feature>> pair in lookupResult) {
                    if (!result.ContainsKey(pair.Key)) {
                        result.Add(pair.Key, new List<Feature>());
                    }
                    result[pair.Key].AddRange(pair.Value);
                }

                // add the feature to the result if matches an attribute name
            } else if (attributeNames.Contains(feature.Attribute)) {
                if (!result.ContainsKey(feature.Attribute)) {
                    result.Add(feature.Attribute, new List<Feature>());
                }
                result[feature.Attribute].Add(feature);
            }
        }

        return result;
    }

    /// <summary>
    /// Finds the numeric value all the features passed in
    /// </summary>
    /// <param name="features"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public int CalculateNumericValueFromFeatures(List<Feature> features) {

        int sumValues = 0;
        float multiplyValues = 1;

        foreach (Feature feature in features) {
            switch (feature.Operation) {
                case FeatureOperation.Add:
                    sumValues += feature.NumericValue;
                    break;
                case FeatureOperation.Sub:
                    sumValues -= feature.NumericValue;
                    break;
                case FeatureOperation.Mul:
                    multiplyValues *= feature.NumericValue;
                    break;
                case FeatureOperation.Div:
                    if (feature.NumericValue == 0) {
                        throw new System.Exception("Can not divide by zero");
                    }
                    multiplyValues = multiplyValues / feature.NumericValue;
                    break;
                case FeatureOperation.Set:
                    return feature.NumericValue;
                default:
                    // does nothing
                    break;
            }
        }

        return Mathf.FloorToInt(sumValues * multiplyValues);
    }

    public int CalculateNumbericValueOfAttribute(string attributeName) {
        List<Feature> features = GetFeaturesByAttribute(attributeName);
        return CalculateNumericValueFromFeatures(features);
    }
}
