using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatureSerializable
{
    // The name of the condition being displayed (Blinded, AC, Speed)
    public string DisplayName { get; set; }

    // The source of the modification (Armor, Leveling, Blinded)
    public string SourceName { get; set; }

    // the name of the attribute to target (Speed, Strength, AC)
    public string AttributeName { get; set; }

    // When the condition wears off, if ever (longRest, shortRest, Round, Turn)
    public string Expiration { get; set; }

    // The numeric value of the feature
    public int NumericValue { get; set; }

    // The string value of the feature
    public string StringValue { get; set; }

    // The operation that gets applied to the numeric value
    public FeatureOperation Operation { get; set; }
}
