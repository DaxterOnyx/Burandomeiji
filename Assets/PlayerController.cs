using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float speed = 20f;

    [SerializeField]
    private float lookSensitivityX = 10f;

    [SerializeField]
    private float lookSensitivityY = 10f;

    [SerializeField]
    PlayerMotor motor;

    private Vector3 velocity;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Avancer / Reculer
        float _xMov = Input.GetAxis("Horizontal");
        // Gauche / Droite
        float _zMov = Input.GetAxis("Vertical");
        // Mouvement horizontaux et verticaux
        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVertical = transform.forward * _zMov;
        //Calcul de la vélocité
        velocity = (_moveHorizontal + _moveVertical) * speed;
        // Application de la vélocité
        motor.Move(velocity);

        // Axe X de la souris
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0, _yRot, 0) * lookSensitivityX;
        motor.Rotate(_rotation);

        // Axe Y de la souris
        float _xRot = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRot * lookSensitivityY;
        motor.RotateCamera(_cameraRotationX);
    }
}
