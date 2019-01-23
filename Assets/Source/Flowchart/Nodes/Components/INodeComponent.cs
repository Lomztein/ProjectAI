using Lomztein.ProjectAI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.ProjectAI.Flowchart.Nodes
{
    public interface INodeComponent : IJsonSerializable
    {
        void Init(Node parentNode);
    }
}
