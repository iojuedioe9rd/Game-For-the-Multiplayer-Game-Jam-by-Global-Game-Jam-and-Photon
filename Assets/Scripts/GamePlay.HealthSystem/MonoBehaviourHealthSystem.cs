using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlay.JSON;

namespace GamePlay.HealthSystem
{
    public class MonoBehaviourHealthSystem : MonoBehaviour, IPunObservable, IHealthSystem
    {
        public HealthSystem healthSystem;

        public HealthSystem GetHealthSystem()
        {
            return healthSystem;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                if(healthSystem != null)
                {
                    stream.SendNext(healthSystem.ToJSON());
                }
                
            }
            else
            {
                if (healthSystem != null)
                {
                    healthSystem.SetJSON((string)stream.ReceiveNext());
                }
            }
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
