using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class CameraSetting : NetworkBehaviour
{
    public GameObject cameraHolder;
    public Vector3 offset;

    public void OnStartAuthority()
    {
        cameraHolder.SetActive(true);
    }
    public void Update()
    {
        if (SceneManager.GetActiveScene().name == "TankMultiplayer")
        {
        cameraHolder.transform.position = transform.position + offset;
        }
    }
}
