using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class PlayerController5: MonoBehaviour
{
    [HideInInspector]
    public enum STATE { None, Jump, Move }

    [SerializeField]
    public STATE state = STATE.None;

    [Header("player Event")]
    public UnityEvent<Vector3> OnMoved;
    public UnityEvent OnFired;
    public UnityEvent<bool> OnRepeatFired;
    //public UnityEvent<Vector2> OnRotated;
    public UnityEvent<Vector2> OnRotatedTurret;
    public UnityEvent OnJumped;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void OnMove(InputValue value)
    {
        Vector3 inputDir = new Vector3();
        inputDir.x = value.Get<Vector2>().x;
        inputDir.z = value.Get<Vector2>().y;

        OnMoved?.Invoke(inputDir);
        //OnRotated?.Invoke(inputDir);
    }

    //private void OnRotate(InputValue value) 
    //{
    //    Vector2 inputDir = new Vector2();
    //    inputDir.x = value.Get<Vector2>().x;
    //    inputDir.y = value.Get<Vector2>().y;

    //    OnRotated?.Invoke(inputDir);
    //}

    private void OnFire()
    {
        OnFired?.Invoke();
        animator.SetTrigger("FireTrigger");
    }

    private void OnRepeatFire(InputValue value)
    {
        OnRepeatFired?.Invoke(value.isPressed);
        animator.SetTrigger("FireTrigger");
    }

    private void OnJump()
    {
        OnJumped?.Invoke();
        animator.SetTrigger("JumpTrigger");
    }

    private void OnRotateTurret(InputValue value)
    {
        OnRotatedTurret?.Invoke(value.Get<Vector2>());
    }

    private void Update()
    {
      
    }

}
