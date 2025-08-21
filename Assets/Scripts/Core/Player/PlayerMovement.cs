using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{

    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;


    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float turningRate = 30f;

    private Vector2 previousMovementInput;

    //a netcode lifecycle method which is called autoamtically when the object is spawned on the network (like a player joins)
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;//isowner is true only if the client owns the object 

        inputReader.MoveEvent += HandleMove;

    }

    public override void OnNetworkDespawn()//lifecycle method called when a object is despawned(like player disconnects object destroyed)
    {
        if (!IsOwner) return;
        inputReader.MoveEvent -= HandleMove;

    }
    private void Update()
    {
        if (!IsOwner) return;

        float zRotation = previousMovementInput.x * -turningRate * Time.deltaTime;
        bodyTransform.Rotate(0f, 0f, zRotation);
    }


    private void FixedUpdate()
    {
        if (!IsOwner) return;
        rb.linearVelocity = (Vector2)bodyTransform.up * previousMovementInput.y * movementSpeed;
    }

    private void HandleMove(Vector2 movementInput)
    {
        previousMovementInput = movementInput;
    }
}


