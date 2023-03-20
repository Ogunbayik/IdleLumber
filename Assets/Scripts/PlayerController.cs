using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string HORIZONTAL_INPUT = "Horizontal";
    private const string VERTICAL_INPUT = "Vertical";

    private PlayerAnimation animator;

    [SerializeField] private Transform body;
    [SerializeField] private float runSpeed;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;

    private bool isRun;
    private void Awake()
    {
        animator = GetComponentInChildren<PlayerAnimation>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(VERTICAL_INPUT);
        movementDirection = new Vector3(horizontalInput, 0f, verticalInput);

        if (movementDirection.magnitude != 0)
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }

        if(isRun)
        {
            animator.RunAnimation(true);
            body.transform.forward = Vector3.Slerp(body.transform.forward, movementDirection, 5f * Time.deltaTime);
            transform.Translate(movementDirection * runSpeed * Time.deltaTime);
        }
        else
        {
            animator.RunAnimation(false);
        }

    }
}
