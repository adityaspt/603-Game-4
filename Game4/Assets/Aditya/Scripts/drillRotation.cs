using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drillRotation : MonoBehaviour
{
    [SerializeField]
    private Transform aimTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - aimTransform.position);
        aimDirection.z = 0;
        aimDirection = aimDirection.normalized;
        aimTransform.right = aimDirection;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition,Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
