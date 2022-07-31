using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Feature
{
    // The name of the condition being displayed (Blinded, AC, Speed)
    public string DisplayName { get; }

    // The source of the modification (Armor, Leveling, Blinded)
    public string SourceName { get; }

    // the name of the attribute to target (Speed, Strength, AC)
    public string AttributeName { get; }

    // When the condition wears off, if ever (longRest, shortRest, Round, Turn)
    public string Expiration { get; set; }

    // The numeric value attached to the Condiditon
    public int NumericValue { get; set; }

    // The operation that gets applied to the numeric value
    public FeatureOperation Operation { get; set; }

    // The string value of the feature
    public string StringValue { get; set; }

    public Feature(string displayName, string sourceName, string targetAttributeName) {
        this.DisplayName = displayName;
        this.SourceName = sourceName;
        this.AttributeName = targetAttributeName;
        Operation = FeatureOperation.Add;
    }
}