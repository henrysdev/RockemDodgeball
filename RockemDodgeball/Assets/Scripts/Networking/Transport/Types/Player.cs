using System;

public class PlayerData
{
    public long playerId;
    public short teamId;
    public string name;
    public Vector3Data position;
    public QuaternionData rotation;
    public Vector3Data velocity;
    public PlayerActionState actionState;
    public short ballCount;
    public PlayerHealthState healthState;
}

public class PlayerActionState
{
    public bool isMoving;
    public bool isGrounded;
    public bool isWindingUp;
    public bool isHit;
}

public class PlayerHealthState
{
    public short headHealth;
    public short leftArmHealth;
    public short rightArmHealth;
    public short chestHealth;
    public short abdomenHealth;
    public short leftLegHealth;
    public short rightLegHealth;
}
