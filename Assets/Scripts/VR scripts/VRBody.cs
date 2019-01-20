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
        
        if(player != null)
        {
            Instantiate(body, player.transform, false);
        }
	}


    private void Update()
    {
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
