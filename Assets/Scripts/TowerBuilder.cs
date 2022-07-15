using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public List<Transform> pointsCollection = new List<Transform>();
    public GameObject tower;
    Vector2 pointCoordinates;
    private int randomSpawnPoint;
    private int towerCount=0;
    private int initialPointcount;
    UIManager uIManager;

    private void Start()
    {
        uIManager = FindObjectOfType<UIManager>();
        initialPointcount = pointsCollection.Count;
       
    }

    //Takes the transform of each field as a list, picks a random index in the list and instantiate the tower(weapon) o that index's transform.
    //After it is removed from the list so that more than one weapon is not snapwed to the same location.
    //It is called onButtonClick event.
    public void SpawnTower()
    {
        
        if (towerCount < initialPointcount )
        {
            randomSpawnPoint = Random.Range(0, pointsCollection.Count);
            pointCoordinates = new Vector2(pointsCollection[randomSpawnPoint].transform.position.x, pointsCollection[randomSpawnPoint].transform.position.y);
            Instantiate(tower, pointCoordinates, Quaternion.identity);
            pointsCollection.Remove(pointsCollection[randomSpawnPoint]);
            towerCount++;
        }

        // Displays warning when all the weapons are spawned, and more cannot be spawned.
        else
        {
            uIManager.allTowersSpawned.SetActive(true);
        }

    }

    
}
