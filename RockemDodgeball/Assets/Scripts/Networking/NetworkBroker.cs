using UnityEngine;
using System.Text;

public class NetworkBroker : MonoBehaviour
{
    // Responsibilities
    // 1. Communicate with Transport layer
    // 2. Maintain Tick queue (TODO)
    // 3. Assemble updates to send to udp client to send to server on a defined tick rate

    private GamestateController gamestateController;
    private UdpGameClient udpClient;

    void Awake()
    {
        gamestateController = transform.GetComponent<GamestateController>();
        udpClient = transform.GetComponent<UdpGameClient>();
    }

    public void SendClientUpdateToServer(ClientGamestateUpdate update)
    {
        string message = TransportSerializer.Serialize(ref update);
        byte[] byteMessage = Encoding.UTF8.GetBytes(message);
        udpClient.SendPacket(byteMessage);
    }

    public void ReceiveUpdateFromServer(ServerGamestateUpdate update)
    {
        gamestateController.ReceiveGamestate(update);
    }
}
