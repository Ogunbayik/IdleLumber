using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMill : MonoBehaviour
{
    private SawMillAnimation animator;
    [SerializeField] private List<GameObject> timberList;

    [SerializeField] private Transform timberPrefab;
    [SerializeField] private Transform buildPosition;

    [SerializeField] private int woodCount;
    [SerializeField] private int maximumTimber;
    private int stackCount = 10;

    private bool canBuild;

    private void Awake()
    {
        animator = GetComponent<SawMillAnimation>();
        timberList = new List<GameObject>();
    }

    private void Update()
    {
        Build();
    }

    private void Build()
    {
        if (woodCount > 0)
            canBuild = true;
        else
            canBuild = false;

        if (canBuild)
            animator.WorkAnimation(true);
        else
            animator.WorkAnimation(false);
    }

    public void BuildTimber()
    {
        var timberCount = timberList.Count;
        var rowCount = timberCount / stackCount;

        if (timberCount < maximumTimber)
        {
            var rotation = new Vector3(0, 90f, 0);
            var timber = Instantiate(timberPrefab);
            timberList.Add(timber.gameObject);
            

            var distanceBetweenTimber = 6f;
            timber.transform.position = new Vector3(buildPosition.position.x + ((float)rowCount/1.5f), buildPosition.position.y + (timberCount%stackCount)/ distanceBetweenTimber, buildPosition.position.z);
            timber.transform.rotation = Quaternion.Euler(rotation);
        }

        woodCount--;
    }

    public void RemoveLumber()
    {
        GameObject lastWood = timberList[timberList.Count - 1];

        if (timberList.Count > 0)
        {

            var lastWoodIndex = timberList.Count - 1;
            timberList.RemoveAt(lastWoodIndex);
            Destroy(lastWood);
        }
    }

    public void AddWood()
    {
        woodCount++;
    }


}
