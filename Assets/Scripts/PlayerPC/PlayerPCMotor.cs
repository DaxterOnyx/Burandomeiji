using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerPCMotor : MonoBehaviour
{
    /// Attributs

    [SerializeField] private float speed_angle_up = 10f;
    [SerializeField] private float speed_angle_turn = 5f;
    [SerializeField] private float speed = 45f;
    [SerializeField] private float cameraRotationLimit = 90f;
    [SerializeField] private float speed_Up_Down = 1f;
    [SerializeField] public Transform eyes;

    private Vector3 localRotation;
    private Vector3 eyeRotation;
    private Rigidbody rb;

    [SerializeField] private float maxY = 0f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxX = 0f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxZ = 0f;
    [SerializeField] private float minZ = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.freezeRotation = true;
        rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();

        if (PlayerPCController.Up)
        {
            PerformUp();
        }
        if (PlayerPCController.Down)
        {
            PerformDown();
        }
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, minX, maxX), Mathf.Clamp(rb.position.y, minY, maxY), Mathf.Clamp(rb.position.z, minZ, maxZ));
    }


    private void PerformMovement()
    {
        rb.velocity = Vector3.zero;
        Vector3 velocity = new Vector3(PlayerPCController.Horizontal, 0f, PlayerPCController.Vertical);
        rb.AddRelativeForce(velocity * speed, ForceMode.VelocityChange);
    }

    private void PerformRotation()
    {
        eyeRotation = new Vector3(Mathf.Clamp(eyeRotation.x - speed_angle_up * PlayerPCController.MouseY, -cameraRotationLimit, cameraRotationLimit), 0f, 0f);
        localRotation = new Vector3(0f, localRotation.y + speed_angle_turn * PlayerPCController.MouseX, 0f);
        transform.localRotation = Quaternion.Euler(localRotation);
        eyes.localRotation = Quaternion.Euler(eyeRotation);
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

    #region Ancien script
    /*

    private Rigidbody rb;
    [SerializeField] private GameObject cam;
    private PlayerPCController playerPCController;

    [SerializeField] private float speed_angle_up = 10f;
    [SerializeField] private float speed_angle_turn = 5f;
    [SerializeField] private float speed = 90f;
    [SerializeField] private float cameraRotationLimit = 90f;
    [SerializeField] private float speed_Up_Down = 1f;

    [SerializeField] private float maxY = 20f;
    [SerializeField] private float maxX = 0f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxZ = 0f;
    [SerializeField] private float minZ = 0f;

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
        else if (this.transform.position.y > maxY)
        {
            this.transform.position = new Vector3(this.transform.position.x, maxY, this.transform.position.z);
        }

        if (this.transform.position.x > maxX)
        {
            this.transform.position = new Vector3(maxX, this.transform.position.y, this.transform.position.z);
        }
        else if (this.transform.position.x < minX)
        {
            this.transform.position = new Vector3(minX, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.z > maxZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, maxZ);
        }
        else if (this.transform.position.z < minZ)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, minZ);
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

    //private void PerformRotation()
    //{
    //    localRotation = new Vector3(Mathf.Clamp(localRotation.x - speed_angle_up * playerPCController.MouseY, -cameraRotationLimit, cameraRotationLimit), localRotation.y + speed_angle_turn * playerPCController.MouseX, 0f);
    //    this.transform.localRotation = Quaternion.Euler(localRotation);
    //}

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
    }*/

    #endregion
}