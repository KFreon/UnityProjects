using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupEmitterController : MonoBehaviour
{
    public GameObject Particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Instantiate(Particles, transform.position, Particles.transform.rotation);
        }
    }
}
