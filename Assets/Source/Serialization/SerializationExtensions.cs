using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.ProjectAI.Serialization
{
    public static class SerializationExtensions
    {
        public static void Save (this IJsonSerializable serializable, string path)
        {
            JObject obj = serializable.Serialize();
            TextIO.Save(path, obj.ToString());
        }

        public static void Load (this IJsonSerializable serializable, string path)
        {
            JObject obj = JObject.Parse(TextIO.Load(path));
            serializable.Deserialize(obj);
        }
    }
}
