using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthVR : MonoBehaviour {

    [SerializeField] private RectTransform healthBarFill;

    private GameObject player;
    private TakeHits takeHits;

    private bool canUpdate = true;
    [HideInInspector] public float currentHealth;

    public IEnumerator SetHealth(float healthMax_, float currentAmount_)
    {
        canUpdate = false;
        if (currentAmount_ < 0)
        {
            currentAmount_ = 0;
        }
        healthBarFill.localScale = new Vector3(1f, currentAmount_ / healthMax_, 1f);

        currentHealth = currentAmount_;
        yield return new WaitForSeconds(0.05f);
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
        if (canUpdate && takeHits != null)
        {
            StartCoroutine(SetHealth(takeHits.health, takeHits.currentHealth));
        }
    }
}
