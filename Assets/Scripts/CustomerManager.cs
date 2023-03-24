using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private Truck[] allTrucks;
    private Truck oldTruck;
    private Truck newTruck;

    [SerializeField] private Transform spawnPoint;
    
    void Start()
    {
        SpawnFirst();
    }

    void Update()
    {
        SpawnRandomTruck();
    }
    private void SpawnFirst()
    {
        var randomIndex = Random.Range(0, allTrucks.Length);
        var randomTruck = allTrucks[randomIndex];
        oldTruck = Instantiate(randomTruck, spawnPoint.position, Quaternion.identity);
        oldTruck.SetWoodCount(3);
    }

    private void SpawnRandomTruck()
    {
        if(oldTruck.GetWoodCount() <= 0)
        {
            var randomIndex = Random.Range(0, allTrucks.Length);
            var randomTruck = allTrucks[randomIndex];
            newTruck = Instantiate(randomTruck, spawnPoint.position, Quaternion.identity);
            newTruck.SetWoodCount(4);
            oldTruck = newTruck;
        }
    }

}
