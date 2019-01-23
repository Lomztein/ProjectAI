using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Components
{
    public abstract class NodeComponent : INodeComponent
    {
        public abstract void Init(Node parentNode);

        public virtual JObject Serialize() { return new JObject(); }
        public virtual void Deserialize(JObject source) { }
    }
}
