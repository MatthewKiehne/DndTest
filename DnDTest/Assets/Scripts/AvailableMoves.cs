using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AvailableMoves {

    public static Dictionary<Vector2Int, int> GetAvailableMoves(BattleMap battleMap, Vector2Int startingPosition, int moveRange) {
        Dictionary<Vector2Int, int> tileMap = new Dictionary<Vector2Int, int>();
        recursiveSearch(tileMap, battleMap, startingPosition, startingPosition, moveRange);
        return tileMap;
    }

    private static void recursiveSearch(Dictionary<Vector2Int, int> tileMap, BattleMap battleMap, Vector2Int currentPosition, Vector2Int previousPosition, int moveRadius) {

        // starting out
        if (previousPosition == currentPosition) {

            tileMap.Add(currentPosition, 0);

            List<Vector2Int> surroundingTiles = GridUtils.GetSurroundingTiles();
            foreach (Vector2Int surroundingTile in surroundingTiles) {
                Vector2Int nextPosition = currentPosition + surroundingTile;
                recursiveSearch(tileMap, battleMap, nextPosition, currentPosition, moveRadius);
            }
            return;
        }

        // return if the block can not be moved through
        string blockMovementAttribute = EntityFactory.GenerateTargetAttributeName(EntityFactory.ExtensionName, EntityFactory.GroupMovement, EntityFactory.AttributeMovementImpassible);
        if (battleMap.DoesEntityWithAttributeExistAtPosition(currentPosition, blockMovementAttribute)) {
            return;
        }

        bool updated = false;

        int terrainDifficulty = 1;

        int previousValue = tileMap[previousPosition];
        int travelDistance = previousValue + terrainDifficulty;

        if (travelDistance <= moveRadius) {
            if (!tileMap.ContainsKey(currentPosition)) {

                tileMap.Add(currentPosition, travelDistance);
                updated = true;

            } else {
                int valueInMap = tileMap[currentPosition];
                if (valueInMap > travelDistance) {

                    tileMap[currentPosition] = travelDistance;
                    updated = true;
                }
            }
        }

        if (updated) {
            List<Vector2Int> surroundingTiles = GridUtils.GetSurroundingTiles();
            foreach (Vector2Int surroundingTile in surroundingTiles) {
                Vector2Int nextPosition = currentPosition + surroundingTile;
                recursiveSearch(tileMap, battleMap, nextPosition, currentPosition, moveRadius);
            }
        }
    }
}
