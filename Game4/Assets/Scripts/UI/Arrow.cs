using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{

    public Image arrow;
    public playerController player;
    public Transform hub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float angle = Mathf.Rad2Deg * Mathf.Atan((player.transform.position.x - hub.position.x) / (player.transform.position.y - hub.transform.position.y));

        if (player.transform.position.y < hub.position.y)
        {
            arrow.gameObject.transform.eulerAngles = new Vector3(0, 0, -angle);
        }
        else
        {
            arrow.gameObject.transform.eulerAngles = new Vector3(0, 0, -angle + 180);
        }
       
        
    }
}
