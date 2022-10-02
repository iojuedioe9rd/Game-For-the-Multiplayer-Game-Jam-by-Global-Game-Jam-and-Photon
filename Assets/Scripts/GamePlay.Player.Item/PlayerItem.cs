using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using ExitGames.Client.Photon;

namespace GamePlay.Player.Item
{
    public class PlayerItem : MonoBehaviourPunCallbacks
    {

        public TMP_Text playerName;

        public Image backImg;
        public Color highlightColor;
        public GameObject leftArrButt;
        public GameObject rightArrButt;

        ExitGames.Client.Photon.Hashtable playerProp = new ExitGames.Client.Photon.Hashtable();
        public Image playerAvatar;
        public Sprite[] avatars;

        Photon.Realtime.Player player;

        public void SetPlayerInfo(Photon.Realtime.Player player)
        {
            playerName.text = player.NickName;
            this.player = player;
            UpdatePlayerItem(player);
        }

        public void ApplyLocalChanges()
        {
            backImg.color = highlightColor;
            leftArrButt.SetActive(true);
            rightArrButt.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            backImg = GetComponent<Image>();
            playerProp["playerAvatar"] = 0;
            PhotonNetwork.SetPlayerCustomProperties(playerProp);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void OnClickLeftArr()
        {
            if ((int)playerProp["playerAvatar"] == 0)
            {
                playerProp["playerAvatar"] = avatars.Length - 1;
            }
            else
            {
                playerProp["playerAvatar"] = (int)playerProp["playerAvatar"] - 1;
            }
            PhotonNetwork.SetPlayerCustomProperties(playerProp);
        }

        void UpdatePlayerItem(Photon.Realtime.Player player)
        {
            if (player.CustomProperties.ContainsKey("playerAvatar"))
            {
                int v = (int)player.CustomProperties["playerAvatar"];
                playerAvatar.sprite = avatars[v];
                playerProp["playerAvatar"] = v;
            }else
            {
                playerProp["playerAvatar"] = 0;
            }
        }

        public override void OnPlayerPropertiesUpdate(Photon.Realtime.Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
        {
            if(player == targetPlayer)
            {
                UpdatePlayerItem(player);
            }
        }

        public void OnClickRightArr()
        {
            if ((int)playerProp["playerAvatar"] == avatars.Length - 1)
            {
                playerProp["playerAvatar"] = 0;
            }
            else
            {
                playerProp["playerAvatar"] = (int)playerProp["playerAvatar"] + 1;
            }
            PhotonNetwork.SetPlayerCustomProperties(playerProp);
        }
    }
}
