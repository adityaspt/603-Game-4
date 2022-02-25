using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tile;

    public int width;
    public int height;
    private Vector2 start;

    // Start is called before the first frame update
    void Start()
    {
        start = new Vector2(Random.Range(500, 1000), Random.Range(500, 1000));

        float scale = tile.transform.localScale.x;
        for (int i = - width / 2; i < width / 2; i++)
        {
            for (int j = -height / 2; j < height / 2; j++)
            {
                Vector2 pos = new Vector2(i, j) * scale;

                if (pos.sqrMagnitude > 3)
                {
                    Wall wall = Instantiate(tile, pos, Quaternion.identity, transform).GetComponent<Wall>();
                    Vector2 stardardGridCoords = start + pos * 0.2f;
                    float noiseVal = Mathf.PerlinNoise(stardardGridCoords.x, stardardGridCoords.y);

                    if (noiseVal < 0.5f)
                        wall.setType(0);
                    else if (noiseVal < 0.75f)
                        wall.setType(1);
                    else
                        wall.setType(2);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
