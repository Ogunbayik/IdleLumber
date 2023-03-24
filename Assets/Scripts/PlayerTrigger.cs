using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerTrigger : MonoBehaviour
{
    public event EventHandler OnCollectWood;
    public event EventHandler OnSellWood;

    public BuyPlace buyPlace;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Test>())
        {
            OnCollectWood?.Invoke(this, EventArgs.Empty);
        }

        var buyPlace = other.gameObject.GetComponent<BuyPlace>();

        if(buyPlace)
        {
            this.buyPlace = buyPlace;
            OnSellWood?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(buyPlace)
        {
            buyPlace = null;
        }
    }

}
