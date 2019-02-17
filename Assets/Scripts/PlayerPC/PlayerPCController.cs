using UnityEngine;

public static class PlayerPCController {

        //*** Les cliques souris ***//

    public static bool Click0_Down
    {
        get { return Input.GetMouseButtonDown(0); }
    }

    public static bool Click1_Down
    {
        get { return Input.GetMouseButtonDown(1); }
    }

    public static bool Click2_Down
    {
        get { return Input.GetMouseButtonDown(2); }
    }

    public static bool Click0
    {
        get { return Input.GetMouseButton(0); }
    }

    public static bool Click1
    {
        get { return Input.GetMouseButton(1); }
    }

    public static bool Click2
    {
        get { return Input.GetMouseButton(2); }
    }

        //*** Molette de la souris ***//

    public static float ScrollWheel
    {
        get { return Input.GetAxis("Mouse ScrollWheel"); }
    }

        //*** Les axes pour le mouvement ***//

    public static float MouseX
    {
        get { return Input.GetAxis("Mouse X"); }
    }

    public static float MouseY
    {
        get { return Input.GetAxis("Mouse Y"); }
    }

    public static float Horizontal
    {
        get { return Input.GetAxis("Horizontal"); }
    }

    public static float Vertical
    {
        get { return Input.GetAxis("Vertical"); }
    }

        //*** Inputs in game ***//

    public static bool Up
    {
        get { return Input.GetButton("Up"); }
    }

    public static bool Down
    {
        get { return Input.GetButton("Down"); }
    }

    public static bool BonusMenu
    {
        get { return Input.GetButtonDown("BonusMenu"); }
    }

    public static bool A
    {
        get { return Input.GetButtonDown("LeftA"); }
    }

    public static bool E
    {
        get { return Input.GetButtonDown("RightE"); }
    }
}
