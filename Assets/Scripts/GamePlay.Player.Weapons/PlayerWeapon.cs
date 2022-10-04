using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace GamePlay.Player.Weapons
{
    public class PlayerWeapon : MonoBehaviour
    {
        public Transform firePoint;

        float bulletForce = 20f;

        PhotonView view;

        // Start is called before the first frame update
        void Start()
        {
            view = GetComponent<PhotonView>();
        }

        // Update is called once per frame
        void Update()
        {
            if(!view.IsMine) { return; }
            if(Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        }
    }
}
