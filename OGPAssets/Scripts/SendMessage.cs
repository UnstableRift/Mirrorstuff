using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class SendMessage : NetworkBehaviour
{
    // Start is called before the first frame update
    //[SerializeField] private TMP_Text _inputText;
    [SerializeField] private TMP_Text _display;
    [SerializeField] private TMP_Text _outPut;

    // Sends message when enter is pressed
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) 
        { 
            MyGlobalServerRpc();
            UpdateName();
            ServerRpc();
            ChangeNameServerRPC();
            Debug.Log("Trigger methods");
        }
    }
    [ServerRpc]
    public void MyGlobalServerRpc(ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            Debug.Log(clientId, _display);
        }
    }

    void UpdateName()
    {
        _display.text = _outPut.text.ToString();
        Debug.Log("Update");
    }
    [ServerRpc(RequireOwnership = false)]
    void ServerRpc()
    {
        _display.text = "> " + _outPut.text.ToString();
    }

    [ServerRpc]
    private void ChangeNameServerRPC()
    {
        ChangeNameClientRPC();
        _display.text = "> " + _outPut.text.ToString();
    }
    [ClientRpc]
    private void ChangeNameClientRPC()
    {
        _display.text = "> " + _outPut.text.ToString();
        Debug.Log("Made to end");
    }
}

