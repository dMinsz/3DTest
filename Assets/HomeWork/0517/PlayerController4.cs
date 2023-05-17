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
            yield return new WaitForSeconds(RepeatTime);     // n�ʰ� �ð�����

        }
    }

    private void CoroutineStart()
    {
        StartCoroutine(FireRoutine());
    }

    private void CoroutineStop()
    {
        StopAllCoroutines();        // ��� �ڷ�ƾ ����
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
        transform.Translate(Vector3.forward* moveDir.z * Time.deltaTime * MovePower); // ���� ����.
    }

    private void jump() 
    {
        if (state != STATE.Jump) //&& state != STATE.Move) // ���� , �����̴� �߿��� ���� �Ұ�
        {
            rgd.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            state = STATE.Jump;
        }
    }

    private void Roatate()
    {
        transform.Rotate(Vector3.up, moveDir.x * Time.deltaTime * RotatePower , Space.World);
    }


    //input system �̿��Ͽ� �����̱�
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
        {   // ���� ��ƾ��� ���� �����ϰ� ��
            //Debug.Log(name+"�� ������Ҵ�.");
            state = STATE.None;
        }
    }



}
