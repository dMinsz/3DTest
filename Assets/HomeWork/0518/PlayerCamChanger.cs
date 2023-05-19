using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]

public class PlayerCamChanger : MonoBehaviour
{
    private bool ChangeCam = false;


    public void Focus()
    {
        if (!ChangeCam)
        {
            var BackCam = transform.Find("BackCam").GetComponent<CinemachineVirtualCamera>();
            BackCam.Priority = 0;
            ChangeCam = true;
        }
        else
        {
            var BackCam = transform.Find("BackCam").GetComponent<CinemachineVirtualCamera>();
            BackCam.Priority = 2;
            ChangeCam = false;
        }

    }
}
