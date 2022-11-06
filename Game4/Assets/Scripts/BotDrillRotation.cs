using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDrillRotation : MonoBehaviour
{
    [SerializeField] private Transform aimTransform;
    [SerializeField] private GameObject player;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = player.GetComponent<BotScript>().movement;
    }

    // Update is called once per frame
    void Update()
    {
        aimTransform.right = direction;
    }
}
