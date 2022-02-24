using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wear;
    public float strength;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        mat = new Material(mat);
        mat = GetComponent<SpriteRenderer>().material;
        wear = 0;
    }

    public bool Drill(float time)
    {
        wear += time;

        if (wear < strength)
        {
            mat.SetFloat("_Dither", (wear / strength) * 3);
            return false;
        }
        else
        {
            Break();
            return true;
        }
    }

    void Break()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
