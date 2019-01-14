using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCMotor))]
public class PlayerPCController : MonoBehaviour {

    public bool ClickDown
    {
        get { return Input.GetMouseButtonDown(0); }
    }
    public bool Menu
    {
        get { return Input.GetButtonDown("Cancel"); }
    }

    //Axis View
    public float MouseX
    {
        get { return Input.GetAxisRaw("Mouse X"); }
    }

    public float MouseY
    {
        get { return Input.GetAxisRaw("Mouse Y"); }
    }

    //Axis Mouvement
    public float Horizontal
    {
        get { return Input.GetAxis("Horizontal"); }
    }

    public float Vertical
    {
        get { return Input.GetAxis("Vertical"); }
    }

    public bool SwitchEnemy
    {
        get { return Input.GetButtonDown("SwitchEnemy"); }
    }
}
