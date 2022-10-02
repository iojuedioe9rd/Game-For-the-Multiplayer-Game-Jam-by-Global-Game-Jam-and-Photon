using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        public PhotonView view { get; private set; }
        public Rigidbody2D rb { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            view = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if(view.AmOwner)
            {
                print("hi");
            }
        }
    }
}
