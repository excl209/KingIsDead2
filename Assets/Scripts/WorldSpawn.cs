using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawn : MonoBehaviour
{
    [SerializeField] GameObject OrcPrefab;
    [SerializeField] GameObject ArcherPrefab;
    [SerializeField] Transform OrcRallyPoint;
    [SerializeField] Transform HumanRallyPoint;
    //[SerializeField] List<GameObject> OrcHomes;
    [SerializeField] bool war;
    [SerializeField] bool stopMovingPpl;
    [SerializeField] bool stopMovingAllies;

    private bool spawning = true;
    [SerializeField] float spawnRate = 5f;

    EnemyBuildings[] enemyBuilding;
    HumanSpawner[] allyBuilding;

    private void Start()
    {
        enemyBuilding = FindObjectsOfType<EnemyBuildings>();
        allyBuilding = FindObjectsOfType<HumanSpawner>();
        war = false;
        stopMovingPpl = false;
    }

    void Update()
    {
        

        int rand = Random.Range(0, enemyBuilding.Length - 1);
        int randAlly = Random.Range(0, allyBuilding.Length - 1);

        if (spawning)
        {
            StartCoroutine(PopOutEnemies(enemyBuilding[rand]));
            StartCoroutine(PopOutAllies(allyBuilding[randAlly]));
            spawning = false;
        }
    }

    private IEnumerator PopOutEnemies(EnemyBuildings enemyBuilding)
    {
        Enemy[] enemyMaxLimiter = FindObjectsOfType<Enemy>();
        if (enemyMaxLimiter.Length < 20)
        {

            if (enemyBuilding.gameObject.activeInHierarchy)
            {
                GameObject newEnemies = Instantiate(OrcPrefab, enemyBuilding.transform.position + new Vector3(2f, 0f), Quaternion.identity);
                Transform me = FindObjectOfType<GameSession>().gameObject.transform;
                newEnemies.transform.parent = me.transform;
            }

        }
        else
        {
            war = true;
            if (war && !stopMovingPpl)
            {
                foreach (Enemy enemy in enemyMaxLimiter)
                {
                    enemy.transform.position = OrcRallyPoint.position;
                }
                stopMovingPpl = true;
            }
        }
        yield return new WaitForSeconds(spawnRate);
        spawning = true;
    }

    private IEnumerator PopOutAllies (HumanSpawner allyBuilding)
    {
        Allies[] allyMaxLimiter = FindObjectsOfType<Allies>();
        if (allyMaxLimiter.Length < 20)
        {

            if (allyBuilding.gameObject.activeInHierarchy)
            {
                GameObject newAllies = Instantiate(ArcherPrefab, allyBuilding.transform.position + new Vector3(1f, 0f), Quaternion.identity);
                Transform me = FindObjectOfType<GameSession>().gameObject.transform;
                newAllies.transform.parent = me.transform;
            }
        }
        else
        {
            war = true;
            if (war && !stopMovingAllies)
            {
                foreach (Allies ally in allyMaxLimiter)
                {
                    ally.transform.position = HumanRallyPoint.position;
                }
                stopMovingAllies = true;
            }
        }
        yield return new WaitForSeconds(spawnRate);
        spawning = true;
    }
}
