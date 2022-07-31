using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

    [SerializeField]
    private GameObject squarePrefab;

    private SpriteRenderer[] renderers;

    private Vector2Int bounds = Vector2Int.zero;

    public void initVisuals(Vector2Int bounds) {

        this.bounds = bounds;
        renderers = new SpriteRenderer[bounds.x * bounds.y];

        for (int i = 0; i < renderers.Length; i++) {
            int y = i / bounds.x;
            int x = i % bounds.x;

            GameObject go = Instantiate(squarePrefab);
            go.transform.position = new Vector2(x, y);
            go.transform.name = x + "," + y;

            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            this.renderers[i] = spriteRenderer;
        }
    }

    public void destoryVisuals() {
        foreach(SpriteRenderer renderer in renderers) {
            Destroy(renderer.gameObject);
        }
        renderers = null;
        bounds = Vector2Int.zero;
    }

    public void paintSquareColor(IEnumerable<Vector2Int> positions, Color color) {
        foreach (Vector2Int pos in positions) {
            SpriteRenderer renderer = getRenderer(pos);
            renderer.color = color;
        }
    }

    public void colorAllRenders(Color color) {
        foreach (SpriteRenderer renderer in renderers) {
            renderer.color = color;
        }
    }

    private SpriteRenderer getRenderer(Vector2Int position) {
        int index = (position.y * bounds.x) + position.x;
        return this.renderers[index];
    }
}
