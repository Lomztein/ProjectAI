using Lomztein.ProjectAI.Flowchart.Exceptions;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks {
    public static class HookExtensions {

        public static void AddConnection (this IHook hook, IConnection connection) {
            if (hook.MaxConnections != 0 && hook.Connections.Count >= hook.MaxConnections)
                throw new HookException ("Hook has already reached maximum amount of connections.", hook);
            hook.Connections.Add (connection);
        }

        public static bool AllowConnection (this IHook hook, IHook other) {
            if (hook.IsConnectedTo (other))
                return false;

            if (hook.MaxConnections != 0 && hook.Connections.Count >= hook.MaxConnections)
                return false;

            return true;
        }

        public static bool IsConnectedTo (this IHook hook, IHook other) {
            return hook.Connections.Exists (x => x.From == other || x.To == other);
        }

        public static IConnection GetConnectionTo (this IHook hook, IHook other) {
            return hook.Connections.Find (x => x.From == other || x.To == other);
        }

        public static void Disconnect(this IHook hook, IConnection connection, bool deleteConnection = true) {
            hook.Connections.Remove (connection);

            if (deleteConnection)
                connection.Delete (); // Easy way to avoid stack overflows, though I feel there is a better solution.
        }

        public static void DisconnectAll (this IHook hook) {
            while (hook.Connections.Count > 0) {
                hook.Disconnect (hook.Connections[0]);
            }
        }

        public static void InitAll (this IEnumerable<IHook> hooks)
        {
            foreach (IHook hook in hooks)
                hook.Init();
        }

        public static void EnqueueAndExecuteNextNextNodes(this ChainHook chainHook) {
            Executor.EnqueueAllOnCurrent (chainHook.GetConnectedHooks<ChainHook> ());
            Executor.CurrentExecutor.ExecuteAll ();
        }

        public static T[] GetConnectedHooks<T>(this IHook hook) where T : class {
            List<T> nodes = new List<T> ();
            foreach (var connection in hook.Connections) {
                nodes.Add (connection.To as T);
            }
            return nodes.ToArray ();
        }

    }
}
