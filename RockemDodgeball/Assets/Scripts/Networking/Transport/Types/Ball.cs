using System;

public class BallData
{
    public int ballId;
    public Vector3Data position;
    public QuaternionData rotation;
    public Vector3Data velocity;
    public short ricochetCount;
    public long lastThrownBy;
    public bool isLive;
    public int damagePoints;
}
