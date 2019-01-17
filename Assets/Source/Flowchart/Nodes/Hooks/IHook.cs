using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Hooks {

    public enum Direction { In = -1, Uni = 0, Out = 1 };

    public interface IHook : IFlowchartElement, INamed {

        Node ParentNode { get; set; }

        Direction Direction { get; set; }

        int MaxConnections { get; set; }
        List<IConnection> Connections { get; set; }

        IConnection CreateConnection();

        void Init();

    }

}
