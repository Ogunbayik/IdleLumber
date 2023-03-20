using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPlace : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if(player)
        {
            Debug.Log("You can buy");
        }
    }


}
