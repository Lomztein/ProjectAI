using Newtonsoft.Json.Linq;

namespace Lomztein.ProjectAI.Serialization
{
    public interface IJsonSerializable
    {
        JObject Serialize();

        void Deserialize(JObject source);
    }
}
