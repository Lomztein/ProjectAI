using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Connections {

    public static class ConnectionExtensions {

        public static bool Connect(this IConnection connection, IHook one, IHook two) {

            IHook from;
            IHook to;

            Hook.GetOrderedIO (one, two, out from, out to);

            if (connection.AllowConnection (from, to) && connection.CanConnect (from, to)) {

                connection.From = from;
                connection.To = to;

                from.AddConnection (connection);
                to.AddConnection (connection);

                return true;
            }

            return false;
        }

        public static bool AllowConnection (this IConnection connection, IHook one, IHook two) {

            if (one == null || two == null)
                return false;

            // TODO: Implement multidirectional hooks to this.
            if (one.Direction == two.Direction)
                return false;

            if (one.ParentNode == two.ParentNode)
                return false;

            if (!one.AllowConnection (two) || !two.AllowConnection (one))
                return false;

            IHook from;
            IHook to;

            Hook.GetOrderedIO (one, two, out from, out to);

            if (from == null || to == null)
                return false;

            return true;

        }

    }
}
