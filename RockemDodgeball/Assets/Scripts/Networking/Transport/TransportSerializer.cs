using System.Linq;
using Newtonsoft.Json;
using System.Text;

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

    public static ServerUpdate DeserializeServerTick(byte[] content)
    {
        string stringContent = Encoding.ASCII.GetString(content);
        return JsonConvert.DeserializeObject<ServerUpdate>(stringContent);
    }
}