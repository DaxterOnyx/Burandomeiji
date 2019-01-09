using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    Rigidbody rb;

    /*Spawner prefab*/
    public GameObject spawner;

    /*Controller
	 * */
    public PlayerController control;

    /*Movement
	 * */
    public float speed_angle_up;
    public float speed_angle_turn;
    public float speed;
    public float cameraRotationLimit = 90f;

    [SerializeField]
    private Vector3 localRotation;

    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        control = GetComponent<PlayerController>();}

    // Utilisation de FixedUpdate pour toutes les updates liées à la physique
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void Update()
    {
        ///Click spawn
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "Terrain")
            {
                Debug.DrawRay(hit.point, hit.normal * 10, Color.green);
                if (control.ClickDown)
                {
                    Debug.Log("Click");
                    Instantiate(spawner, hit.point, Quaternion.identity);
                }
            }
        }
    }

    private void PerformMovement()
    {
        rb.velocity = Vector3.zero;
        Vector3 velocity = new Vector3(control.Horizontal, 0f, control.Vertical);
        rb.AddRelativeForce(velocity * speed, ForceMode.VelocityChange);
    }

    private void PerformRotation()
    {
        localRotation = new Vector3(Mathf.Clamp(localRotation.x + speed_angle_up * control.MouseY, -cameraRotationLimit, cameraRotationLimit), localRotation.y + speed_angle_turn * control.MouseX, 0f);
        this.transform.localRotation = Quaternion.Euler(localRotation);
    }
}