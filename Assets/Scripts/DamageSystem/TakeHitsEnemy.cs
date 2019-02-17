using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoHits))]
[RequireComponent(typeof(AIScript))]
public class TakeHitsEnemy : TakeHits {

    [SerializeField] private GameObject damagePopupVR;
    private List<GameObject> damagePopupListVR = new List<GameObject>();

    protected override void Awake()
    {
        aiScript = GetComponent<AIScript>();
        doHits = GetComponent<DoHits>();
    }

    protected override void Start()
    {
        currentHealth = health;

        for (int i = 0; i < 6; i++)
        {
            Ins = Instantiate(damagePopupVR, this.gameObject.transform);
            damagePopupListVR.Add(Ins);
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
        aiScript.Die();
    }
   
    protected override void Display(float _hitDamage, bool _critical)
    {
        DisplayAux(damagePopupVR, damagePopupListVR, _hitDamage);
    }

    public override void Freeze()
    {
        aiScript.isFreeze = true;
    }

    public override void UnFreeze()
    {
        aiScript.isFreeze = false;
    }

    public override void Slow(float _power)
    {
        doHits.Slow(_power); // Diminue la vitesse d'attaque
        aiScript.Slow(_power);  // Diminue la vitesse de marche
        isSlow = true;
    }

    public override void UnSlow()
    {
        doHits.UnSlow();
        aiScript.UnSlow();
        isSlow = false;
    }
}
