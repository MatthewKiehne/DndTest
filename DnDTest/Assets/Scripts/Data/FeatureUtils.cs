using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FeatureUtils
{
    public static Feature Clone(Feature feature) {
        Feature result = new Feature(feature.DisplayName, feature.SourceName, feature.TargetAttributeName);
        result.Operation = feature.Operation;
        result.StringValue = feature.StringValue;
        result.NumericValue = feature.NumericValue;
        result.Expiration = feature.Expiration;
        return result;
    }
}
