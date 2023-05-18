using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
//using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject BulletFrepab;

    public float RepeatTime = 1.0f;


    private void CoroutineStart()
    {
        StartCoroutine(FireRoutine());
    }

    private void CoroutineStop()
    {
        StopAllCoroutines();        // ��� �ڷ�ƾ ����
    }



    IEnumerator FireRoutine()
    {
        while (true)
        {
            Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
            Instantiate(BulletFrepab, shoot.position, shoot.rotation);
            yield return new WaitForSeconds(RepeatTime);     // n�ʰ� �ð�����

        }
    }

    public void Fire()
    {
        GameManager.Data.AddShootCount(1);

        Transform shoot = transform.Find("Tank").Find("TankRenderers").Find("TankTurret").Find("ShootPosition");
        GameObject obj = Instantiate(BulletFrepab, shoot.position, shoot.rotation);
    }

    //private void OnFire(InputValue input)
    //{
    //    Fire();
    //}

    public void RepeatFire(bool isPressed)
    {

        if (isPressed)
        {
            GameManager.Data.AddShootCount(1);
            CoroutineStart();
        }
        else
        {
            CoroutineStop();
        }
    }
}
