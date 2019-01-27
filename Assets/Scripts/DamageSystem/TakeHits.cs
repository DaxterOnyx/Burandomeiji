using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TakeHits : MonoBehaviour {

    private bool die = false;
    [SerializeField] private GameObject damagePopupPC;
    [SerializeField] private GameObject criticalPopupPC;
    [SerializeField] private GameObject damagePopupVR;
    [SerializeField] private GameObject criticalPopupVR;
    private TextMeshProUGUI damageTextPC;
    private TextMeshProUGUI damageTextVR;
    private GameObject Ins;

    [SerializeField] private float m_health;
    private float m_currentHealth;
    public float currentHealth { get { return m_currentHealth; } private set { m_currentHealth = value; } }
    public float health { get { return m_health; } set { m_health = value; } }

    private void Start()
    {
        m_currentHealth = m_health;
    }

    public void SetAttributs(float _multHealth)
    {
        m_health *= _multHealth;
        m_currentHealth = m_health;
    }

    public void takeHits(float _hitDamage, bool _critical)
    {
        if (!die)
        {
            m_currentHealth -= _hitDamage;
            //Debug.Log("[" + this.name + "]" +  " health restant : " + m_currentHealth.ToString("0.0") + "hit damage : " + _hitDamage.ToString("0.0"));

            if (m_currentHealth <= 0f)
            {
                m_currentHealth = 0f;
                Die();
                return;
            }

            DisplayDamage(_hitDamage, _critical);
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " est mort");
        //die = true;
        m_currentHealth = m_health;
    }

    private void DisplayDamage(float _hitDamage, bool _critical)
    {
        Ins = null;
        if (this.name == "BodyVR(Clone)")
        {
            if (_critical)
                Ins = Instantiate(criticalPopupPC, this.gameObject.transform);
            else
                Ins = Instantiate(damagePopupPC, this.gameObject.transform);
        }
        else
        {
            if (_critical)
                Ins = Instantiate(criticalPopupVR, this.gameObject.transform);
            else
                Ins = Instantiate(damagePopupVR, this.gameObject.transform);          
        }

        if (Ins != null)
        {
            damageTextVR = Ins.GetComponentInChildren<TextMeshProUGUI>();
            damageTextVR.text = _hitDamage.ToString("0");
            Destroy(Ins, 0.95f);
        }
        else
        {
            Debug.LogError("Le GO Ins du script TakeHits est null.");
        }

    }
    
}
