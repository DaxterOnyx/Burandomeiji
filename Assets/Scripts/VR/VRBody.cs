using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRBody : MonoBehaviour {

    [SerializeField]
    GameObject body;

    GameObject player;

	// Use this for initialization
	void Start ()
    {
      
	}


    private void Update()
    {
        //TODO TO_CHANGE Deplace to gamemanager
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Instantiate(body, player.transform, false);
            }
        }
    }
}
