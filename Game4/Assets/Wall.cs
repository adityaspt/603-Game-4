using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wear;
    public float strength;

    private Material mat;
    private int type;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void setType(int type)
    {
        mat = GetComponent<SpriteRenderer>().material;
        mat = new Material(mat);
        mat = GetComponent<SpriteRenderer>().material;
        wear = 0;


        this.type = type;
        switch (type)
        {
            case 0: // dirt
                strength = 0.5f;
                mat.SetColor("_Color", new Color(0.7358f, 0.4797f, 0.2464f)); // brown
                break;
            case 1: // stone
                strength = 1.0f;
                mat.SetColor("_Color", new Color(0.4245f, 0.4245f, 0.4245f)); // brown
                break;
            case 2: // metal
                strength = 2.0f;
                mat.SetColor("_Color", new Color(0.7835f, 0.7835f, 0.7835f)); // brown
                break;

            default:
                break;
        }

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

}
