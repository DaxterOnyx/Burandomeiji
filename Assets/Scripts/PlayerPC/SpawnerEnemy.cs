using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private float cooldown= 2f;
    [SerializeField] private GameObject enemy;
    [SerializeField] private float health = 1000f;
    [SerializeField] private int level = 0;
    private float currentHealth;
    [SerializeField] private int[] experienceTab;
    [SerializeField] private float xp = 0f;

    private void Start()
    {
        currentHealth = health;
    }

    private void Update()
    {
        if(level > 0)
        {
            if(xp < experienceTab[level - 1])
            {
                xp += Time.deltaTime;

                if(xp >= experienceTab[level - 1])
                {
                    LevelUp();
                }
            }
        }
    }

    /*public IEnumerator Spawn_One_Ennemy_Coroutine(Transform _spawnerTransform, GameObject _enemy)
    {
        GameObject enemyIns_ = Instantiate(_enemy, _spawnerTransform.position, _spawnerTransform.rotation);
        yield return new WaitForSeconds(_spawner.cooldown);
        StartCoroutine(Spawn_One_Ennemy_Coroutine(_spawnerTransform, _enemy, _spawner));
    }*/

    private void LevelUp()
    {

    }
}
