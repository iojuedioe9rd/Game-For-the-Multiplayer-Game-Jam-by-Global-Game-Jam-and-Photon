using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
using GamePlay.Player.Item;

namespace GamePlay.Matchmaking
{
    public class LobbyManager : MonoBehaviourPunCallbacks
    {

        public TMP_InputField roomInputField;
        public GameObject lobbyPanel;
        public GameObject roomPanel;
        public TMP_Text roomName;

        public RoomItem roomItemPrefab;
        List<RoomItem> roomItemsList = new List<RoomItem>();
        public Transform contentObj;

        public float timeBetweenUpdates = 2.5f;
        float nextUpdateTime;

        float dis = 19.33f*4;

        public List<PlayerItem> playerItemsList = new List<PlayerItem>();
        public PlayerItem playerItemPrefab;
        public Transform playerItemParent;

        public GameObject playButton;

        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.JoinLobby();
        }

        public RoomOptions roomOptions
        {
            get
            {
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.MaxPlayers = 6;
                roomOptions.BroadcastPropsChangeToAll = true;
                return roomOptions;
            }
        }

        public void OnClickCreate()
        {
            if(!(string.IsNullOrEmpty(roomInputField.text)) && roomInputField.text.Length >= 1)
            {
                PhotonNetwork.CreateRoom(roomInputField.text, roomOptions);
            }
        }

        public override void OnJoinedRoom()
        {
            lobbyPanel.SetActive(false);
            roomPanel.SetActive(true);
            roomName.text = $"Room Name: {PhotonNetwork.CurrentRoom.Name}";
            UpdatePlayerList();
        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            UpdatePlayerList();
        }

        public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
        {
            UpdatePlayerList();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            if(Time.time >= nextUpdateTime)
            {
                UpdateRoomList(roomList);
                nextUpdateTime = Time.time + timeBetweenUpdates;
            }
            
        }

        public void SetDis(float v)
        {
            dis = v;
        }

        private void UpdateRoomList(List<RoomInfo> roomList)
        {
            int i = 0;
            foreach (RoomItem item in roomItemsList)
            {
                Destroy(item.gameObject);
            }
            roomItemsList.Clear();

            foreach (RoomInfo room in roomList)
            {
                
                RoomItem newRoom = Instantiate(roomItemPrefab, contentObj);
                Vector3 v = newRoom.transform.localPosition;
                v.y += -(i * dis);
                newRoom.transform.localPosition = v;
                newRoom.SetRoomName(room.Name);
                roomItemsList.Add(newRoom);
                i++;

            }
        }

        public void OnClickPlayButton()
        {
            PhotonNetwork.LoadLevel("Game");
        }


        // Update is called once per frame
        void Update()
        {
            // && PhotonNetwork.CurrentRoom.PlayerCount >= 2
            if (PhotonNetwork.IsMasterClient)
            {
                playButton.SetActive(true);
            }
            else
            {
                playButton.SetActive(false);
            }
        }

        public void JoinRoom(string roomName)
        {
            print("hi");
            PhotonNetwork.JoinRoom(roomName);
        }

        

        public void OnClickLeave()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            roomPanel.SetActive(false);
            lobbyPanel.SetActive(true);
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }

        void UpdatePlayerList()
        {
            foreach (PlayerItem item in playerItemsList)
            {
                Destroy(item.gameObject);
            }
            playerItemsList.Clear();

            if (PhotonNetwork.CurrentRoom == null) { return; }

            foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
            {
                PlayerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
                newPlayerItem.SetPlayerInfo(player.Value);

                if(player.Value == PhotonNetwork.LocalPlayer)
                {
                    newPlayerItem.ApplyLocalChanges();
                }

                playerItemsList.Add(newPlayerItem);
            }

        }
    }
}
