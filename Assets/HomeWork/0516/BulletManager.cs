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
        //���� ��ƼŬ�ý���
        Instantiate(ExplosionEffect, transform.position, transform.rotation); // �ڵ������ǰ��صξ���
        
        //gameObject.SetActive(false);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        var audio = GetComponent<AudioSource>();

        var wait = audio.clip.length;
        audio.clip = ExplosiveSound;

        wait += audio.clip.length;
        
        audio.Play();
        
        Destroy(gameObject , wait);     
    }
}
