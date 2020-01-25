using UnityEngine;

public class GamestateController : MonoBehaviour
{
    // Responsibilities
    // 1. Provide interface to send and receive updates from NetworkedObjects
    // 2. Maintain local gamestate update rate
    // 3. Forward current local gamestate to NetworkBroker
    // 4. Receive global gamestate updates from NetworkBroker

    public string playerName;
    public long playerId;
    public int framesPerSecond = 60;
    public GameObject player;
    public OldNetworkedObject enemyObject;
    public OldNetworkedObject[] ballObjects;

    private ClientGamestateUpdate queuedGamestate;
    private NetworkBroker networkBroker;
    private float updateTimer;
    private float updateFrequency;

    void Awake()
    {
        networkBroker = transform.GetComponent<NetworkBroker>();
        updateFrequency = (1f / framesPerSecond);
        queuedGamestate = new ClientGamestateUpdate();
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
        queuedGamestate.player.name = playerName;
        queuedGamestate.player.playerId = playerId;
        networkBroker.SendClientUpdateToServer(queuedGamestate);
    }

    public void ReceiveGamestate(ServerGamestateUpdate gamestate)
    {
        // Update Enemy player
        UpdateEnemy(gamestate.enemy);

        // Update Balls
        //foreach (BallData ball in gamestate.balls)
        //{
        //    UpdateBall(ball);
        //}
    }

    // Outgoing Client -> Server Methods
    public void PlayerUpdate(Vector3 position, Quaternion rotation)
    {
        queuedGamestate.player.position = new Vector3Data(position);
        queuedGamestate.player.rotation = new QuaternionData(rotation);
    }

    // Incoming Server -> Client Methods
    private void UpdateEnemy(PlayerData enemyInfo)
    {
        Vector3Data rawPos = enemyInfo.position;
        Vector3 pos = new Vector3(rawPos.x, rawPos.y, rawPos.z);

        QuaternionData rawRot = enemyInfo.rotation;
        Quaternion rot = new Quaternion(rawRot.x, rawRot.y, rawRot.z, rawRot.w);

        enemyObject.SetTransform(pos, rot);
    }

    private void UpdateBall(BallData ball)
    {
        //ballObjects[ball.ballId].SetTransform()
    }
}
