using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableMoves : MonoBehaviour {
    [SerializeField]
    private GameObject sqarePrefab;

    [SerializeField]
    private GridManager gridManager;

    private BattleMap battleMap;

    private Dictionary<Vector2Int, int> tileMap = new Dictionary<Vector2Int, int>();

    private int moveRadius = 10;

    // Start is called before the first frame update
    void Start() {
        Vector2Int bounds = new Vector2Int(40, 40);
        battleMap = new BattleMap(bounds);
        Vector2Int playerPos = new Vector2Int(20, 20);

        List<Vector2Int> blocks = new List<Vector2Int>() {
            new Vector2Int(18, 22),
            new Vector2Int(19, 22),
            new Vector2Int(20, 22),
            new Vector2Int(21, 22),
            new Vector2Int(22, 22)
        };
        foreach(Vector2Int block in blocks) {
            Actor wall = EntityFactory.GenerateWall(block);
            battleMap.AddEntity(block, wall);
        }
        
        recursiveSearch(playerPos, playerPos, moveRadius);

        gridManager.initVisuals(bounds);

        gridManager.colorAllRenders(Color.white);
        colorMoveableTiles();
        gridManager.paintSquareColor(blocks, Color.gray);
    }

    private void colorMoveableTiles() {
        foreach(KeyValuePair<Vector2Int, int> pair in tileMap) {
            float tint = (1f / moveRadius) * pair.Value;
            Color color = new Color(0, 0, tint);
            gridManager.paintSquareColor(new List<Vector2Int>() { pair.Key }, color);
        }
    }

    private void recursiveSearch(Vector2Int currentPosition, Vector2Int previousPosition, int moveRadius) {

        // starting out
        if (previousPosition == currentPosition) {
            
            tileMap.Add(currentPosition, 0);
            
            List<Vector2Int> surroundingTiles = GridUtils.GetSurroundingTiles();
            foreach (Vector2Int surroundingTile in surroundingTiles) {
                Vector2Int nextPosition = currentPosition + surroundingTile;
                recursiveSearch(nextPosition, currentPosition, moveRadius);
            }
            return;
        } 

        bool updated = false;

/*        TerrainTile currentTerrainTile = battleMap.GetTerrainAt(currentPosition);
        if(currentTerrainTile == null) {
            return;
        }*/

        int terrainDifficulty = 1;
/*        if (currentTerrainTile.TerrainType == TerrainType.Difficult) {
            terrainDifficulty = 2;
        } else if (currentTerrainTile.TerrainType == TerrainType.Impassable) {
            terrainDifficulty = int.MaxValue - (moveRadius * 10) - 10;
        }*/

        int previousValue = tileMap[previousPosition];
        int travelDistance = previousValue + terrainDifficulty;

        if(travelDistance <= moveRadius) {
            if (!tileMap.ContainsKey(currentPosition)) {

                tileMap.Add(currentPosition, travelDistance);
                updated = true;

            } else {
                int valueInMap = tileMap[currentPosition];
                if(valueInMap > travelDistance) {

                    tileMap[currentPosition] = travelDistance;
                    updated = true;
                }
            }
        }

        if (updated) {
            List<Vector2Int> surroundingTiles = GridUtils.GetSurroundingTiles();
            foreach (Vector2Int surroundingTile in surroundingTiles) {
                Vector2Int nextPosition = currentPosition + surroundingTile;
                recursiveSearch(nextPosition, currentPosition, moveRadius);
            }
        }
    }

    private int GetTileMapValue(Vector2Int position) {

        if (tileMap.ContainsKey(position)) {
            return tileMap[position];
        }
        return int.MaxValue;
    }
}
