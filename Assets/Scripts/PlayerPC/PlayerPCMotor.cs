using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerPCController))]
public class PlayerPCMotor : MonoBehaviour {

    private Rigidbody rb;
    [SerializeField] private GameObject cam;
    /* Controller */
    private PlayerPCController playerPCController;

    /* Movement */
    [SerializeField] private float speed_angle_up = 10f;
    [SerializeField] private float speed_angle_turn = 5f;
    [SerializeField] private float speed = 90f;
    [SerializeField] private float cameraRotationLimit = 90f;
    [SerializeField] private float speed_Up_Down = 1f;

	public Transform Eye;
    private Vector3 localRotation;
	private Vector3 eyeRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerPCController = GetComponent<PlayerPCController>();
    }

    // Utilisation de FixedUpdate pour toutes les updates liées à la physique
    void FixedUpdate()
    {
        if(this.transform.position.y < 2f)
        {
            this.transform.position = new Vector3(this.transform.position.x, 2f, this.transform.position.z);
        }
        PerformMovement();
        PerformRotation();
        if(playerPCController.Up)
        {
            PerformUp();
        }
        if(playerPCController.Down)
        {
            PerformDown();
        }
    }

    /*private void PerformRotation()
    {
        localRotation = new Vector3(Mathf.Clamp(localRotation.x - speed_angle_up * playerPCController.MouseY, -cameraRotationLimit, cameraRotationLimit), localRotation.y + speed_angle_turn * playerPCController.MouseX, 0f);
        this.transform.localRotation = Quaternion.Euler(localRotation);
    }*/

    private void PerformMovement()
    {
        rb.velocity = Vector3.zero;
        Vector3 velocity = new Vector3(playerPCController.Horizontal, 0f, playerPCController.Vertical);
        rb.AddRelativeForce(velocity * speed, ForceMode.VelocityChange);
    }

    private void PerformRotation()
    {
        eyeRotation = new Vector3(Mathf.Clamp(eyeRotation.x - speed_angle_up * playerPCController.MouseY, -cameraRotationLimit, cameraRotationLimit), 0f, 0f);
		localRotation = new Vector3(0f, localRotation.y + speed_angle_turn * playerPCController.MouseX, 0f);
		transform.localRotation = Quaternion.Euler(localRotation);
		Eye.localRotation = Quaternion.Euler(eyeRotation);
    }

    private void PerformUp()
    {
        rb.velocity = Vector3.zero;
        Vector3 velocity = new Vector3(0f, speed_Up_Down, 0f);
        rb.AddForce(velocity * speed, ForceMode.VelocityChange);
    }

    private void PerformDown()
    {
        Ray ray = new Ray(this.transform.position, this.transform.TransformDirection(Vector3.down));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 2f))
        {
            return;
        }
        rb.velocity = Vector3.zero;
        Vector3 velocity = new Vector3(0f, -speed_Up_Down, 0f);
        rb.AddForce(velocity * speed, ForceMode.VelocityChange);
    }
}