using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float speed;

    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        Vector3 moveVelocity = moveInput.normalized * speed;
        controller.Move(moveVelocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump();
        }
    }
}
