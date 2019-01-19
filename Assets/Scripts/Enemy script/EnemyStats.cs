using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public enum enemyType { Melee, Distance }

    public Collider areaOfHit;
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

    public float multSpeed { get; set; }
    public float multHealth { get; set; }
    public float multHitDamage { get; set; }
    public float multCritical { get; set; }
    public float multHitCooldown { get; set; }

    public float costSpeed { get; set; }
    public float costHealth { get; set; }
    public float costHitDamage { get; set; }
    public float costCritical { get; set; }
    public float costHitCooldown { get; set; }

    public string enemyName { get { return m_enemyName; } private set { m_enemyName = value; } }
    public float hitCooldown { get { return m_hitCooldown * multHitCooldown; } private set { m_hitCooldown = value; } }
    public float speed { get { return m_speed * multSpeed; } private set { m_speed = value; } }
    public float health { get { return m_health * multHealth; } private set { m_health = value; } }
    public float hitDamage { get { return m_hitDamage * multHitDamage; } private set { m_hitDamage = value; } }
    public float critical { get { return m_critical * multCritical; } private set { m_critical = value; } }
    public float mana { get { return (m_mana + costCritical + costHealth + costHitCooldown + costHitDamage + costSpeed); } private set { m_mana = value; } }
    public enemyType type { get { return m_type; } private set { m_type = value; } }

    public void GetID(int _ID)
    {
        ID = _ID;
    }

}
