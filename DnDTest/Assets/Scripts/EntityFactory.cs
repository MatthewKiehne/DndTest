using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EntityFactory {
    public static class Extension {
        public static readonly string Name = "Test";
    }

    public static class Group {
        public static readonly string Visability = "Visability";
        public static readonly string Movement = "Movement";
        public static readonly string Postion = "Position";
    }

    public static class Attribute {
        public static class Visability {
            public static readonly string Block = "VisabilityBlock";
            public static readonly string Token = "VisabilityToken";
        }

        public static class Movement {
            public static readonly string Impassible = "Impassible";
        }

        public static class Position {
            public static readonly string X = "X";
            public static readonly string Y = "Y";
        }
    }

    public static class Source {
        public static readonly string Intrinsic = "Intrinsic";
    }

    public static class Experation {
        public static readonly string Never = "Never";
    }

    public static Actor GenerateWall(Vector2Int position) {
        Feature blockVisability = new Feature("Block Visability",
            GenerateSourceName(Extension.Name, Group.Visability, Source.Intrinsic),
            GenerateTargetAttributeName(Extension.Name, Group.Visability, Attribute.Visability.Block));
        blockVisability.Expiration = Experation.Never;

        Feature blockMovement = new Feature("Block Movement",
            GenerateSourceName(Extension.Name, Group.Movement, Source.Intrinsic),
            GenerateTargetAttributeName(Extension.Name, Group.Movement, Attribute.Movement.Impassible));
        blockMovement.Expiration = Experation.Never;

        Feature token = new Feature("Token",
            GenerateSourceName(Extension.Name, Group.Visability, Source.Intrinsic),
            GenerateTargetAttributeName(Extension.Name, Group.Visability, Attribute.Visability.Token));
        token.StringValue = "Wall";

        Feature positionX = new Feature("X",
            GenerateSourceName(Extension.Name, Group.Postion, Source.Intrinsic),
            GenerateTargetAttributeName(Extension.Name, Group.Postion, Attribute.Position.X));
        positionX.NumericValue = position.x;

        Feature positionY = new Feature("Y",
            GenerateSourceName(Extension.Name, Group.Postion, Source.Intrinsic),
            GenerateTargetAttributeName(Extension.Name, Group.Postion, Attribute.Position.Y));
        positionY.NumericValue = position.y;

        Actor entity = new Actor();
        entity.Features.Add(blockVisability);
        entity.Features.Add(blockMovement);
        entity.Features.Add(token);
        entity.Features.Add(positionX);
        entity.Features.Add(positionY);

        return entity;
    }

    public static string GenerateSourceName(string extensionName, string groupName, string sourceName) {
        return extensionName + '.' + groupName + '.' + sourceName;
    }

    public static string GenerateTargetAttributeName(string extensionName, string groupName, string attributeName) {
        return extensionName + '.' + groupName + '.' + attributeName;
    }
}
