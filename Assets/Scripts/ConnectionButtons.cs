using Unity.Netcode;
using UnityEngine;

public class ConnectionButtons : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

}
