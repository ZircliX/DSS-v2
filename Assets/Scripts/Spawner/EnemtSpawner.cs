using System.Collections.Generic;
using DG.Tweening;
using DSS.Entities;
using LTX.Tools;
using UnityEngine;

namespace DSS.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private EnemyData enemyData;
        private List<Enemy> enemies;
        private DynamicBuffer<Enemy> enemyBuffer;
        private int maxEnemies = 10;
        
        public static EnemySpawner Instance { get; private set; }
        private bool gameStarted = false;
        
        private void Awake()
        {
            Instance = this;
            
            enemies = new List<Enemy>(10);
            enemyBuffer = new DynamicBuffer<Enemy>(10);
        }

        public void StartGame()
        {
            gameStarted = true;
        }
        
        public void StopGame()
        {
            gameStarted = false;
            ClearEnemies();
        }

        private void Update()
        {
            if (enemies.Count >= maxEnemies || !gameStarted) return;
            
            Vector3 spawnPosition = new Vector3(Random.Range(-13f, 13f), Random.Range(-13f, 13f), 0);
            SpawnEnemy(spawnPosition);
        }

        public void SpawnEnemy(Vector3 position)
        {
            Enemy newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
            newEnemy.Initialize(enemyData);
            enemies.Add(newEnemy);
        }
        
        public void RemoveEnemy(Health target)
        {
            enemyBuffer.CopyFrom(enemies);

            for (int index = 0; index < enemyBuffer.Length; index++)
            {
                Enemy enemy = enemyBuffer[index];
                
                if (enemy.Health.GetInstanceID() == target.GetInstanceID())
                {
                    target.DOKill();
                    enemies.Remove(enemy);
                    Destroy(enemy.gameObject);
                    return;
                }
            }
            
            Debug.LogWarning("eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
        }
        
        public void ClearEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }
            
            enemies.Clear();
        }
        
        public List<Enemy> GetEnemies()
        {
            return enemies;
        }
    }
}