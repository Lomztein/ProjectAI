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
        public List<IConnection> Connections { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public abstract IConnection CreateConnection();

        public Hook (Program _parentProgram, Node _parent, Direction _direction, int _maxConnections) : base (_parentProgram) {
            ParentNode = _parent;
            Direction = _direction;
            MaxConnections = _maxConnections;
            Connections = new List<IConnection> ();
        }

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
