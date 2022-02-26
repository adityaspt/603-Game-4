using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceSpawner : MonoBehaviour
{

    public static resourceSpawner resourceSpawnerInstance;

    private void Awake()
    {
        resourceSpawnerInstance = this;
    }

    [SerializeField]
    public GameObject blankResourcePrefab;

    [SerializeField]
    Transform ParentOfResources;

    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateResource(Vector3 position,Quaternion rotation,int resType)
    {
       GameObject tempResource=Instantiate(blankResourcePrefab, position, rotation, ParentOfResources);
        setResourceType(tempResource,resType);
    }

    void setResourceType(GameObject tempRes,int resType)
    {
        

        //set material
        mat = tempRes.GetComponent<SpriteRenderer>().material;
       // mat = new Material(mat);

        switch (resType)  {
           
            case 3: // Gold
                    //Set tags
                tempRes.tag = "Gold";
                mat.SetColor("_Color", new Color(1, 1, 0)); // brown
                break;

            case 2: // coal
                    //Set tags
                tempRes.tag = "Coal";
                mat.SetColor("_Color", new Color(0.3f, 0.3f, 0.3f)); // brown
                break;

            case 5: // metal
                    //Set tags
                tempRes.tag = "Metal";
                mat.SetColor("_Color", new Color(0.7835f, 0.7835f, 0.7835f)); // light grey
                break;
            case 6: // Gem
                //Set tags
                tempRes.tag = "Gems";
                mat.SetColor("_Color", new Color(0, 0.95f, 1)); // light blue
                break;

            default:
                print("resource type did not work");
                break;
        }

    }
}
