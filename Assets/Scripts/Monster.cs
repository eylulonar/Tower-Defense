using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    public int wavepointIndex = 0;

    public int maxHealth = 100;
    public int currentHealth;
    public Text currentHealthText;

    public bool gameEnded;

    MonsterHealth monsterHealth;
    UIManager uIManager;
   

    // Start is called before the first frame update
    void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        monsterHealth = FindObjectOfType<MonsterHealth>();

        //Starts with maximum health
        currentHealth = maxHealth;
        monsterHealth.SetMaxHealth(maxHealth);

        //Set its waypoint target index to 0 as starting point
        target = Waypoints.points[0];

        gameEnded = false;
    }

    // When weapons shoot, bullets have damage effect on monsters, decreasing their health value.
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // When health becomes 0, monster object is destroyed and shot count is increased.
    void Die()
    {
        Destroy(gameObject);
        uIManager.shotMonsterCount++;

    }


    // Move monster towards the waypoits.
    void Update()
    {

        Vector2 dir = new Vector2 (target.position.x - transform.position.x, target.position.y - transform.position.y);
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        if (Vector2.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        //Display health of the monster
        currentHealthText.text = "HEALTH: " + currentHealth.ToString();
        monsterHealth.SetHealth(currentHealth);


    }

    //Updates target's next waypoint index.
    void GetNextWaypoint()
    {
        //When monster reaches the end of the path => game ends
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
            PlayerPrefs.SetString("isGameEnded", "Yes");
            return;
        }


        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }


}
