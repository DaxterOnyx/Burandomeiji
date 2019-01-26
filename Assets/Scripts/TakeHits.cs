using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TakeHits : MonoBehaviour {

    private bool die = false;
    [SerializeField] private GameObject damagePopupPC;
    [SerializeField] private GameObject damagePopupVR;
    private TextMeshProUGUI damageTextPC;
    private TextMeshProUGUI damageTextVR;

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

    public void takeHits(float _hitDamage)
    {
        if (!die)
        {
            m_currentHealth -= _hitDamage;
            Debug.Log("[" + this.name + "]" +  " health restant : " + m_currentHealth.ToString("0.0") + "hit damage : " + _hitDamage.ToString("0.0"));

            if (m_currentHealth <= 0f)
            {
                m_currentHealth = 0f;
                Die();
                return;
            }

            /*GameObject Ins_ = Instantiate(damageGO, this.gameObject.transform);
            damageText = Ins_.GetComponentInChildren<Text>();
            damageText.text = _hitDamage.ToString("0");
            Destroy(Ins_, 1f);*/

            GameObject InsPC_ = Instantiate(damagePopupPC, this.gameObject.transform);
            if(InsPC_ != null)
            {
                damageTextPC = GetComponentInChildren<TextMeshProUGUI>();
                damageTextPC.text = _hitDamage.ToString("0");
                Destroy(InsPC_, 2f);
            }
            
            GameObject InsVR_ = Instantiate(damagePopupVR, this.gameObject.transform);
            if(InsVR_ != null)
            {
                damageTextVR = GetComponentInChildren<TextMeshProUGUI>();
                damageTextVR.text = _hitDamage.ToString("0");
                Destroy(InsVR_, 2f);
            }
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " est mort");
        die = true;
        m_currentHealth = m_health;
    }
    
}
