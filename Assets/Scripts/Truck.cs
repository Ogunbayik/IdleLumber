using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    private enum States
    {
        SellWood,
        BuyLumber,
        Move,
    }

    [SerializeField] private States currentState;

    private NavMeshAgent agent;
    [SerializeField] private TruckMovePoints[] movePoints;

    private int woodCount;
    private int moveIndex = 0;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        currentState = States.Move;
        SetWoodCount(woodCount);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            woodCount--;
        }

        switch(currentState)
        {
            case States.Move: HandleMovement();
                break;
            case States.SellWood: HandleSell();
                break;
            case States.BuyLumber: HandleBuy();
                break;
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        var allPoints = FindObjectsOfType<TruckMovePoints>();
        for (int i = 0; i < allPoints.Length; i++)
        {
            movePoints = allPoints;
        }

        if (currentState == States.Move)
            agent.SetDestination(movePoints[moveIndex].transform.position);

        var distanceBetweenPoint = Vector3.Distance(transform.position, movePoints[moveIndex].transform.position);
        var sellIndex = 0;
        var buyIndex = 2;

        if (distanceBetweenPoint == 0.15f && moveIndex == sellIndex)
        {
            currentState = States.SellWood;
        }
        else if(distanceBetweenPoint == 0.15f && moveIndex == buyIndex)
        {
            currentState = States.BuyLumber;
        }
        else if(distanceBetweenPoint == 0.15f)
        {
            if (moveIndex >= movePoints.Length)
            {
                moveIndex = movePoints.Length;
                return;
            }

            moveIndex++;
        }
        Debug.Log(moveIndex);
    }

    private void HandleSell()
    {
        Debug.Log("You can sell wood!");

        if(Input.GetKeyDown(KeyCode.T))
        {
            moveIndex++;
            currentState = States.Move;
        }
    }

    private void HandleBuy()
    {
        Debug.Log("You can buy lumber!");
        if (Input.GetKeyDown(KeyCode.T))
        {
            moveIndex++;
            currentState = States.Move;
        }
    }

    



    public void SetWoodCount(int woodCount)
    {
        this.woodCount = woodCount;
    }
    public int GetWoodCount()
    {
        return woodCount;
    }
}
