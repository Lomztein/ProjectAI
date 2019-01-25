using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Newtonsoft.Json.Linq;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces
{
    public abstract class NodeInterface : INodeInterface
    {
        public abstract Direction Direction { get; set; }
        public abstract List<IHook> InterfaceHooks { get; set; }

        public abstract void Delete();
        public abstract void Init(Node parent);

        public virtual JToken Serialize() { return new JObject(); }
        public virtual void Deserialize(JToken source) { }
    }
}
