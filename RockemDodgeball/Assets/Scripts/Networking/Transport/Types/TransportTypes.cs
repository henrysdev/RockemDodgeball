using System;
using UnityEngine;
using System.IO;









// A Gamestate "Tick"
//public class ServerUpdate
//{
//    public long timestamp;
//    public EnemyInfo enemy;
//    public BallInfo[] balls;
//}

//public struct ClientUpdate
//{
//    public string userId;

//    public float xPos;
//    public float yPos;
//    public float zPos;

//    public float xRot;
//    public float yRot;
//    public float zRot;
//    public float wRot;
//}

//public struct EnemyInfo
//{
//    public string userId;

//    public float xPos;
//    public float yPos;
//    public float zPos;

//    public float xRot;
//    public float yRot;
//    public float zRot;
//    public float wRot;
//}

//public struct BallInfo
//{
//    public string ballId;

//    public float xPos;
//    public float yPos;
//    public float zPos;

//    public float xRot;
//    public float yRot;
//    public float zRot;
//    public float wRot;

//    public float xVel;
//    public float yVel;
//    public float zVel;
//}


//struct UpdatePacket
//{
//    // Universal fields
//    public short messageType;
//    public long userId;
//    public long timestamp;
//    public long gameId;
//    public long matchId;
//    public Vector3Data playerPosition;
//    public QuaternionData playerRotation;
//    public Vector3Data playerVelocity;

//    public UpdatePacket(
//        short messageType,
//        long userId,
//        long timestamp,
//        long gameId,
//        long matchId,
//        Vector3 playerPosition,
//        Quaternion playerRotation,
//        Vector3 playerVelocity)
//    {
//        this.messageType = messageType;
//        this.userId = userId;
//        this.timestamp = timestamp;
//        this.gameId = gameId;
//        this.matchId = matchId;
//        this.playerPosition = new Vector3Data(playerPosition);
//        this.playerRotation = new QuaternionData(playerRotation);
//        this.playerVelocity = new Vector3Data(playerVelocity);
//    }

//    public byte[] ToArray()
//    {
//        var stream = new MemoryStream();
//        var writer = new BinaryWriter(stream);

//        writer.Write(this.messageType);
//        writer.Write(this.userId);
//        writer.Write(this.timestamp);
//        writer.Write(this.gameId);
//        writer.Write(this.matchId);
//        playerPosition.WriteStream(writer);
//        playerRotation.WriteStream(writer);
//        playerVelocity.WriteStream(writer);

//        return stream.ToArray();
//    }

//}

//struct SimpleTransform
//{
//    public Vector3 position;
//    public Quaternion rotation;

//    public SimpleTransform(Vector3 pos, Quaternion rot)
//    {
//        position = pos;
//        rotation = rot;
//    }
//}

//struct SimpleTransformData
//{
//    public Vector3Data position;
//    public QuaternionData rotation;

//    public SimpleTransformData(SimpleTransform trans)
//    {
//        position = new Vector3Data(trans.position);
//        rotation = new QuaternionData(trans.rotation);
//    }

//    public byte[] ToArray()
//    {
//        var stream = new MemoryStream();
//        var writer = new BinaryWriter(stream);

//        position.WriteStream(writer);
//        rotation.WriteStream(writer);

//        return stream.ToArray();
//    }

//    public static SimpleTransform FromArray(byte[] bytes)
//    {
//        var reader = new BinaryReader(new MemoryStream(bytes));

//        var pos = default(Vector3Data);
//        pos.x = reader.ReadSingle();
//        pos.y = reader.ReadSingle();
//        pos.z = reader.ReadSingle();
//        var fullPos = new Vector3(pos.x, pos.y, pos.z);

//        var rot = default(QuaternionData);
//        rot.x = reader.ReadSingle();
//        rot.y = reader.ReadSingle();
//        rot.z = reader.ReadSingle();
//        rot.w = reader.ReadSingle();
//        var fullRot = new Quaternion(rot.x, rot.y, rot.z, rot.w);

//        return new SimpleTransform(fullPos, fullRot);
//    }
//}

//public struct Vector3Data
//{
//    public float x;
//    public float y;
//    public float z;

//    public Vector3Data(Vector3 vector)
//    {
//        x = vector.x;
//        y = vector.y;
//        z = vector.z;
//    }

//    public void WriteStream(BinaryWriter writer)
//    {
//        writer.Write(this.x);
//        writer.Write(this.y);
//        writer.Write(this.z);
//    }

//    public byte[] ToArray()
//    {
//        var stream = new MemoryStream();
//        var writer = new BinaryWriter(stream);

//        writer.Write(this.x);
//        writer.Write(this.y);
//        writer.Write(this.z);

//        return stream.ToArray();
//    }

//    public static Vector3 FromArray(byte[] bytes)
//    {
//        var reader = new BinaryReader(new MemoryStream(bytes));

//        var s = default(Vector3Data);
//        s.x = reader.ReadSingle();
//        s.y = reader.ReadSingle();
//        s.z = reader.ReadSingle();

//        return new Vector3(s.x, s.y, s.z);
//    }
//}

//public struct QuaternionData
//{
//    public float x;
//    public float y;
//    public float z;
//    public float w;

//    public QuaternionData(Quaternion quaternion)
//    {
//        x = quaternion.x;
//        y = quaternion.y;
//        z = quaternion.z;
//        w = quaternion.w;
//    }

//    public void WriteStream(BinaryWriter writer)
//    {
//        writer.Write(this.x);
//        writer.Write(this.y);
//        writer.Write(this.z);
//        writer.Write(this.w);

//    }

//    public byte[] ToArray()
//    {
//        var stream = new MemoryStream();
//        var writer = new BinaryWriter(stream);

//        writer.Write(this.x);
//        writer.Write(this.y);
//        writer.Write(this.z);
//        writer.Write(this.w);

//        return stream.ToArray();
//    }

//    public static Quaternion FromArray(byte[] bytes)
//    {
//        var reader = new BinaryReader(new MemoryStream(bytes));

//        var s = default(QuaternionData);
//        s.x = reader.ReadSingle();
//        s.y = reader.ReadSingle();
//        s.z = reader.ReadSingle();
//        s.w = reader.ReadSingle();


//        return new Quaternion(s.x, s.y, s.z, s.w);
//    }
//}
