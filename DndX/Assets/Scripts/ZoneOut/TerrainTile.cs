using UnityEngine;

public class TerrainTile
{
    public TerrainType TerrainType { get; set; }

    public TerrainTile(TerrainType terrainType) {
        TerrainType = terrainType;
    }
}
