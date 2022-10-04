using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.HealthSystem;
using Photon.Pun;

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

        // Update is called once per frame
        void Update()
        {
            Transform player = GetClosestPlayer(players);

            if(Vector3.Distance(transform.position, player.position) <= 8f)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, player.position, 2.5f);
        }
    }
}
