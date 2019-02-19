using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LobbyScriptPC : MonoBehaviour {

    public string scenePlayName;
    public string sceneCreditsName;

    [SerializeField] private bool playPC = false;
    public bool playVR = false;

    [SerializeField] private TextMeshProUGUI textPC_PC;
    [SerializeField] private TextMeshProUGUI textVR_PC;
    [SerializeField] private TextMeshProUGUI textPC_VR;
    [SerializeField] private TextMeshProUGUI textVR_VR;

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

        if(playPC)
        {
            textPC_PC.color = Color.green;
            textVR_PC.color = Color.green;
        }
        else
        {
            textPC_PC.color = Color.white;
            textVR_PC.color = Color.white;
        }

        if (playVR)
        {
            textVR_VR.color = Color.green;
            textPC_VR.color = Color.green;
        }
        else
        {
            textVR_VR.color = Color.white;
            textPC_VR.color = Color.white;
        }
    }
    
	public void PlayPC()
    {
        playPC = !playPC;
    }

    public void Quit()
    {
        Application.Quit();
    }

    //TODO TO_CHANGE Create scene credit, and his menu implemented too
    public void Credits()
    {
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(sceneCreditsName);*/
    }
}
