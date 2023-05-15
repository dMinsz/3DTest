using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    private Rigidbody rgb;


    private void Awake()
    {
        rgb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rgb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject,5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
