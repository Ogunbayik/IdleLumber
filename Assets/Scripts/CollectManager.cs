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

    [SerializeField] private float collectTimer;
    [SerializeField] private float startCollectTimer;

    

    private void Awake()
    {
        playerTrigger = GetComponent<PlayerTrigger>();
        animator = GetComponentInChildren<PlayerAnimation>();
    }
    void Start()
    {
        woodList = new List<GameObject>();
        collectTimer = startCollectTimer;
        playerTrigger.OnCollectWood += PlayerTrigger_OnCollectWood;
        playerTrigger.OnSellWood += PlayerTrigger_OnSellWood;
    }

    private void Update()
    {
        collectTimer -= Time.deltaTime;
    }

    private void PlayerTrigger_OnCollectWood(object sender, System.EventArgs e)
    {
        if (collectTimer <= 0)
        {
            var wood = Instantiate(woodPrefab, carryPoint);
            woodList.Add(wood.gameObject);

            var woodIndex = woodList.IndexOf(wood.gameObject);
            wood.transform.position = new Vector3(wood.transform.position.x, wood.transform.position.y + (float)woodIndex/3.3f , wood.transform.position.z);

            collectTimer = startCollectTimer;
        }
    }
    private void PlayerTrigger_OnSellWood(object sender, System.EventArgs e)
    {
        if (woodList.Count > 0)
        {
            if (collectTimer <= 0)
            {
                var lastWood = woodList[woodList.Count - 1];
                woodList.Remove(lastWood);
                Destroy(lastWood.gameObject);

                collectTimer = startCollectTimer;
            }

        }
        else
        {
            animator.CarryAnimation(false);
        }

    }
}