using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GamePlay.Matchmaking
{
    public class RoomItem : MonoBehaviour
    {

        public TMP_Text roomName;
        LobbyManager manager;


        public void SetRoomName(string roomName)
        {
            this.roomName.text = roomName;
        }

        public void OnClickItem()
        {
            print("hi");
            manager.JoinRoom(roomName.text);
        }

        // Start is called before the first frame update
        void Start()
        {
            manager = FindObjectOfType<LobbyManager>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
