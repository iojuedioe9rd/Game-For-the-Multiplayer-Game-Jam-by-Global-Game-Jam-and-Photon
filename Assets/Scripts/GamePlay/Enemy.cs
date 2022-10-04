using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.HealthSystem;
using Photon.Pun;
using GamePlay.All;

namespace GamePlay
{
    public class Enemy : MonoBehaviourHealthSystem
    {
        Transform[] players;

        Transform GetClosestPlayer(Transform[] players)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (Transform potentialTarget in players)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            return bestTarget;
        }

        void GetPlayers()
        {
            GameObject[] GameObjectNewPlayers = GameObject.FindGameObjectsWithTag("Player");
            List<Transform> newPlayers = new List<Transform>();

            foreach (var player in GameObjectNewPlayers)
            {
                newPlayers.Add(player.transform);
            }

            players = newPlayers.ToArray();
        }

        private void Awake()
        {
            healthSystem = new HealthSystem.HealthSystem(10);
            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead(object sender, System.EventArgs e)
        {
            PhotonNetwork.Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            GetPlayers();
        }

        float t = 5f;

        // Update is called once per frame
        void Update()
        {
            Transform player = GetClosestPlayer(players);

            if(Vector3.Distance(transform.position, player.position) <= 8f)
            {
                if(Time.time >= t)
                {
                    t += 5f;
                    Vector2 pos = (Vector2)player.position + (player.GetComponent<IPlayer>().GetDir() * 5);

                    GameObject bullet = PhotonNetwork.Instantiate("Bullet", pos, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(pos * 20f, ForceMode2D.Impulse);
                }
                
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, player.position, 2.5f);
        }
    }
}
