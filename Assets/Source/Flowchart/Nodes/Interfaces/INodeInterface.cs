using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Lomztein.ProjectAI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces
{
    public enum Direction { In = -1, Uni = 0, Out = 1 };

    public interface INodeInterface : IJsonSerializable
    {
        Direction Direction { get; set; }

        List<IHook> InterfaceHooks { get; set; }

        void Init(Node parent, Direction direction);

        void Delete();
    }
}
