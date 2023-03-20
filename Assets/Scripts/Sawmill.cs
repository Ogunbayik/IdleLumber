using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : MonoBehaviour
{
    private List<GameObject> lumberList;

    [SerializeField] private Transform lumberPrefab;
    [SerializeField] private Transform producePoint;
    [SerializeField] private Vector3 lumberRotation;

    [SerializeField] private float produceRate;
    [SerializeField] private int produceAmountMax;
    private float produceTimer;

    void Start()
    {
        lumberList = new List<GameObject>();
        produceTimer = produceRate;
    }

    void Update()
    {
        ProduceLumber();
    }

    private void ProduceLumber()
    {
        produceTimer -= Time.deltaTime;
  
        if (produceTimer <= 0 && lumberList.Count < produceAmountMax)
        {
            CreateLumber();
            produceTimer = produceRate;
        }
    }

    private void CreateLumber()
    {
        var lumber = Instantiate(lumberPrefab);
        lumberList.Add(lumber.gameObject);
        var lumberIndex = lumberList.IndexOf(lumber.gameObject);
        var distanceBetweenLumber = 3.3f;
        var nextPosition = (float)lumberIndex / distanceBetweenLumber;
        lumber.position = new Vector3(producePoint.position.x + nextPosition, producePoint.position.y, producePoint.position.z);
        lumber.rotation = Quaternion.Euler(lumberRotation);
    }


}
