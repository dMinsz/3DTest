using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [Header("Move Status")]
    public float MovePower = 10.0f;
    public float RotatePower = 60.0f;

    private Vector3 moveDir;

    private void Update()
    {
        UpdateMove();
        UpdateRoatate();
    }

    public void Move(Vector3 input)
    {
        this.moveDir = input;
    }
    


    public void UpdateRoatate()
    {
        transform.Rotate(Vector3.up, moveDir.x * Time.deltaTime * RotatePower, Space.World);
    }

    public void UpdateMove()
    {
        transform.Translate(Vector3.forward * moveDir.z * Time.deltaTime * MovePower);
    }
}
