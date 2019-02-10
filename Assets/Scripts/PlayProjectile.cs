using TMPro;
using UnityEngine;

public class PlayProjectile : MonoBehaviour {

    [SerializeField] private GameObject lobby;
    private LobbyScriptPC lobbyScriptPC;

    private void Start()
    {
        lobbyScriptPC = lobby.GetComponent<LobbyScriptPC>();
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log("Collision!");
        lobbyScriptPC.playVR = !lobbyScriptPC.playVR;
    }

}
