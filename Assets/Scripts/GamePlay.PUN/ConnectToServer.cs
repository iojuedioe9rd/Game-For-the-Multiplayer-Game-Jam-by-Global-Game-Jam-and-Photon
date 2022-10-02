using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

namespace GamePlay.PUN
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {

        public TMP_InputField usernameInput;
        public TMP_Text buttonText;

        public void OnClickConnect()
        {
            if(!(string.IsNullOrEmpty(usernameInput.text)) && usernameInput.text.Length >= 1)
            {
                PhotonNetwork.NickName = usernameInput.text;
                buttonText.text = "Connecting...";
                PhotonNetwork.AutomaticallySyncScene = true;
                PhotonNetwork.ConnectUsingSettings();

            }
        }

        public override void OnConnectedToMaster()
        {
            SceneManager.LoadScene("Lobby");
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
