using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;
public class CameraTarget : NetworkBehaviour
{
    public Camera cam;

    private void Start()
    {
        if (Object.HasInputAuthority)
        {
            print(111);
            return;
        }

        else
        {

            print(222);
            cam.enabled = false;
        }
    }
}
