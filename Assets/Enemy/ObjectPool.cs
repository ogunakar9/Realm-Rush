using System;
using System.Collections;
using UnityEngine;

    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] GameObject enemyPrefab;
        [SerializeField] [Range(0, 50)] int poolSize = 5;
        [SerializeField] [Range(0.1f, 30f)] float spawnTimer = 1f;

        GameObject[] pool;

        private void Awake()
        {
            PopulatePool();
        }

        void Start()
        {
            StartCoroutine(SpawnEnemy());
        }

        void PopulatePool()
        {
            pool = new GameObject[poolSize];

            for (int i = 0; i < pool.Length; i++)
            {
                pool[i] = Instantiate(enemyPrefab, transform);
                pool[i].SetActive(false);
            }
        }

        void EnableObjectInPool()
        {
            for (int i = 0; i < pool.Length; i++)
            {
                if (pool[i].activeSelf != true)
                {
                    pool[i].SetActive(true);
                    return;
                }
            }
        }
        
        IEnumerator SpawnEnemy()
        {
            while (true)
            {
                EnableObjectInPool();
                yield return new WaitForSeconds(spawnTimer);
            }
        }
    }
