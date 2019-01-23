using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Nodes {
    public static class NodeExtensions {

        private static IHook FindHookByName (IEnumerable<IHook> hooks, string name) {
            foreach (var hook in hooks) {
                if (hook.Name == name)
                    return hook;
            }

            return null;
        }

    }
}
