using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    [SerializeField] public Collider areaOfHit;
    [SerializeField] public GameObject icon;
    [SerializeField] private string m_enemyName;
    [SerializeField] private float m_hitCooldown;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_health;
    [SerializeField] private float m_hitDamage;
    [SerializeField] private float m_critical;
    [SerializeField] private int m_mana;

    private float m_multSpeed = 1;
    private float m_multHealth = 1;
    private float m_multHitDamage = 1;
    private float m_multCritical = 1;
    private float m_multHitCooldown = 1;

    private int m_costSpeed = 0;
    private int m_costHealth = 0;
    private int m_costHitDamage = 0;
    private int m_costCritical = 0;
    private int m_costHitCooldown = 0;

    public string enemyName { get { return m_enemyName; } private set { m_enemyName = value; } }
    public float hitCooldown { get { return m_hitCooldown * m_multHitCooldown; } private set { m_hitCooldown = value; } }
    public float speed { get { return m_speed * m_multSpeed; } private set { m_speed = value; } }
    public float health { get { return m_health * m_multHealth; } private set { m_health = value; } }
    public float hitDamage { get { return m_hitDamage * m_multHitDamage; } private set { m_hitDamage = value; } }
    public float critical { get { return m_critical * m_multCritical; } private set { m_critical = value; } }
    public int mana { get { return m_mana; } private set { m_mana = value; } }
}
