using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    // singleton
    public static PlayerInfo instance;

    public GameObject       pauseCanvas;
    public Dropbox          dropBoxReference;
    public GameObject       shopCanvas;
    public GameObject       itemCanvas;
    public GameObject       goldCanvas;
    public GameObject       confirmCanvas;
    public GameObject       compass;
    public ResourceManager  resourceManager;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
