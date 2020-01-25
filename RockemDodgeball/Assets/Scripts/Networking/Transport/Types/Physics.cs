using System;
using System.IO;
using UnityEngine;

public class Vector3Data
{
    public float x;
    public float y;
    public float z;

    public Vector3Data(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public void WriteStream(BinaryWriter writer)
    {
        writer.Write(this.x);
        writer.Write(this.y);
        writer.Write(this.z);
    }

    public byte[] ToArray()
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream);

        WriteStream(writer);

        return stream.ToArray();
    }

    public static Vector3 FromArray(byte[] bytes)
    {
        var reader = new BinaryReader(new MemoryStream(bytes));

        var s = default(Vector3Data);
        s.x = reader.ReadSingle();
        s.y = reader.ReadSingle();
        s.z = reader.ReadSingle();

        return new Vector3(s.x, s.y, s.z);
    }
}

public class QuaternionData
{
    public float x;
    public float y;
    public float z;
    public float w;

    public QuaternionData(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }

    public void WriteStream(BinaryWriter writer)
    {
        writer.Write(this.x);
        writer.Write(this.y);
        writer.Write(this.z);
        writer.Write(this.w);

    }

    public byte[] ToArray()
    {
        var stream = new MemoryStream();
        var writer = new BinaryWriter(stream);

        WriteStream(writer);

        return stream.ToArray();
    }

    public static Quaternion FromArray(byte[] bytes)
    {
        var reader = new BinaryReader(new MemoryStream(bytes));

        var s = default(QuaternionData);
        s.x = reader.ReadSingle();
        s.y = reader.ReadSingle();
        s.z = reader.ReadSingle();
        s.w = reader.ReadSingle();


        return new Quaternion(s.x, s.y, s.z, s.w);
    }
}