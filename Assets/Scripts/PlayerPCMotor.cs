using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerPCController))]
public class PlayerPCMotor : MonoBehaviour {

    private Rigidbody rb;

    /* Controller */
    private PlayerPCController playerPCController;

    /* Movement */
    [SerializeField] private float speed_angle_up = 10f;
    [SerializeField] private float speed_angle_turn = 5f;
    [SerializeField] private float speed = 90f;
    [SerializeField] private float cameraRotationLimit = 90f;

    private Vector3 localRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPCController = GetComponent<PlayerPCController>();
    }

    // Utilisation de FixedUpdate pour toutes les updates liées à la physique
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        rb.velocity = Vector3.zero;
        Vector3 velocity = new Vector3(playerPCController.Horizontal, 0f, playerPCController.Vertical);
        rb.AddRelativeForce(velocity * speed, ForceMode.VelocityChange);
    }

    private void PerformRotation()
    {
        localRotation = new Vector3(Mathf.Clamp(localRotation.x - speed_angle_up * playerPCController.MouseY, -cameraRotationLimit, cameraRotationLimit), localRotation.y + speed_angle_turn * playerPCController.MouseX, 0f);
        this.transform.localRotation = Quaternion.Euler(localRotation);
    }
}