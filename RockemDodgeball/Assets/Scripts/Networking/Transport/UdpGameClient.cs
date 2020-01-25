using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UdpGameClient : MonoBehaviour
{
    // Responsibilities
    // 1. Maintain connection with backend
    // 2. Send and receive updates via UDP
    // 3. Forward received messages to NetworkBroker

    public int clientListenPort = 8085;
    public int serverListenPort = 27189;
    public string serverUri = "127.0.0.1";

    private NetworkBroker networkBroker;
    private UdpClient udpClient;

    private void Awake()
    {
        networkBroker = transform.GetComponent<NetworkBroker>();
        udpClient = new UdpClient(clientListenPort);
        new Thread(() => RunUdpGameClient()).Start();
    }

    private void RunUdpGameClient()
    {
        InitializeConnection();
        ListenLoop();
    }

    // Send frame of player inputs to the server
    public void SendPacket(byte[] message)
    {
        // BYTE WAY (EFFICIENT)
        //var posData = new Vector3Data(playerPosition);
        //var posBytes = posData.ToArray();
        //var rotData = new QuaternionData(playerRotation);
        //var rotBytes = rotData.ToArray();
        //byte[] payload = TransportSerializer.Combine(posBytes, rotBytes);
        //udpClient.Send(payload, payload.Length);

        udpClient.Send(message, message.Length);
    }

    // Initialize connection. Send a message to the host to which you have connected.
    private void InitializeConnection()
    {
        try
        {
            udpClient.Connect(serverUri, serverListenPort);
        }
        catch (Exception e)
        {
            Debug.LogFormat(e.ToString());
            return;
        }
        Debug.Log("Successfully initialized connection with server");
    }

    // Listen for messages to receive
    private void ListenLoop()
    {
        Debug.Log("Listening for incoming messages from server");

        //IPEndPoint object will allow us to read datagrams sent from any source.
        IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, serverListenPort);

        while (true)
        {
            try
            {
                // Blocks until a message returns on this socket from a remote host.
                byte[] receiveBytes = udpClient.Receive(ref remoteIpEndPoint);
                ServerGamestateUpdate update = TransportSerializer.Deserialize<ServerGamestateUpdate>(receiveBytes);
                networkBroker.ReceiveUpdateFromServer(update);
            }
            catch (Exception e)
            {
                Debug.LogFormat(e.ToString());
            }
        }
    }
}
