using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoHits : MonoBehaviour {

    private bool canDoHits = true;

    // Les attributs de bases. Ils peuvent être initialié directement dans l'inspector
    [SerializeField] private float m_hitDamage;
    [SerializeField] private float m_hitCooldown;
    [SerializeField] private float m_critical;

    public float hitDamage { get { return m_hitDamage; } private set { m_hitDamage = value; } }
    public float hitCooldown { get { return m_hitCooldown; } private set { m_hitCooldown = value; } }
    public float critical { get { return m_critical; } private set { m_critical = value; } }

    // Fonction utilisé pour les ennemies
    // Permet de récupérer et d'affecter les multiplicateus
    public void SetAttributs(float _multHitDamage, float _multHitCooldown, float _multCritical)
    {
        m_hitDamage *= _multHitDamage;
        m_hitCooldown *= _multHitCooldown;
        m_critical *= _multCritical;
    }

    public void doHits(TakeHits _takeHits)
    {
        float crit_ = Random.Range(0f, 100f);
        float multCrit_ = Random.Range(2f, 4f);
        //Debug.Log(crit_ + " / " + multCrit_);

        if(crit_ < m_critical) // Si coup critique réussi
        {
            //Debug.Log("Coup critique réussi!");
            _takeHits.takeHits(m_hitDamage * multCrit_);
        }
        else
        {
            _takeHits.takeHits(m_hitDamage);
        }
    }

    private void OnTriggerStay(Collider col)
    {

        if(this.gameObject.tag == "Enemy" && col.gameObject.tag == "Enemy")
        {
            return;
        }

        TakeHits takeHits_ = col.GetComponent<TakeHits>();
        if(takeHits_ != null)
        {
            if(canDoHits)
            {
                StartCoroutine(DoHitsCoroutine(takeHits_));
            }
        }
    }

    private IEnumerator DoHitsCoroutine(TakeHits takeHits_)
    {
        canDoHits = false;
        doHits(takeHits_);
        yield return new WaitForSeconds(1f/m_hitCooldown);
        canDoHits = true;
    }
}