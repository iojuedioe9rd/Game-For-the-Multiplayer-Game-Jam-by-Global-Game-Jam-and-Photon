using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace GamePlay.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        public Transform[] spawnPoints;
        public GameObject[] playerPrefebs;


        // Start is called before the first frame update
        void Start()
        {
            int ranNum = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[ranNum];
            
            GameObject playerToSpawn = playerPrefebs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
