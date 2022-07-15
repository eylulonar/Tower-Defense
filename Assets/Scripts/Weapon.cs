using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private Transform target;
    public Transform rotateWeapon;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    [Header("Attributes")]
    public float range;
    public float turnRate = 4f;
    public float fireRate=1f;
    private float fireCountdown = 0f;
   
  

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTargetMonster", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            return;
        }

        //As the target lock is updated, the direction of the weapon changes towards the target, only during the game.
        if (PlayerPrefs.HasKey("isGameEnded"))
        {
            string result = PlayerPrefs.GetString("isGameEnded");
            if (result == "No")
            {
                
                Vector3 dir = target.position - transform.position;
                Quaternion turnRotation = Quaternion.LookRotation(dir);
                Vector3 weaponRotation = Quaternion.Slerp(rotateWeapon.rotation, turnRotation, Time.deltaTime * turnRate).eulerAngles;
                rotateWeapon.rotation = Quaternion.Euler(0f, 0f, weaponRotation.z);
            }
        }

        //The weapon shot rate
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;

    }

    void Shoot()
    {
        if (PlayerPrefs.HasKey("isGameEnded"))
        {

            string result = PlayerPrefs.GetString("isGameEnded");
            if(result == "No")
            {
                GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
                Bullet bullet = bulletGameObject.GetComponent<Bullet>();

                if (bullet != null)
                {
                    bullet.Seek(target);
                }

                StartCoroutine(WaitBetweenShots());
            }
        }
       
    }

    IEnumerator WaitBetweenShots()
    {
        yield return new WaitForSeconds(1f);
    }

    // Sets the monster as target within the range of the weapon.
    void UpdateTargetMonster()
    {
       
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float shortestPath = Mathf.Infinity;
        GameObject closestMonster = null;

        foreach(GameObject monster in monsters)
        {
            float distanceToMonster = Vector2.Distance(transform.position, monster.transform.position);
            if(distanceToMonster < shortestPath)
            {
                shortestPath = distanceToMonster;
                closestMonster = monster;
            }
        }

        if(closestMonster != null && shortestPath <= range)
        {
            
            target = closestMonster.transform;
        }
        else
        {
            target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 gizmos = new Vector2(transform.position.x, transform.position.y);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(gizmos, range);
      
    }
}
