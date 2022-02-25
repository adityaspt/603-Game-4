using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tile;
    public int seed;

    public int width;
    public int height;

    private Vector2 start;
    private float scale;
    private float cellWidth;
    private List<Vector2> cells;
    public Vector2 currentCell;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").transform;

        Random.seed = seed;
        start = new Vector2(Random.Range(500, 1000), Random.Range(500, 1000));

        scale = tile.transform.localScale.x;
        cellWidth = scale * width;

        currentCell = new Vector2(0, 0);
        cells = new List<Vector2>();
        //cells.Add(new Vector2(currentCell.x, currentCell.y));

        makeAdjacentCells();
    }



    void makeAdjacentCells()
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                Vector2 tryCell = new Vector2(i, j) + currentCell;
                if (!cells.Contains(tryCell))
                {
                    createCellAt(tryCell);
                    cells.Add(tryCell);
                }
            }
        }
    }

    void createCellAt(Vector2 cell)
    {
        for (int i = - width / 2; i < width / 2; i++)
        {
            for (int j = -height / 2; j < height / 2; j++)
            {
                Vector2 pos = cell * cellWidth + new Vector2(i, j) * scale;
                createWallAt(pos);
            }
        }
    }

    void createWallAt(Vector2 pos)
    {
        if (pos.sqrMagnitude > 3)
        {
            Wall wall = Instantiate(tile, pos, Quaternion.identity, transform).GetComponent<Wall>();
            Vector2 stardardGridCoords = start + pos * 0.2f;
            float noiseVal = Mathf.PerlinNoise(stardardGridCoords.x, stardardGridCoords.y);
            Vector2 GemCoords = stardardGridCoords * 5;
            float gemNoise = Mathf.PerlinNoise(GemCoords.x, GemCoords.y);

            if (gemNoise > 0.85f)
            {
                wall.setType(6);
            }
            else
            {
                if (pos.sqrMagnitude < 10 * 10)
                {
                    if (noiseVal < 0.35f)
                        wall.setType(0);
                    else if (noiseVal < 0.65f)
                        wall.setType(1);
                    else if (noiseVal < 0.9f)
                        wall.setType(2);
                    else
                        wall.setType(3);
                }
                else if (pos.sqrMagnitude < 20 * 20)
                {
                    float lerpVal = (pos.magnitude - 10) / 10;
                    lerpVal *= Mathf.PerlinNoise(stardardGridCoords.x * 0.5f, stardardGridCoords.y * 0.5f) / 2;

                    if (noiseVal + lerpVal < 0.5f)
                        wall.setType(0);
                    else if (noiseVal + lerpVal < 1.5f)
                        wall.setType(1);
                    else
                        wall.setType(4);
                }
                else
                {
                    if (noiseVal < 0.5f)
                        wall.setType(1);
                    else if (noiseVal < 0.75f)
                        wall.setType(4);
                    else
                        wall.setType(5);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerCell = new Vector2(Mathf.Round(player.position.x /cellWidth), Mathf.Round(player.position.y / cellWidth));
        if (playerCell != currentCell)
        {
            currentCell = playerCell;
            makeAdjacentCells();
        }
    }
}
