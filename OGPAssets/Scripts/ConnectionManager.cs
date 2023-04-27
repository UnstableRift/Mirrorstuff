using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace AD0260
{
    public class ConnectionManager : MonoBehaviour
    {
        [SerializeField] private GameObject connectUI;
        [SerializeField] private GameObject disconnectUI;
#if UNITY_SERVER && !UNITY_EDITOR
        public void Start()
        {
            NetworkManager.Singleton.StartServer();
        }
#else
        public void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientDisconnetedCallback;
        }
        void OnClientDisconnetedCallback(ulong clientID)
        {
            if (NetworkManager.Singleton.LocalClientId == clientID)
            {
                disconnectUI.SetActive(true);
                connectUI.SetActive(false);
            }
        }
        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
            disconnectUI.SetActive(true);
            connectUI.SetActive(false);
        }
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
            disconnectUI.SetActive(true);
            connectUI.SetActive(false);
        }
        public void StartServer()
        {
            NetworkManager.Singleton.StartServer();
            disconnectUI.SetActive(true);
            connectUI.SetActive(false);
        }
        public void Disconnect()
        {
            NetworkManager.Singleton.Shutdown();
            disconnectUI.SetActive(false);
            connectUI.SetActive(true);
        }
#endif
    }


}
