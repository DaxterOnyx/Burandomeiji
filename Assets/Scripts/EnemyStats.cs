using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    
    [SerializeField] private Collider m_areaOfHit;
    public Collider areaOfHit
    {
        get { return m_areaOfHit; }
        private set { m_areaOfHit = value; }
    }

    [SerializeField] private float m_hitCooldown;
    public float hitCooldown
    {
        get { return m_hitCooldown; }
        private set { m_hitCooldown = value; }
    }

    [SerializeField] private float m_speed;
    public float speed
    {
        get { return m_speed; }
        private set { m_speed = value; }
    }

    [SerializeField] private float m_health;
    public float health
    {
        get { return m_health; }
        private set { m_health = value; }
    }

    [SerializeField] private float m_mana;
    public float mana
    {
        get { return m_mana; }
        private set { m_health = value; }
    }

    [SerializeField] private float m_hitDamage;
    public float hitDamage
    {
        get { return m_hitDamage;  }
        private set { m_hitDamage = value; }
    }

    [SerializeField] private string m_enemyName;
    public string enemyName
    {
        get { return m_enemyName; }
        private set { m_enemyName = value; }
    }
}
