using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Hooks {

    public abstract class Hook : FlowchartElement, IHook {

        public Node ParentNode { get; set; }
        public Direction Direction { get; set; }

        public int MaxConnections { get; set; }
        public List<IConnection> Connections { get; set; } = new List<IConnection>();

        public abstract IConnection CreateConnection();

        public Hook SetNode (Node parent)
        {
            ParentNode = parent;
            return this;
        }

        public Hook SetDirection (Direction direction)
        {
            Direction = direction;
            return this;
        }

        public Hook SetMaxConnections (int maxConnections)
        {
            MaxConnections = maxConnections;
            return this;
        }

        public abstract void Init();

        public static void GetOrderedIO(IHook one, IHook two, out IHook from, out IHook to) {

            from = null;
            to = null;

            if (one.Direction >= 0) {
                from = one;
                to = two;
            }else if (two.Direction >= 0) {
                from = two;
                to = one;
            }

        }

    }
}
