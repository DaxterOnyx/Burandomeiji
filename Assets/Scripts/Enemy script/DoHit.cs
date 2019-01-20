using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doHit : MonoBehaviour {
    public int Damages = 1;

    private void OnCollisionEnter(Collision collision)
    {
        collision.collider.GetComponent<TakeHits>().HitPoints-=Damages;
    }
}
