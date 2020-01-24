using UnityEngine;

public class GamestateController : MonoBehaviour
{
    // Responsibilities
    // 1. Provide interface to send and receive updates from NetworkedObjects
    // 2. Maintain local gamestate update rate
    // 3. Forward current local gamestate to NetworkBroker
    // 4. Receive global gamestate updates from NetworkBroker

    public ClientUpdate queuedGamestate;
    public int framesPerSecond = 60;
    public GameObject player;
    public OldNetworkedObject enemy;

    private NetworkBroker networkBroker;
    private float updateTimer;
    private float updateFrequency;

    void Awake()
    {
        networkBroker = transform.GetComponent<NetworkBroker>();
        updateFrequency = (1f / framesPerSecond);
    }

    void FixedUpdate()
    {
        updateTimer += Time.deltaTime;
        if (updateTimer > updateFrequency)
        {
            updateTimer = 0.0f;
            SendGamestate();
        }
    }

    private void SendGamestate()
    {
        networkBroker.SendClientUpdateToServer(queuedGamestate);
    }

    public void ReceiveGamestate(ServerUpdate gamestate)
    {
        // This is where most of the action happens
        Vector3 newEnemyPos = new Vector3(gamestate.xPos, gamestate.yPos, gamestate.zPos);
        Quaternion newEnemyRot = new Quaternion(gamestate.xRot, gamestate.yRot, gamestate.zRot, gamestate.wRot);
        UpdateEnemyTransform(newEnemyPos, newEnemyRot);
    }

    // Outgoing Client -> Server Methods
    public void PlayerUpdate(Vector3 position, Quaternion rotation)
    {
        queuedGamestate.xPos = position.x;
        queuedGamestate.yPos = position.y;
        queuedGamestate.zPos = position.z;

        queuedGamestate.xRot = rotation.x;
        queuedGamestate.yRot = rotation.y;
        queuedGamestate.zRot = rotation.z;
        queuedGamestate.wRot = rotation.w;
    }

    // Incoming Server -> Client Methods
    private void UpdateEnemyTransform(Vector3 pos, Quaternion rot)
    {
        enemy.SetTransform(pos, rot);
    }
}
