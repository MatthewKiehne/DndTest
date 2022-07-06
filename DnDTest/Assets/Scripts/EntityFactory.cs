using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory
{
    public static readonly string ExtensionName = "Test";

    public static readonly string GroupVisability = "Visability";
    public static readonly string GroupMovement = "Movement";

    public static readonly string SourceIntrinsic = "Intrinsic";

    public static readonly string AttributeVisabilityBlock = "VisabilityBlock";
    public static readonly string AttributeVisabilityToken = "VisabilityToken";
    public static readonly string AttributeMovementImpassible = "Impassible";

    public static readonly string ExperationNever = "Never";

    public static Actor GenerateWall(Vector2Int position) {
        Feature blockVisability = new Feature("Block Visability",
            GenerateSourceName(ExtensionName, GroupVisability, SourceIntrinsic),
            GenerateTargetAttributeName(ExtensionName, GroupVisability, AttributeVisabilityBlock));
        blockVisability.Expiration = ExperationNever;

        Feature blockMovement = new Feature("Block Movement",
            GenerateSourceName(ExtensionName, GroupMovement, SourceIntrinsic),
            GenerateTargetAttributeName(ExtensionName, GroupMovement, AttributeMovementImpassible));
        blockMovement.Expiration = ExperationNever;

        Feature token = new Feature("Token",
            GenerateSourceName(ExtensionName, GroupVisability, SourceIntrinsic),
            GenerateTargetAttributeName(ExtensionName, GroupVisability, AttributeVisabilityToken));
        token.StringValue = "Wall";

        Actor entity = new Actor(position);
        entity.Conditions.Add(blockVisability);
        entity.Conditions.Add(blockMovement);
        entity.Conditions.Add(token);

        return entity;
    }

    public static string GenerateSourceName(string extensionName, string groupName, string sourceName) {
        return extensionName + '.' + groupName + '.' + sourceName;
    }

    public static string GenerateTargetAttributeName(string extensionName, string groupName, string attributeName) {
        return extensionName + '.' + groupName + '.' + attributeName;
    }
}
