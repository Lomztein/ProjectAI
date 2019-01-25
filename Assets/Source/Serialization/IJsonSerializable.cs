using Newtonsoft.Json.Linq;

namespace Lomztein.ProjectAI.Serialization
{
    public interface IJsonSerializable
    {
        JToken Serialize();

        void Deserialize(JToken source);
    }
}
