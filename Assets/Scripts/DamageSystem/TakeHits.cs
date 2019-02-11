using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TakeHits : MonoBehaviour {

    private bool die = false;
    private GameObject Ins;
    private bool isEnemy;
    private bool isSlow;

    [SerializeField] private GameObject damagePopupVR;
    [SerializeField] private GameObject damagePopupPC;
    [SerializeField] private GameObject criticalPopupVR;
    [SerializeField] private GameObject criticalPopupPC;

    private List<GameObject> damagePopupListVR = new List<GameObject>();
    private List<GameObject> damagePopupListPC = new List<GameObject>();
    //private List<GameObject> criticalPopupListVR = new List<GameObject>();
    private List<GameObject> criticalPopupListPC = new List<GameObject>();

    AIScript aiScript; // Contient "Speed"
    DoHits doHits; // Contient "hitCooldown"

    [SerializeField] private float m_health;
    private float m_currentHealth;
    public float currentHealth { get { return m_currentHealth; } private set { m_currentHealth = value; } }
    public float health { get { return m_health; } set { m_health = value; } }

    private void Start()
    {
        m_currentHealth = m_health;

        if(this.tag == "Enemy") // C'est un ennemi
        {
            isEnemy = true;

            for (int i = 0; i < 6; i++)
            {
                Ins = Instantiate(damagePopupVR, this.gameObject.transform);
                damagePopupListVR.Add(Ins);
                Ins.SetActive(false);
                //Ins = Instantiate(criticalPopupVR, this.gameObject.transform);
                //criticalPopupListVR.Add(Ins);
                //Ins.SetActive(false);
            }
        }
        else // C'est le joueur VR
        {
            isEnemy = false;

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

        if(isEnemy)
        {
            aiScript = this.GetComponent<AIScript>();
            doHits = this.GetComponent<DoHits>();
        }
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
            
            if (m_currentHealth <= 0f)
            {
                m_currentHealth = 0f;
                Die();
            }
            else
            {
                Display(_hitDamage, _critical);  // je display les dégâts
            }
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + " est mort");
        die = true;

        if(isEnemy)
        {
            if(GetComponent<EnemyStats>().type == EnemyStats.enemyType.Melee)
            {
                GameManager.Instance.enemyCountInGame_cac--;
            }
            else if(GetComponent<EnemyStats>().type == EnemyStats.enemyType.Distance)
            {
                GameManager.Instance.enemyCountInGame_dis--;
            }
            else
            {
                GameManager.Instance.enemyCountInGame_boss--;
            }

            Destroy(this.gameObject);
        }
        else
        {
            // Le joueur VR est mort
            GameManager.Instance.SetBoolEnd(true);
        }
    }

    private void Display(float _hitDamage, bool _critical)
    {
        if (isEnemy)
        {
            DisplayAux(damagePopupVR, damagePopupListVR, _hitDamage);

        }
        else
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

    private void DisplayAux(GameObject _Ins , List<GameObject> _list, float _hitDamage)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            if (_list[i].activeSelf == false)
            {
                _list[i].SetActive(true);
                _list[i].GetComponentInChildren<TextMeshProUGUI>().text = _hitDamage.ToString("0");
                StartCoroutine(Wait(_list[i]));
                return;
            }
        }
    }

    private IEnumerator Wait(GameObject _Popup)
    {
        yield return new WaitForSeconds(0.95f);
        _Popup.SetActive(false);
    }

    public void Freeze() // L'ennemi de plus ni bouger ni attaquer
    {
        if(isEnemy)
        {
            aiScript.isFreeze = true;
        }
    }

    public void UnFreeze()
    {
        if(isEnemy)
        {
            aiScript.isFreeze = false;
        }
    }



    /// <summary>
    /// Niveau de slow entre 1 et 10 : -7% de speed à chaque niveau
    /// </summary>
    /// <param name="_power"></param>
    public void Slow(float _power)
    {
        if (isEnemy)
        {
            if(isSlow == false)
            {
                doHits.Slow(_power); // Diminue la vitesse d'attaque
                aiScript.Slow(_power);  // Diminue la vitesse de marche
                isSlow = true;
            }
        }     
    }

    public void UnSlow()
    {
        if (isEnemy)
        {
            doHits.UnSlow();
            aiScript.UnSlow();
            isSlow = false;
        }
    }



}
