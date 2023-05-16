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
        gameObject.SetActive(false);

        var audio = GetComponent<AudioSource>();
        audio.clip = ExplosiveSound;
        audio.Play();


        var obj = Instantiate(ExplosionEffect, transform.position, transform.rotation);
        Destroy(obj, 1f);
        Destroy(gameObject,1f);     
    }
}
