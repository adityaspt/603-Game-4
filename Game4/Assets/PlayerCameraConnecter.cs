using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraConnecter : MonoBehaviour
{
    bool notDone = true;

    // Update is called once per frame
    void Update()
    {
        if (playerController.localController != null && notDone)
        {
            GetComponent<CinemachineVirtualCamera>().Follow = playerController.localController.transform;
            notDone = false;
            this.enabled = false;
        }
    }
}
