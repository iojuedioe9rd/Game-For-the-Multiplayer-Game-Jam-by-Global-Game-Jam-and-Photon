using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace GamePlay
{
    public class EnemySpawner : MonoBehaviour
    {
        [System.Serializable]
        public class Wave
        {
            public string waveName;
            [System.Serializable]
            public class WaveEnemies
            {
                public int noOfEnemies;
                public string EnemyName;
            }
            public List<WaveEnemies> waveEnemies;
        }

        public List<Wave> waves = new List<Wave>();
        public Transform[] spawnPoints;

        private Wave currentWave;
        private int currentWaveNum = 0;

        // Start is called before the first frame update
        void Start()
        {
            currentWave = waves[currentWaveNum];
        }

        float t = 5f;

        // Update is called once per frame
        void Update()
        {
            if(Time.time >= t)
            {
                SpawnWave();
                t = Time.time + 5f;
            }
        }

        IEnumerator Enemy(Wave.WaveEnemies enemy)
        {
            for (int i = 0; i < enemy.noOfEnemies; i++)
            {
                PhotonNetwork.Instantiate(enemy.EnemyName, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                yield return new WaitForSeconds(10);
            }
            
        }

        void SpawnWave()
        {
            foreach (var enemy in currentWave.waveEnemies)
            {


                Coroutine coroutine = StartCoroutine(Enemy(enemy));
                print(coroutine);
            }
        }
    }
}
