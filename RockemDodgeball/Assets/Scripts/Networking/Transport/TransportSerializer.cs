using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.IO;

public class TransportSerializer
{
    public static byte[] Combine(params byte[][] arrays)
    {
        byte[] rv = new byte[arrays.Sum(a => a.Length)];
        int offset = 0;
        foreach (byte[] array in arrays)
        {
            System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
            offset += array.Length;
        }
        return rv;
    }

    public static string Serialize<T>(ref T content)
    {
        return JsonConvert.SerializeObject(content);
    }

    public static T Deserialize<T>(byte[] data) where T : class
    {
        using (var stream = new MemoryStream(data))
        using (var reader = new StreamReader(stream, Encoding.UTF8))
            return JsonSerializer.Create().Deserialize(reader, typeof(T)) as T;
    }
}