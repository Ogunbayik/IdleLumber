using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyPlace : MonoBehaviour
{
    [SerializeField] private SawMill sawMill;

    public void AddWood()
    {
        sawMill.AddWood();
    }


}
