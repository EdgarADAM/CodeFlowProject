using Newtonsoft.Json;
using System.Text;

namespace DoctorsApi.Helpers
{
    public static class Serializer
    {
        public static byte[]? Serialize(this object model)
        {
           if (model == null)
                {
                    return null;
                }

                var stringJson = JsonConvert.SerializeObject(model);
                return Encoding.ASCII.GetBytes(stringJson);
            }
        

        public static object? Deserialize(this byte[] arrBytes, Type model)
        {
            var stringJson = Encoding.Default.GetString(arrBytes);
            return JsonConvert.DeserializeObject(stringJson, model);
        }
    }
}
