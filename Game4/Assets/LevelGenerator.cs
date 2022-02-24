using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tile;

    public int width;
    public int height;
    public Vector2 start;

    // Start is called before the first frame update
    void Start()
    {
        float scale = tile.transform.localScale.x;
        for (int i = - width / 2; i < width / 2; i++)
        {
            for (int j = -height / 2; j < height / 2; j++)
            {
                Vector2 pos = start + new Vector2(i, j) * scale;
                Vector2 stardardGridCoords = new Vector2(i / width + 0.5f, j / height + 0.5f);
                float noiseVal = Mathf.PerlinNoise(stardardGridCoords.x, stardardGridCoords.y);

                if ((pos-start).sqrMagnitude > 3)
                    Instantiate(tile, pos, Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
