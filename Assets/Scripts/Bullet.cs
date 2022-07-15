using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float bulletSpeed=10f;
    public Vector3 dir;
    public float frameDist;

    public GameObject bulletEffect;
    int randomDamage;

    // Update is called once per frame
    void Update()
    {
        if ( target == null)
        {
            Destroy(gameObject);
            return;
        }

        dir = target.position - transform.position;
        frameDist = bulletSpeed * Time.deltaTime;

        if (dir.magnitude <= frameDist)
        {
            HitMonster();
            return;
        }

        transform.Translate(dir.normalized * frameDist, Space.World);
        
        
    }

    //Looks for the target that weapon is locked on.
    public void Seek(Transform _target)
    {
        target = _target;
    }


    //Destroys bullet itself, creates particle effect..
    public void HitMonster()
    {
        GameObject effectInstance = (GameObject)Instantiate(bulletEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 1f);
        Destroy(gameObject);
        DamageMonster(target);

    }

    //Decreases monster health by 10 to 50 units/s, it is randomized.
    public void DamageMonster(Transform enemy)
    {
        Monster m = enemy.GetComponent<Monster>();
        if (m != null)
        {
            randomDamage = Random.Range(10, 51);
            m.TakeDamage(randomDamage);
        }
        
    }
}
