using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap {
    private Dictionary<Vector2Int, List<Actor>> tileToEntities = new Dictionary<Vector2Int, List<Actor>>();
    public Vector2Int bounds { get; }

    public BattleMap(Vector2Int bounds) {
        this.bounds = bounds;
    }

    public bool PositionInBounds(Vector2Int position) {
        return position.x >= 0 && position.x < bounds.x && position.y >= 0 && position.y < bounds.y;
    }

    public List<Actor> GetEntitiesAt(Vector2Int position) {
        if (!PositionInBounds(position)) {
            throw new Exception("Position out of bounds of battleMap");
        }

        if (tileToEntities.ContainsKey(position)) {
            return tileToEntities[position];
        } else {
            return new List<Actor>();
        }
    }

    public bool ContainsEntity(Vector2Int position, Actor entity) {
        if (!PositionInBounds(position)) {
            throw new Exception("Position out of bounds of battleMap");
        }

        if (!tileToEntities.ContainsKey(position)) {
            return false;
        }

        return tileToEntities[position].Contains(entity);
    }

    public void AddEntity(Vector2Int position, Actor entity) {
        if (!PositionInBounds(position)) {
            throw new Exception("Position out of bounds of battleMap");
        }
        if (ContainsEntity(position, entity)) {
            throw new Exception("Entity already exists on that positon on the battleMap.");
        }

        List<Actor> entityList = null;
        if (tileToEntities.ContainsKey(position)) {
            entityList = tileToEntities[position];
        } else {
            entityList = new List<Actor>();
            tileToEntities.Add(position, entityList);
        }

        entityList.Add(entity);
    }

    public void RemoveEntity(Vector2Int position, Actor entity) {
        if (!PositionInBounds(position)) {
            throw new Exception("Position out of bounds of battleMap");
        }
        if (!ContainsEntity(position, entity)) {
            throw new Exception("Entity does not exists on that positon on the battleMap.");
        }

        tileToEntities[position].Remove(entity);

        if (tileToEntities[position].Count == 0) {
            tileToEntities.Remove(position);
        }
    }

    public void MoveEntity(Actor actor, Vector2Int newPosition) {
        if (!PositionInBounds(newPosition)) {
            throw new Exception("Position out of bounds of battleMap");
        }

        Vector2Int currentPosition = actor.GetPosition();

        RemoveEntity(currentPosition, actor);
        AddEntity(newPosition, actor);
    }

    public bool DoesEntityWithAttributeExistAtPosition(Vector2Int position, string attributeName) {
        List<Actor> actors = GetEntitiesAt(position);
        return actors.Exists(actor => actor.GetFeaturesByAttribute(attributeName).Count != 0);
    }
}
