using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Actor
{
    public List<Feature> Conditions { get; }
    public Vector2Int Position { get; set; }

    public Actor(Vector2Int position) {
        Position = position;
        Conditions = new List<Feature>();
    }

    public List<Feature> GetConditionsWithAttribute(string attributeName) {
        return Conditions.FindAll(condition => condition.TargetAttributeName.Equals(attributeName));
    }

    public List<Feature> GetConditionsWithSource(string SourceName) {
        return Conditions.FindAll(condition => condition.SourceName.Equals(SourceName));
    }
}
