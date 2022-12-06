using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float wear;
    public float strength;

    public ParticleSystem wallVFX;

    private Material mat;
    [SerializeField]
    private int type;

    [SerializeField]
    private ResourceManager.ResourceType resourceType;

    bool broken = false;

    // Start is called before the first frame update
    void Start()
    {
        wallVFX.gameObject.SetActive(false);

        chooseType();
    }

    void chooseType()
    {
        // set the type for this block
        Vector2 stardardGridCoords = LevelGenerator.start + (Vector2)transform.position * 0.2f;
        float noiseVal = Mathf.PerlinNoise(stardardGridCoords.x, stardardGridCoords.y);
        Vector2 GemCoords = stardardGridCoords * 5;
        float gemNoise = Mathf.PerlinNoise(GemCoords.x, GemCoords.y);

        if (gemNoise > 0.85f)
        {
            setType(6);
        }
        else
        {
            if (transform.position.sqrMagnitude < 10 * 10)
            {
                if (noiseVal < 0.35f)
                    setType(0);
                else if (noiseVal < 0.65f)
                    setType(1);
                else if (noiseVal < 0.9f)
                    setType(2);
                else
                    setType(3);
            }
            else if (transform.position.sqrMagnitude < 20 * 20)
            {
                float lerpVal = (transform.position.magnitude - 10) / 10;
                lerpVal *= Mathf.PerlinNoise(stardardGridCoords.x * 0.5f, stardardGridCoords.y * 0.5f) / 2;

                if (noiseVal + lerpVal < 0.5f)
                    setType(0);
                else if (noiseVal + lerpVal < 1.5f)
                    setType(1);
                else
                    setType(4);
            }
            else
            {
                if (noiseVal < 0.5f)
                    setType(1);
                else if (noiseVal < 0.75f)
                    setType(4);
                else
                    setType(5);
            }
        }
    }

    public void setType(int type)
    {
        mat = GetComponent<SpriteRenderer>().material;
        mat = new Material(mat);
        mat = GetComponent<SpriteRenderer>().material;
        wear = 0;

        var main = wallVFX.main;

        this.type = type;

        switch (type)
        {
            case 0: // dirt
                strength = 0.25f;
                mat.SetColor("_Color", new Color(0.7358f, 0.4797f, 0.2464f)); // brown
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.7358f, 0.4797f, 0.2464f));
                break;
            case 1: // hard dirt
                strength = 0.5f;
                mat.SetColor("_Color", new Color(0.5094f, 0.3457f, 0.1994f)); // brown
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.5094f, 0.3457f, 0.1994f));
                break;
            case 2: // coal
                strength = 0.75f;
                mat.SetColor("_Color", new Color(0.245f, 0.245f, 0.245f)); // brown
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.5094f, 0.3457f, 0.1994f));
                break;
            case 3: // Gold
                strength = 1.0f;
                mat.SetColor("_Color", new Color(1, 1, 0)); // brown
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(1, 1, 0));
                break;


            case 4: // stone
                strength = 1.0f;
                mat.SetColor("_Color", new Color(0.4245f, 0.4245f, 0.4245f)); // grey
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0.4245f, 0.4245f, 0.4245f));
                break;
            case 5: // metal
                strength = 2.0f;
                mat.SetColor("_Color", new Color(1f, 0f, 0.03997374f)); // red
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(1f, 0f, 0.03997374f));
                break;
            case 6: // Gem
                strength = 3.0f;
                mat.SetColor("_Color", new Color(0, 0.95f, 1)); // light blue
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(0, 0.95f, 1));
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
            transform.parent.GetComponent<Chunck>().WearChild(transform.GetSiblingIndex(), wear);
            return false;
        }
        else
        {
            transform.parent.GetComponent<Chunck>().BreakChild(transform.GetSiblingIndex());
            return true;
        }
    }

    public void Wear(float w)
    {
        wear = w;

        if (wear < strength)
            mat.SetFloat("_Dither", (wear / strength) * 3);

    }

    public void Break()
    {
        if (broken)
            return;

        broken = true;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        if (type == 2 || type == 3 || type == 5 || type == 6)
        {
            //Code to Spawn the resources on map to be collected by then player
            resourceSpawner.resourceSpawnerInstance.CreateResource(this.transform.position + new Vector3(0, 0, 0.5f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)),this.type);
          //  print("Resource created; type " + this.type);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enable block particle system if player is drilling current block
        if(collision.tag == "Drill")
        {
            Drill drill = collision.GetComponent<Drill>();

            if (drill.isDrilling)
            {
                wallVFX.gameObject.SetActive(true);
            }
            else
            {
                wallVFX.gameObject.SetActive(false);
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Drill")
        {
            wallVFX.gameObject.SetActive(false);
        }
    }

}
