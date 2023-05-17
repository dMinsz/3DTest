using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class PlayerController4: MonoBehaviour
{
    private enum STATE { None, Jump, Move }

    [SerializeField]
    private STATE state = STATE.None;
    private Rigidbody rgd;

    //public Camera camera;
    [HideInInspector]
    public Vector3 moveDir;

    [Header("Tank Parts")]
    public GameObject tankTurret;
    public GameObject BulletFrepab;

    [Header("Tank Status")]
    public float MovePower = 10.0f;
    public float RotatePower = 60.0f;
    public float JumpPower = 5.0f;
    public float RepeatTime = 1.0f;

    private Vector3 orgBackCam;
    private bool ChangeCam = false;
    IEnumerator FireRoutine()
    {
        while (true)
        {
            Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
            Instantiate(BulletFrepab, shoot.position, shoot.rotation);
            yield return new WaitForSeconds(RepeatTime);     // n초간 시간지연

        }
    }

    private void CoroutineStart()
    {
        StartCoroutine(FireRoutine());
    }

    private void CoroutineStop()
    {
        StopAllCoroutines();        // 모든 코루틴 종료
    }


    private void Awake()
    {
        //camera = GetComponent<Camera>();
        rgd = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        Move();
        Roatate();
    }

    private void Move()
    {
        transform.Translate(Vector3.forward* moveDir.z * Time.deltaTime * MovePower); // 위와 같다.
    }

    private void jump() 
    {
        if (state != STATE.Jump) //&& state != STATE.Move) // 점프 , 움직이는 중에는 점프 불가
        {
            rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            state = STATE.Jump;
        }
    }

    private void Roatate()
    {
        transform.Rotate(Vector3.up, moveDir.x * Time.deltaTime * RotatePower , Space.World);
    }


    //input system 이용하여 움직이기
    private void OnMove(InputValue input)
    {
        //Vector3 inputDir = new Vector3();
        moveDir.x = input.Get<Vector2>().x;
        moveDir.z = input.Get<Vector2>().y;
    }


    private void OnJump(InputValue input)
    {
        jump();
    }

    private void OnFire(InputValue input) 
    {
        Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
        GameObject obj = Instantiate(BulletFrepab, shoot.position , shoot.rotation);
    }

    private void OnRepeatFire(InputValue input) 
    {
        if (input.isPressed)
        {
            CoroutineStart();
        }
        else 
        {
            CoroutineStop();
        }
    }

    private void OnFocus(InputValue input)
    {
        //if (!ChangeCam)
        //{
        //    Transform shot = transform.Find("ShotPos").transform;
        //    Camera cam = transform.Find("BackCamera").GetComponent<Camera>();
        //    orgBackCam = cam.transform.localPosition;
        //    cam.transform.localPosition = shot.localPosition;
        //    ChangeCam = true;

        //}
        //else 
        //{
        //    //Transform shot = transform.Find("ShotPos").transform;
        //    Camera cam = transform.Find("BackCamera").GetComponent<Camera>();
        //    cam.transform.localPosition = orgBackCam;
        //    ChangeCam = false;

        //}
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


    private void OnRotateTurret(InputValue input)
    {
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {   // 땅에 닿아야지 점프 가능하게 함
            //Debug.Log(name+"이 땅에닿았다.");
            state = STATE.None;
        }
    }



}
