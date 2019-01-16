using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    public enum enemyTypeEnum { Distance, Melee }

    [SerializeField] public Collider areaOfHit;
    [SerializeField] public GameObject icon;
    [SerializeField] private string m_enemyName;
    [SerializeField] private float m_hitCooldown;
    [SerializeField] private float m_speed;
    [SerializeField] private float m_health;
    [SerializeField] private float m_hitDamage;
    [SerializeField] private float m_critical;
    [SerializeField] private enemyTypeEnum m_enemyType;

    /*[SerializeField] private float m_multSpeed;
    [SerializeField] private float m_multHealth;
    [SerializeField] private float m_multHitDamage;
    [SerializeField] private float m_multCritical;
    [SerializeField] private float m_multHitCooldown;*/

    public string enemyName { get { return m_enemyName; } private set { m_enemyName = value; } }
    public float hitCooldown { get { return m_hitCooldown; } private set { m_hitCooldown = value; } }
    public float speed { get { return m_speed; } private set { m_speed = value; } }
    public float health { get { return m_health; } private set { m_health = value; } }
    public float hitDamage { get { return m_hitDamage; } private set { m_hitDamage = value; } }
    public float critical { get { return m_critical; } private set { m_critical = value; } }
    public enemyTypeEnum enemyType { get { return m_enemyType; } private set { m_enemyType = value; }  }

}
