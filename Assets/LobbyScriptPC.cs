using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyScriptPC : MonoBehaviour {

    public string scenePlayName;
    public string sceneCreditsName;

    private bool playPC = false;
    private bool playVR = false;

    [SerializeField] private TextMeshProUGUI textPC;
    [SerializeField] private TextMeshProUGUI textVR;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void Update()
    {
        if(playPC && playVR)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            SceneManager.LoadScene(scenePlayName);
        }
    }
    
	public void PlayPC()
    {
        if(!playPC)
        {
            textPC.color = Color.green;
            playPC = true;
        }
        else
        {
            textPC.color = Color.white;
            playPC = false;
        }
    }

    public void PlayVR()
    {
        if (!playVR)
        {
            textVR.color = Color.green;
            playVR = true;
        }
        else
        {
            textVR.color = Color.white;
            playVR = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(sceneCreditsName);*/
    }
}
