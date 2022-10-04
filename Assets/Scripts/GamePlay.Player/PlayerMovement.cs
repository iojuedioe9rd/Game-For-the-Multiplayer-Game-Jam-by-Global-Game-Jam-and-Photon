using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using GamePlay.All;

namespace GamePlay.Player
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour, IPlayer
    {
        public PhotonView view { get; private set; }
        public Rigidbody2D rb { get; private set; }

        public float moveSpeed = 5f;

        Vector2 movement = Vector2.zero;
        Vector2 mousePos = Vector2.zero;

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
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");

                movement.x = horizontal;
                movement.y = vertical;

                print(UnityEngine.Camera.main);
                mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        private void FixedUpdate()
        {
            Movement();
            Look();
        }

        void Movement()
        {
            

            rb.MovePosition(rb.position + (moveSpeed * Time.fixedDeltaTime * movement));
        }

        void Look()
        {
            if (view.AmOwner)
            {
                Vector2 lookDir = mousePos - rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
            }

                
        }

        public Vector2 GetDir()
        {
            return movement;
        }
    }
}
