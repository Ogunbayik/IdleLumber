using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTrigger : MonoBehaviour
{
    private CollectManager collectManager;
    [SerializeField] private float startTimer;
    private float collectTimer;
    private float sellTimer;

    public event EventHandler OnCollectWood;
    public event EventHandler OnSellWood;

    public BuyPlace buyPlace;

    private void Awake()
    {
        collectManager = GetComponent<CollectManager>();
        collectTimer = startTimer;
        sellTimer = startTimer;
    }
    private void OnTriggerStay(Collider other)
    {
        if (collectManager.HasMaximumWood())
        {
            collectTimer = startTimer;
        }

        if (other.gameObject.GetComponent<Test>())
        {
            collectTimer -= Time.deltaTime;
            if (collectTimer <= 0)
            {
                OnCollectWood?.Invoke(this, EventArgs.Empty);
                collectTimer = startTimer;
            }
        }

        var buyPlace = other.gameObject.GetComponent<BuyPlace>();
        if(buyPlace)
        {
            this.buyPlace = buyPlace;
            sellTimer -= Time.deltaTime;
            if (sellTimer <= 0)
            {
                OnSellWood?.Invoke(this, EventArgs.Empty);
                sellTimer = startTimer;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Test>())
            collectTimer = startTimer;


        if(buyPlace)
        {
            buyPlace = null;
            sellTimer = startTimer;
        }
    }

}
