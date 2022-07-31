using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Names {
    public static class Extension {
        public static readonly string Name = "Default";
    }

    public static class Group {
        public static readonly string Visability = "Visability";
        public static readonly string Movement = "Movement";
        public static readonly string Postion = "Position";
        public static readonly string Vision = "Vision";

    }

    public static class Attribute {
        public static class Visability {
            public static readonly string Block = "VisabilityBlock";
            public static readonly string Token = "VisabilityToken";
        }

        public static class Movement {
            public static readonly string Impassible = "Impassible";
            public static readonly string MovementRange = "MovementRange";
        }

        public static class Position {
            public static readonly string X = "X";
            public static readonly string Y = "Y";
        }

        public static class Lookup {
            public static readonly string Look = "Lookup";
        }

        public static class Reference {
            public static readonly string Ref = "Reference";
        }

        public static class Vision {
            public static readonly string VisionRange = "VisionRange";
            public static readonly string DarkVisionRange = "DarkVisionRange";
            public static readonly string BlindSightRange = "VisionRangeRange";
        }
    }

    public static class Source {
        public static readonly string Intrinsic = "Intrinsic";
    }

    public static class Experation {
        public static readonly string Never = "Never";
    }

    public static string CreateSourceName(string groupName, string sourceName) {
        return Extension.Name + '.' + groupName + '.' + sourceName;
    }

    public static string CreateAttributeName(string groupName, string attributeName) {
        return Extension.Name + '.' + groupName + '.' + attributeName;
    }
}
