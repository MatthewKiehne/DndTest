using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class View {

    public static List<Vector2Int> GetTilesInView(Vector2Int viewLocation, int viewRange, BattleMap battleMap) {

        List<Vector2Int> visibleTiles = new List<Vector2Int>();
        int sliceCount = 8;

        string viewBlockAttribute = EntityFactory.GenerateTargetAttributeName(EntityFactory.Extension.Name, EntityFactory.Group.Visability, EntityFactory.Attribute.Visability.Block);

        for (int slice = 0; slice < sliceCount; slice++) {
            List<Vector2Int> octant = new List<Vector2Int>();
            List<Vector2Int> simulatedOctant = new List<Vector2Int>();
            List<ShadowLine> shadows = new List<ShadowLine>();


            for (int row = 1; row < viewRange; row++) {
                for (int column = 0; column <= row; column++) {

                    Vector2Int pos = new Vector2Int(column, row);
                    Vector2Int transformedVector = OctantUtils.TransformOctant(pos, slice) + viewLocation;
                    octant.Add(transformedVector);
                    simulatedOctant.Add(pos);

                    if(!battleMap.PositionInBounds(transformedVector)){
                        break;
                    }

                    ShadowLine shadow = projectTile(pos);
                    bool hidden = false;
                    foreach (ShadowLine s in shadows) {
                        if (s.contains(shadow)) {
                            hidden = true;
                            break;
                        }
                    }

                    if(!hidden) {
                        visibleTiles.Add(transformedVector);

                        // adds the shadow if the tile is visible and has a condition that blocks vision
                        bool blocksVision = battleMap.DoesEntityWithAttributeExistAtPosition(transformedVector, viewBlockAttribute);
                        if (blocksVision) {
                            addShadowLine(shadows, shadow);
                        }
                    }
                }
            }
        }

        return visibleTiles;
    }

    private static void addShadowLine(List<ShadowLine> currentShadowLines, ShadowLine newShadowLine) {

        // findes the index where the new shadow should go
        int index = 0;
        for (; index < currentShadowLines.Count; index++) {
            if (currentShadowLines[index].StartSlope <= newShadowLine.StartSlope) {
                break;
            }
        }

        // checks if overlaps with previous shadow
        ShadowLine overlapPrevious = null;
        if (index > 0 &&
            currentShadowLines[index - 1].EndSlope < newShadowLine.StartSlope &&
            !currentShadowLines[index - 1].contains(newShadowLine)) {

            overlapPrevious = currentShadowLines[index - 1];
        }

        // check if overlaps with next shadow
        ShadowLine overlapNext = null;
        if (index < currentShadowLines.Count &&
            currentShadowLines[index].StartSlope > newShadowLine.EndSlope &&
            !currentShadowLines[index].contains(newShadowLine)) {

            overlapNext = currentShadowLines[index];
        }

        if (overlapNext != null) {
            if (overlapPrevious != null) {
                // overlaps with both
                overlapPrevious.EndSlope = overlapNext.EndSlope;
                currentShadowLines.RemoveAt(index);
            } else {
                // overlaps with the next one
                overlapNext.StartSlope = newShadowLine.StartSlope;
            }
        } else {
            if (overlapPrevious != null) {
                // overlaps with the previous
                overlapPrevious.EndSlope = newShadowLine.EndSlope;
            } else {
                // does not overlap with anything
                currentShadowLines.Insert(index, newShadowLine);
            }
        }
    }

    private static ShadowLine projectTile(Vector2 position) {
        // finds the slop of the the corners
        // NOTE: we add one to the column so we do not devide by zero. ie. the colums should be +0 and + 1

        float topLeftSlope = 0;
        if (position.x == 0) {
            // avoids a devide by zero
            topLeftSlope = 9999;
        } else {
            topLeftSlope = (position.y + 1f) / (position.x);
        }
        float bottomRight = position.y / (position.x + 1f);
        return new ShadowLine(topLeftSlope, bottomRight);
    }


    private class ShadowLine {

        public float StartSlope { get; set; }
        public float EndSlope { get; set; }
        public ShadowLine(float startSlop, float endSlope) {
            StartSlope = startSlop;
            EndSlope = endSlope;
        }

        public bool contains(ShadowLine otherLine) {
            return StartSlope >= otherLine.StartSlope && EndSlope <= otherLine.EndSlope;
        }
    }
}
