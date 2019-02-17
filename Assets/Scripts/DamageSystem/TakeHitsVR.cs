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

        for (int i = 0; i < 5; i++)
        {
            Ins = Instantiate(damagePopupPC, this.gameObject.transform);
            damagePopupListPC.Add(Ins);
            Ins.SetActive(false);
            Ins = Instantiate(criticalPopupPC, this.gameObject.transform);
            criticalPopupListPC.Add(Ins);
            Ins.SetActive(false);
        }
    }

    public override void takeHits(float _hitDamage, bool _critical)
    {
        base.takeHits(_hitDamage, _critical);
    }

    public override void Die()
    {
        base.Die();
        GameManager.Instance.SetBoolEnd(true);
    }

    protected override void Display(float _hitDamage, bool _critical)
    {
        if (_critical)
        {
            DisplayAux(criticalPopupPC, criticalPopupListPC, _hitDamage);
        }
        else
        {
            DisplayAux(damagePopupPC, damagePopupListPC, _hitDamage);
        }
    }
}
