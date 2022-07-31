using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridUtils {
    public static List<Vector2Int> GetSurroundingTiles() {
        return new List<Vector2Int>{
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
            new Vector2Int(-1, -1),
            new Vector2Int(1, -1),
            new Vector2Int(-1, 1),
            new Vector2Int(1, 1)
        };
    }
}
