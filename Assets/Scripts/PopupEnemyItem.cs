using UnityEngine;
using UnityEngine.UI;

public class PopupEnemyItem : MonoBehaviour {

    [SerializeField] private Text popupText;

    public void Setup(string name)
    {
        popupText.text = name;
    }
}
