using Antlr4.Runtime;
using Calculator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapController : MonoBehaviour {

    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private GameObject squarePrefab;

    private Vector2Int bounds = new Vector2Int(100, 100);
    private BattleMap battleMap;

    private int viewRange = 15;
    private int moveRange = 5;
    private Actor playerEntity;

    Dictionary<Actor, HashSet<Vector2Int>> playerViews = new Dictionary<Actor, HashSet<Vector2Int>>();
    Dictionary<Actor, Dictionary<Vector2Int, int>> playerMoveRange = new Dictionary<Actor, Dictionary<Vector2Int, int>>();
    private Dictionary<Vector2Int, SpriteRenderer> entityVisuals = new Dictionary<Vector2Int, SpriteRenderer>();

    private Vector2Int lastMousePostion = Vector2Int.zero;
    private List<Vector2Int> playerPath = new List<Vector2Int>();
    private List<GameObject> pathGameObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start() {


        const string exp = "5+2*3";
        AntlrInputStream inputStream = new AntlrInputStream(exp);
        CalculatorLexer lexer = new CalculatorLexer(inputStream);

        CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
        CalculatorParser parser = new CalculatorParser(commonTokenStream);

        CustomVisitor customVisitor = new CustomVisitor();
        double result = customVisitor.Visit(parser.start());

        Debug.Log(result);




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

        Dictionary<Vector2Int, int> moveArea = AvailableMoves.GetAvailableMoves(battleMap, playerPositon, moveRange * 2);
        playerMoveRange.Add(playerEntity, moveArea);

        UpdateView(playerEntity);
        gridManager.initVisuals(bounds);
        gridManager.colorAllRenders(Color.black);
        gridManager.paintSquareColor(new List<Vector2Int>(playerViews[playerEntity]), Color.green);
        foreach(KeyValuePair<Vector2Int,int> thing in moveArea) {
            Color c = new Color(0, 0, (1f / moveRange) * thing.Value);
            gridManager.paintSquareColor(new List<Vector2Int>() { thing.Key }, c);
        }

        List<Vector2Int> path = AStar.getPath(battleMap, playerEntity.Position, playerEntity.Position + new Vector2Int(-30,0), 1, playerViews[playerEntity]);

        if(path != null) {
            // a path does not exist
            foreach (Vector2Int step in path) {
                GameObject go = GameObject.Instantiate(squarePrefab);
                go.transform.position = (Vector2)step;
                go.GetComponent<SpriteRenderer>().sortingOrder = 10;
                pathGameObjects.Add(go);
            }
        }
    }

    public void Update() {

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int roundedCameraPosition = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y));
        
        if(roundedCameraPosition != lastMousePostion && playerMoveRange[playerEntity].ContainsKey(roundedCameraPosition) && battleMap.PositionInBounds(roundedCameraPosition)) {

            playerPath = AStar.getPath(battleMap, playerEntity.Position, roundedCameraPosition, 1, playerViews[playerEntity]);
            if(playerPath != null) {
                foreach (GameObject go in pathGameObjects) {
                    Destroy(go);
                }
                pathGameObjects.Clear();

                foreach (Vector2Int step in playerPath) {
                    GameObject go = GameObject.Instantiate(squarePrefab);
                    go.transform.position = (Vector2)step;
                    go.GetComponent<SpriteRenderer>().sortingOrder = 10;
                    pathGameObjects.Add(go);
                }
            }

            lastMousePostion = roundedCameraPosition;
        }

        Vector2Int updatePositon = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerViews[playerEntity].Contains(roundedCameraPosition) && battleMap.PositionInBounds(roundedCameraPosition)) {
            updatePositon = roundedCameraPosition;
            playerEntity.Position = updatePositon;
        }

        if (updatePositon != Vector2Int.zero) {
            UpdateView(playerEntity);
            gridManager.colorAllRenders(Color.black);
            playerMoveRange[playerEntity].Clear();
            playerMoveRange[playerEntity] = AvailableMoves.GetAvailableMoves(battleMap, playerEntity.Position, moveRange * 2);
            
            gridManager.colorAllRenders(Color.black);
            gridManager.paintSquareColor(new List<Vector2Int>(playerViews[playerEntity]), Color.cyan);
            foreach (KeyValuePair<Vector2Int, int> moveTile in playerMoveRange[playerEntity]) {
                if (playerViews[playerEntity].Contains(moveTile.Key)) {
                    Color paintColor = Color.green;
                    if(moveTile.Value > moveRange) {
                        paintColor = Color.yellow;
                    } 
                    gridManager.paintSquareColor(new List<Vector2Int>() { moveTile.Key }, paintColor);
                }
            }
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
