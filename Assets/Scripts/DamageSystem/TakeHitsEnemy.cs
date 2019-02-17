using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DoHits))]
[RequireComponent(typeof(AIScript))]
public class TakeHitsEnemy : TakeHits {

    [SerializeField] private GameObject damagePopupVR;
    private List<GameObject> damagePopupListVR = new List<GameObject>();

    private void Awake()
    {
        aiScript = GetComponent<AIScript>();
        doHits = GetComponent<DoHits>();
    }

    protected override void Start()
    {
        currentHealth = health;
        for (int i = 0; i < maxPopup; i++)
        {
            Ins = Instantiate(damagePopupVR, this.gameObject.transform);
            damagePopupListVR.Add(Ins);
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

            Display(damagePopupVR, damagePopupListVR, _hitDamage);
        }
    }

    public override void Die()
    {
        die = true;
        aiScript.Die();
    }

    public void Freeze()
    {
        aiScript.isFreeze = true;
    }

    public void UnFreeze()
    {
        aiScript.isFreeze = false;
    }

    public void Slow(float _power)
    {
        doHits.Slow(_power); // Diminue la vitesse d'attaque
        aiScript.Slow(_power);  // Diminue la vitesse de marche
        isSlow = true;
    }

    public void UnSlow()
    {
        doHits.UnSlow();
        aiScript.UnSlow();
        isSlow = false;
    }
}
