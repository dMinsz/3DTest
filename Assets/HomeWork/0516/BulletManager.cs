using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]


public class BulletManager : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;
    public GameObject ExplosionEffect;
    public AudioClip ExplosiveSound;

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

    private void OnTriggerEnter(Collider other)
    {
        var audio = GetComponent<AudioSource>();
        audio.PlayOneShot(ExplosiveSound);

        var obj = Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(obj, 1f);
        Destroy(gameObject);     
    }
}
