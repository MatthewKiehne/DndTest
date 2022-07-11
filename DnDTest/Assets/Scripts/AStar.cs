using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AStar {

    private static Dictionary<Vector2Int, AStarSquare> possibleSquares = new Dictionary<Vector2Int, AStarSquare>();
    private static HashSet<Vector2Int> usedSquares = new HashSet<Vector2Int>();

    private static int updateSquaresHorizontalBeginIndex = 4;

    private static int adjacentSquareValue = 10;
    private static int horizontalSquareValue = 14;

    private static BattleMap _battleMap;

    private static readonly string blockMovementAttribute = EntityFactory.GenerateTargetAttributeName(EntityFactory.ExtensionName, EntityFactory.GroupMovement, EntityFactory.AttributeMovementImpassible);

    private static HashSet<Vector2Int> _visibleTiles;

    public static List<Vector2Int> getPath(BattleMap battleMap, Vector2Int startPoint, Vector2Int endPoint, int characterSize, HashSet<Vector2Int> visibleTiles) {
        possibleSquares.Clear();
        usedSquares.Clear();
        _visibleTiles = visibleTiles;
        _battleMap = battleMap;

        AStarSquare startingSquare = new AStarSquare();
        startingSquare.setG(0);
        startingSquare.setH(generateH(startPoint, endPoint));
        possibleSquares.Add(startPoint, startingSquare);

        // loop until done
        while (updateMap(endPoint)) { }

        // gets path
        return getPathFromPointToPoint(startPoint, endPoint);
    }

    private static List<Vector2Int> getPathFromPointToPoint(Vector2Int startPoint, Vector2Int endPoint) {

        // this path was not found
        if (!possibleSquares.ContainsKey(endPoint)) {
            return null;
        }

        List<Vector2Int> result = new List<Vector2Int>();
        Dictionary<AStarSquare, Vector2Int> squareToVector = possibleSquares.ToDictionary(x => x.Value, x => x.Key);
        AStarSquare currentSquare = possibleSquares[endPoint];

        while (currentSquare.getParentSquare() != null) {
            Vector2Int currentPosition = squareToVector[currentSquare];
            result.Add(currentPosition);
            currentSquare = currentSquare.getParentSquare();
        }

        return result;
    }

    private static bool updateMap(Vector2Int endPosition) {
        Vector2Int foundSquare = findNextPosition();
        if (foundSquare.Equals(endPosition) || foundSquare == -Vector2Int.one) {
            return false;
        }

        updateTiles(foundSquare, endPosition);
        usedSquares.Add(foundSquare);
        return true;
    }

    private static Vector2Int findNextPosition() {
        Vector2Int result = -Vector2Int.one;
        AStarSquare resultSquare = null;

        foreach (KeyValuePair<Vector2Int, AStarSquare> entry in possibleSquares) {
            if (usedSquares.Contains(entry.Key)) {
                continue;
            }

            if (resultSquare == null) {
                resultSquare = entry.Value;
                result = entry.Key;
            }

            if ((entry.Value.getValue() < resultSquare.getValue()) ||
                entry.Value.getValue() == resultSquare.getValue() && entry.Value.getH() < resultSquare.getH()) {
                resultSquare = entry.Value;
                result = entry.Key;
            }
        }

        return result;
    }

    private static void updateTiles(Vector2Int centerPos, Vector2Int endPosition) {
        AStarSquare centerSquare = getPossibleSquareAt(centerPos);
        if (centerSquare == null) {
            throw new Exception("Unable to find tile to update at: " + centerPos.ToString());
        }

        List<Vector2Int> updateSquares = GridUtils.GetSurroundingTiles();
        for (int i = 0; i < updateSquares.Count; i++) {
            Vector2Int worldUpdatePos = centerPos + updateSquares[i];

            // continue if out of bounds, the tile is used, we can not see it, or it blocks movement
            if (!_battleMap.PositionInBounds(worldUpdatePos) || usedSquares.Contains(worldUpdatePos) 
                || !_visibleTiles.Contains(worldUpdatePos) || _battleMap.DoesEntityWithAttributeExistAtPosition(worldUpdatePos, blockMovementAttribute)) {
                continue;
            }

            int travelValue = i < updateSquaresHorizontalBeginIndex ? adjacentSquareValue : horizontalSquareValue;

            AStarSquare updateSquare = getPossibleSquareAt(worldUpdatePos);
            if (updateSquare == null) {
                updateSquare = createAStarSquare(worldUpdatePos, centerPos, endPosition, travelValue);
                possibleSquares.Add(worldUpdatePos, updateSquare);

            }

            updateAStarSquare(worldUpdatePos, centerPos, travelValue);
        }
    }

    private static AStarSquare getPossibleSquareAt(Vector2Int position) {
        AStarSquare result = null;
        if(possibleSquares.ContainsKey(position)) {
            result = possibleSquares[position];
        }
        return result;
    }

    private static bool updateAStarSquare(Vector2Int targetPos, Vector2Int centerPos, int travelValue) {
        bool result = false;

        AStarSquare targetSquare = getPossibleSquareAt(targetPos);
        AStarSquare centerSquare = getPossibleSquareAt(centerPos);

        if (targetSquare == null || centerSquare == null) {
            throw new Exception("Unable to find possible square");
        }

        if (targetSquare.getG() > centerSquare.getG() + travelValue) {
            targetSquare.setG(centerSquare.getG() + travelValue);
            result = true;
            targetSquare.setParentSquare(centerSquare);
        }
        return result;
    }

    private static AStarSquare createAStarSquare(Vector2Int position, Vector2Int previousPosition, Vector2Int endPosition, int travelValue) {
        AStarSquare previousSquare = getPossibleSquareAt(previousPosition);
        if (previousSquare == null) {
            throw new Exception("Previous position does not exist: " + previousPosition);
        }

        AStarSquare result = new AStarSquare();
        int gValue = previousSquare.getG() + travelValue;
        result.setG(gValue);
        result.setH(generateH(position, endPosition));
        result.setParentSquare(previousSquare);

        return result;
    }

    private static int generateH(Vector2Int currentPos, Vector2Int endPos) {
        Vector2Int diff = currentPos - endPos;
        int[] distanceValues = new int[]{
            Math.Abs(diff.x),
            Math.Abs(diff.y)
        };

        Array.Sort(distanceValues);

        int horizontalValue = distanceValues[0] * horizontalSquareValue;
        int adjacentValue = (distanceValues[1] - distanceValues[0]) * adjacentSquareValue;
        return horizontalValue + adjacentValue;
    }

    private class AStarSquare {
        private int g = 0;
        private int h = 0;
        private int value = 0;
        private AStarSquare parentSquare = null;

        public int getG() {
            return g;
        }
        public void setG(int g) {
            this.g = g;
            updateValue();
        }

        public int getH() {
            return h;
        }
        public void setH(int h) {
            this.h = h;
            updateValue();
        }

        public int getValue() {
            return this.value;
        }

        public void setParentSquare(AStarSquare parentSquare) {
            this.parentSquare = parentSquare;
        }

        public AStarSquare getParentSquare() {
            return parentSquare;
        }

        private void updateValue() {
            this.value = g + h;
        }
    }
}
