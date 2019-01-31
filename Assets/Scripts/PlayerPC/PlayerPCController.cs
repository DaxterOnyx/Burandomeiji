using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerPCMotor))]
public class PlayerPCController : MonoBehaviour {

    public bool ClickDown
    {
        get { return Input.GetMouseButtonDown(0); }
    }

    /*public bool Click0
    {
        get { return Input.GetMouseButton(0); }
    }

    public bool Click1
    {
        get { return Input.GetMouseButton(1); }
    }*/

    public bool Menu
    {
        get { return Input.GetButtonDown("Cancel"); }
    }

    //Axis View
    public float MouseX
    {
        get { return Input.GetAxis("Mouse X"); }
    }

    public float MouseY
    {
        get { return Input.GetAxis("Mouse Y"); }
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

    public bool Up
    {
        get { return Input.GetButton("Up"); }
    }

    public bool Down
    {
        get { return Input.GetButton("Down"); }
    }

    public float ScrollWheel
    {
        get { return Input.GetAxis("Mouse ScrollWheel"); }
    }

    public bool BonusMenu
    {
        get { return Input.GetButtonDown("BonusMenu"); }
    }

    public bool A
    {
        get { return Input.GetButtonDown("LeftA"); }
    }

    public bool E
    {
        get { return Input.GetButtonDown("RightE"); }
    }
}
