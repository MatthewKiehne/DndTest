using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapController : MonoBehaviour {

    [SerializeField]
    private GridManager gridManager;

    private Vector2Int bounds = new Vector2Int(100, 100);
    private BattleMap battleMap;

    private int viewRange = 5;
    // private Vector2Int playerPositon;
    private Actor playerEntity;

    Dictionary<Actor, HashSet<Vector2Int>> playerViews = new Dictionary<Actor, HashSet<Vector2Int>>();
    Dictionary<Vector2Int, SpriteRenderer> entityVisuals = new Dictionary<Vector2Int, SpriteRenderer>();


    // Start is called before the first frame update
    void Start() {
        battleMap = new BattleMap(bounds);

        List<Vector2Int> walls = new List<Vector2Int>() {
            new Vector2Int(50,50),
            new Vector2Int(50,51),
            new Vector2Int(50,52),
            new Vector2Int(50,53),
            new Vector2Int(50,54),
            new Vector2Int(50,55),
            new Vector2Int(50,56),
            new Vector2Int(50,57)
        };

        foreach (Vector2Int wallPosition in walls) {
            Actor wall = EntityFactory.GenerateWall(wallPosition);
            battleMap.AddEntity(wallPosition, wall);
        }

        Vector2Int playerPositon = new Vector2Int(47, 47);
        playerEntity = new Actor(playerPositon);
        playerViews.Add(playerEntity, new HashSet<Vector2Int>());

        UpdateView(playerEntity);
        gridManager.initVisuals(bounds);
        gridManager.colorAllRenders(Color.black);
        gridManager.paintSquareColor(new List<Vector2Int>(playerViews[playerEntity]), Color.green);
    }

    public void Update() {
        Vector2Int updatePositon = Vector2Int.zero;

        if(Input.GetKeyDown(KeyCode.W)) {
            updatePositon = Vector2Int.up;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            updatePositon = Vector2Int.right;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            updatePositon = Vector2Int.down;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            updatePositon = Vector2Int.left;
        }

        if (updatePositon != Vector2Int.zero) {
            playerEntity.Position = playerEntity.Position + updatePositon;
            UpdateView(playerEntity);
            gridManager.colorAllRenders(Color.black);

            Debug.Log(playerEntity.Position);
            gridManager.colorAllRenders(Color.black);
            gridManager.paintSquareColor(new List<Vector2Int>(playerViews[playerEntity]), Color.green);
        }
    }

    private void UpdateView(Actor entityViewing) {

        List<Vector2Int> inView = View.GetTilesInView(entityViewing.Position, viewRange, battleMap);
        HashSet<Vector2Int> mapInView = new HashSet<Vector2Int>(inView);

        //remove all the old tiles
        List<Vector2Int> tileToRemove = new List<Vector2Int>();

        foreach (Vector2Int tile in playerViews[entityViewing]) {
            if (!mapInView.Contains(tile)) {
                tileToRemove.Add(tile);
            }
        }

        foreach (Vector2Int tile in tileToRemove) {
            if (entityVisuals.ContainsKey(tile)) {
                Destroy(entityVisuals[tile].gameObject);
                entityVisuals.Remove(tile);
            }

            playerViews[entityViewing].Remove(tile);
        }

       

        // add the new tiles
        foreach (Vector2Int tile in inView) {

            // only display it if it is new
            if (playerViews[entityViewing].Contains(tile) || entityVisuals.ContainsKey(tile)) {
                continue;
            }

            playerViews[entityViewing].Add(tile);
            DisplayEntityOnTile(tile);
        }
    }

    private void DisplayEntityOnTile(Vector2Int tile) {
        string tokenAttribute = EntityFactory.GenerateTargetAttributeName(EntityFactory.ExtensionName, EntityFactory.GroupVisability, EntityFactory.AttributeVisabilityToken);

        // adds the entity token;
        if (battleMap.DoesEntityWithAttributeExistAtPosition(tile, tokenAttribute)) {

            foreach (Actor entity in battleMap.GetEntitiesAt(tile)) {

                List<Feature> tokenAttributes = entity.GetConditionsWithAttribute(tokenAttribute);

                if (tokenAttributes.Count != 0) {

                    GameObject go = new GameObject();
                    go.transform.position = new Vector3(tile.x, tile.y, 0);

                    SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
                    Sprite sprite = TokenFactory.GetToken(tokenAttributes[0].StringValue);
                    sr.sprite = sprite;
                    sr.sortingOrder = 1;

                    entityVisuals.Add(tile, sr);
                }
            }
        }
    }
}
