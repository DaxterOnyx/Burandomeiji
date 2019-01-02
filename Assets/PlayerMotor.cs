using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    Rigidbody rb;

    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX;
    private float currentCameraRotationX;
    private float currentRotationY;
    private float currentRotationZ;

    [SerializeField]
    private float cameraRotationLimit = 90f;

    [SerializeField]
    private Camera cam;

    [SerializeField]

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Utilisation de FixedUpdate pour toutes les updates liées à la physique
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentCameraRotationX -= cameraRotationX;
        currentRotationY += rotation.y;
        currentRotationZ += rotation.z;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        transform.localEulerAngles = new Vector3(currentCameraRotationX, currentRotationY, currentRotationZ);
    }
}