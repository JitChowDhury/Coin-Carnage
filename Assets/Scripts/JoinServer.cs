using Unity.Netcode;
using UnityEngine;

public class JoinServer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Join()
    {
        NetworkManager.Singleton.StartClient();
    }
}
