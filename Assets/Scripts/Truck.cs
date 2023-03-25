using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Truck : MonoBehaviour
{
    [SerializeField] private GameObject strap;
    [SerializeField] private GameObject strapR;
    [SerializeField] private GameObject cargo;

    private PlayerTrigger playerTrigger;
    private NavMeshAgent agent;
    private enum States
    {
        SellWood,
        BuyLumber,
        Move,
    }
    [SerializeField] private TruckMovePoints[] movePoints;
    [SerializeField] private States currentState;


    private float buyTimer;
    private float startBuyTimer = 1f;
    private int woodCount;
    private int desiredLumberCount;
    private int moveIndex = 0;
    private void Awake()
    {
        playerTrigger = FindObjectOfType<PlayerTrigger>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        SetWoodCount();
        currentState = States.Move;
        buyTimer = startBuyTimer;

        playerTrigger.OnCollectWood += PlayerTrigger_OnCollectWood;
    }

    private void PlayerTrigger_OnCollectWood(object sender, System.EventArgs e)
    {
        if (currentState == States.SellWood)
            woodCount--;
    }

    void Update()
    {
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
    private void SetWoodCount()
    {
        var minimumWood = 3;
        var maximumWood = 10;

        woodCount = Random.Range(minimumWood, maximumWood);
        desiredLumberCount = woodCount;
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
        if (woodCount <= 0)
        {
            ActivateCargo(false);
            moveIndex++;
            currentState = States.Move;
        }

    }

    private void HandleBuy()
    {
        if(desiredLumberCount <= 0)
        {
            moveIndex++;
            currentState = States.Move;
        }

        buyTimer -= Time.deltaTime;

        if(buyTimer <= 0)
        {
            ActivateCargo(true);
            buyTimer = startBuyTimer;
            desiredLumberCount--;
        }
    }

    private void ActivateCargo(bool isActive)
    {
        strap.SetActive(isActive);
        strapR.SetActive(isActive);
        cargo.SetActive(isActive);
    }

    public int GetWoodCount()
    {
        return woodCount;
    }
}
