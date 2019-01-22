using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : TakeHits {

    public enum enemyType { Melee, Distance }

    public Collider areaOfHit;
    //TODO 
    public GameObject icon;
    [SerializeField] private string m_enemyName;
    [SerializeField] private float m_hitCooldown;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_health;
    [SerializeField] private float m_hitDamage;
    [SerializeField] private float m_critical;
    [SerializeField] private float m_mana;
    [SerializeField] private enemyType m_type;
    [HideInInspector] public int ID;

    public string enemyName { get { return m_enemyName; } private set { m_enemyName = value; } }
    public float hitCooldown { get { return m_hitCooldown; } private set { m_hitCooldown = value; } }
    public float speed { get { return m_speed; } private set { m_speed = value; } }
    public float HitPoint { get { return m_health; } private set { m_health = value; } }
    public float hitDamage { get { return m_hitDamage; } private set { m_hitDamage = value; } }
    public float critical { get { return m_critical; } private set { m_critical = value; } }
    public float mana { get { return m_mana; } private set { m_mana = value; } }
    public enemyType type { get { return m_type; } private set { m_type = value; } }

    public void SetID(int _ID)
    {
        ID = _ID;
    }


    void doHit(TakeHits _takeHits)
    {
        _takeHits.HitPoints -= hitDamage;
    }

    public void SetStats(float _multSpeed, float _multHealth, float _multCritical, float _multHitDamage, float _multHitCooldown)
    {
        m_speed *= _multSpeed;
        m_health *= _multHealth;
        m_critical *= _multCritical;
        m_hitDamage *= _multHitDamage;
        m_hitCooldown *= _multHitCooldown;
    }

    public void SetCost(float _costMana)
    {
        m_mana += _costMana;
    }
}
