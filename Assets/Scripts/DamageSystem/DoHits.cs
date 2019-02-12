using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoHits : MonoBehaviour {

    private bool canDoHitsCoroutine = true;
    private float oldHitCooldown;
    private float slowTimeAdd = 0f;

    // Les attributs de bases. Ils peuvent être initialié directement dans l'inspector
    [SerializeField] private float m_hitDamage;
    [SerializeField] private float m_hitCooldown;
    [SerializeField] private float m_critical;

	//sound and particle to hit
	public GameObject PrefabSound;
	public Transform SpawnSound;

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
        float multCrit_ = Random.Range(2f, 3f);
        float multRand_ = Random.Range(0.85f, 1.15f);

        if (crit_ < m_critical) // Si coup critique réussi
        {
            _takeHits.takeHits(multRand_ * m_hitDamage * multCrit_, true);
        }
        else
        {
            _takeHits.takeHits(multRand_ * m_hitDamage, false);
        }

		//Spawn Sound
		GameObject newSound = Instantiate(PrefabSound, SpawnSound.position, SpawnSound.rotation, null);
    }

    // Fonction utilisé dans le script AIScript
    // Permet d'attaquer
    public void Attack(TakeHits _takeHits)
    {
        if(canDoHitsCoroutine && m_hitCooldown > 0f)
        {
            StartCoroutine(DoHitsCoroutine(_takeHits));
        }
    }

    private IEnumerator DoHitsCoroutine(TakeHits takeHits_)
    {
        canDoHitsCoroutine = false;
        doHits(takeHits_);
        yield return new WaitForSeconds((1f / hitCooldown) + slowTimeAdd);
        canDoHitsCoroutine = true;
    }

    public void Slow(float _level)
    {
        oldHitCooldown = hitCooldown;
        slowTimeAdd = (1f / hitCooldown) * (_level / 100f);
    }

    public void UnSlow()
    {
        hitCooldown = oldHitCooldown;
        slowTimeAdd = 0f;
    }

    //Placeholder functions for Animation events
    void Hit()
    {

    }
}