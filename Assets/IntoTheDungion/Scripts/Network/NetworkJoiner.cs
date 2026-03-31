using Unity.Netcode;
using UnityEngine;

public class NetworkJoiner : MonoBehaviour
{
    [SerializeField]
    NetworkManager manager;

    public void HostServer()
    {
        if(manager != null)
        {
            manager.StartHost();
            Destroy(this.gameObject);
        }
    }

    public void JoinServer()
    {
        if(manager != null)
        {
            manager.StartClient();
            Destroy(this.gameObject);
        }
    }
}
