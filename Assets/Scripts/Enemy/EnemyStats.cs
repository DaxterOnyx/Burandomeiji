using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TakeHits))]
public class EnemyStats : MonoBehaviour {

    public enum enemyType { Melee, Distance }
    [SerializeField] private Collider col;  // areaOfHits
    public GameObject icon;
    TakeHits takeHits;
    DoHits doHits;

    [SerializeField] private string m_enemyName;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_mana;
    [SerializeField] private enemyType m_type;
    [HideInInspector] public int ID;

    public string enemyName { get { return m_enemyName; } private set { m_enemyName = value; } }
    public float speed { get { return m_speed; } private set { m_speed = value; } }
    public float mana { get { return m_mana; } private set { m_mana = value; } }
    public enemyType type { get { return m_type; } private set { m_type = value; } }

    public void SetID(int _ID)
    {
        ID = _ID;
    }

    public void SetStats(float _multSpeed, float _multHealth, float _multCritical, float _multHitDamage, float _multHitCooldown)
    {
        takeHits = GetComponent<TakeHits>();
        doHits = col.GetComponent<DoHits>();
        takeHits.SetAttributs(_multHealth);
        doHits.SetAttributs(_multHitDamage, _multHitCooldown, _multCritical);

        m_speed *= _multSpeed;
    }

    public void SetCost(float _costMana)
    {
        m_mana += _costMana;
    }
}
