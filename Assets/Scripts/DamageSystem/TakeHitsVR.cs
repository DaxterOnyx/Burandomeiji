using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHitsVR : TakeHits {

    [SerializeField] private GameObject damagePopupPC;
    [SerializeField] private GameObject criticalPopupPC;
    private List<GameObject> damagePopupListPC = new List<GameObject>();
    private List<GameObject> criticalPopupListPC = new List<GameObject>();

    protected override void Start()
    {
        currentHealth = health;
        for (int i = 0; i < maxPopup; i++)
        {
            Ins = Instantiate(damagePopupPC, gameObject.transform);
            damagePopupListPC.Add(Ins);
            Ins.SetActive(false);
            Ins = Instantiate(criticalPopupPC, gameObject.transform);
            criticalPopupListPC.Add(Ins);
            Ins.SetActive(false);
        }
    }

    public override void takeHits(float _hitDamage, bool _critical)
    {
        if (!die)
        {
            currentHealth -= _hitDamage;

            if (currentHealth <= 0f)
            {
                currentHealth = 0f;
                Die();
            }

            if (_critical)
            {
                Display(criticalPopupPC, criticalPopupListPC, _hitDamage);
            }
            else
            {
                Display(damagePopupPC, damagePopupListPC, _hitDamage);
            }
        }
    }

    public override void Die()
    {
        die = true;
        GameManager.Instance.SetBoolEnd(true);
    }
}
