using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectManager : MonoBehaviour
{
    private PlayerTrigger playerTrigger;
    private PlayerAnimation animator;

    [SerializeField] private List<GameObject> woodList;

    [SerializeField] private Transform woodPrefab;
    [SerializeField] private Transform carryPoint;

    [SerializeField] private int maximumCollectAmount;

    private bool hasMaximumWood;
    private void Awake()
    {
        playerTrigger = GetComponent<PlayerTrigger>();
        animator = GetComponentInChildren<PlayerAnimation>();
    }
    void Start()
    {
        woodList = new List<GameObject>();
        playerTrigger.OnCollectWood += PlayerTrigger_OnCollectWood;
        playerTrigger.OnSellWood += PlayerTrigger_OnSellWood;
    }
    private void Update()
    {
        CheckMaximumWood();
    }
    private void CheckMaximumWood()
    {
        if (woodList.Count <= 0)
            animator.CarryAnimation(false);

        if (woodList.Count < maximumCollectAmount)
            hasMaximumWood = false;
        else
            hasMaximumWood = true;
    }

    private void PlayerTrigger_OnCollectWood(object sender, System.EventArgs e)
    {
        if (woodList.Count < maximumCollectAmount)
        {
            var wood = Instantiate(woodPrefab, carryPoint);
            woodList.Add(wood.gameObject);

            var woodIndex = woodList.IndexOf(wood.gameObject);
            var positionYRate = 3.3f;
            var distanceBetweenWood = (float)woodIndex / positionYRate;
            wood.transform.position = new Vector3(wood.transform.position.x, wood.transform.position.y + distanceBetweenWood, wood.transform.position.z);
        }
    }
    private void PlayerTrigger_OnSellWood(object sender, System.EventArgs e)
    {
        if (woodList.Count > 0)
        {
            var lastWood = woodList[woodList.Count - 1];
            woodList.Remove(lastWood);
            Destroy(lastWood.gameObject);
            playerTrigger.buyPlace.AddWood();
        }
    }

    public bool HasMaximumWood()
    {
        return hasMaximumWood;
    }

}
