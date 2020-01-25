using System;

public class ClientGamestateUpdate
{
    public ClientMetadata metadata;
    public PlayerData player;
    public BallData[] balls;

    public ClientGamestateUpdate()
    {
        metadata = new ClientMetadata();
        player = new PlayerData();
    }
}

public class ServerGamestateUpdate
{
    public long tickTimestamp;
    public GameData gameInfo;
    public PlayerData enemy;
    public BallData[] balls;
}
