using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEffect : MonoBehaviour
{
    public GameObject particleEffect;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particleEffect, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
