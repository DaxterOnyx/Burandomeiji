using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarScript : MonoBehaviour {

    [SerializeField] private RectTransform healthBarFill;
    [SerializeField] private TextMeshProUGUI textHealth;

    private GameObject player;
    private TakeHits takeHits;

    private bool canUpdate = true;
    public float currentHealth;

    public IEnumerator SetHealth(float healthMax_, float currentAmount_)
    {
        canUpdate = false;
        if (currentAmount_ < 0)
        {
            currentAmount_ = 0;
        }
        healthBarFill.localScale = new Vector3(currentAmount_ / healthMax_, 1f, 1f);
        textHealth.text = ((currentAmount_/healthMax_)*100).ToString("0") + "%";
        currentHealth = currentAmount_;
        yield return new WaitForSeconds(0.1f);
        canUpdate = true;
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");      
        }
        else if (takeHits == null)
        {
            takeHits = player.GetComponentInChildren<TakeHits>();
            StartCoroutine(SetHealth(takeHits.health, takeHits.currentHealth));
        }
        if (canUpdate)
        {
            StartCoroutine(SetHealth(takeHits.health, takeHits.currentHealth));
        }
    }
}

