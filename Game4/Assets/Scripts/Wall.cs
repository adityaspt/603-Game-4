using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wear;
    public float strength;

    private Material mat;
    [SerializeField]
    private int type;

    [SerializeField]
    private ResourceManager.ResourceType resourceType;



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

        Debug.Log(mat.GetFloat("_Dither"));

        this.type = type;

        switch (type)
        {
            case 0: // dirt
                strength = 0.25f;
                mat.SetColor("_Color", new Color(0.7358f, 0.4797f, 0.2464f)); // brown
                break;
            case 1: // hard dirt
                strength = 0.5f;
                mat.SetColor("_Color", new Color(0.5094f, 0.3457f, 0.1994f)); // brown
                break;
            case 2: // coal
                strength = 0.75f;
                mat.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f)); // brown
                break;
            case 3: // Gold
                strength = 1.0f;
                mat.SetColor("_Color", new Color(1, 1, 0)); // brown
                break;


            case 4: // stone
                strength = 1.0f;
                mat.SetColor("_Color", new Color(0.4245f, 0.4245f, 0.4245f)); // grey
                break;
            case 5: // metal
                strength = 2.0f;
                mat.SetColor("_Color", new Color(0.7835f, 0.7835f, 0.7835f)); // light grey
                break;
            case 6: // Gem
                strength = 3.0f;
                mat.SetColor("_Color", new Color(0, 0.95f, 1)); // light blue
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

        if (type == 2 || type == 3 || type == 5 || type == 6)
        {
            //Code to Spawn the resources on map to be collected by then player
            resourceSpawner.resourceSpawnerInstance.CreateResource(this.transform.position, this.transform.rotation,this.type);
            print("Resource created; type " + this.type);
        }
    }

}
