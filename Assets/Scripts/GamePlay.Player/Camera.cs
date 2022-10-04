using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace GamePlay.Player
{
    public class Camera : MonoBehaviour
    {
        private void Update()
        {
            Vector2 dir = Vector2.MoveTowards(transform.position, UnityEngine.Camera.main.transform.position, 2.5f);

            UnityEngine.Camera.main.transform.position = new Vector3(dir.x, dir.y, -10f);
        }
    }
}

