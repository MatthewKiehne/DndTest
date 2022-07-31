using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OctantUtils
{
    public static Vector2Int TransformOctant(Vector2Int position, int octant) {
        switch (octant) {
            case 0: return new Vector2Int(position.x, position.y);
            case 1: return new Vector2Int(position.y, position.x);
            case 2: return new Vector2Int(position.y, -position.x);
            case 3: return new Vector2Int(position.x, -position.y);
            case 4: return new Vector2Int(-position.x, -position.y);
            case 5: return new Vector2Int(-position.y, -position.x);
            case 6: return new Vector2Int(-position.y, position.x);
            case 7: return new Vector2Int(-position.x, position.y);
            default: return Vector2Int.zero;
        }
    }
}
