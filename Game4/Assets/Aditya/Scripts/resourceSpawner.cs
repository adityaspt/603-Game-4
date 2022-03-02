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

    [Range(0, 10f)]
    public float resourceDropIntensity;
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

        // Create color intensities for HDR
        float powMax = Mathf.Pow(2, resourceDropIntensity);
        float powMin = Mathf.Pow(2, resourceDropIntensity / 3f);

        switch (resType)  {
           
            case 3: // Gold
                    //Set tags
                tempRes.tag = "Gold";
                mat.SetColor("_Color", new Color(1f * powMax, 0.58f * powMax, 0)); // yellow
                break;

            case 2: // coal
                    //Set tags
                tempRes.tag = "Coal";
                mat.SetColor("_Color", new Color(0, 0.02206345f * 1.1f, 0.07547164f * 1.1f)); // dark purple
                break;

            case 5: // metal
                    //Set tags
                tempRes.tag = "Metal";
                mat.SetColor("_Color", new Color(0f, 0.1037736f * powMax, 0.01831298f * powMax)); // dark green
                break;
            case 6: // Gem
                //Set tags
                tempRes.tag = "Gems";
                mat.SetColor("_Color", new Color(0, 0.95f * powMax, 1f * powMax)); // light blue
                break;

            default:
                print("resource type did not work");
                break;
        }

    }
}
