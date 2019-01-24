using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets;
using System;
using System.Linq;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public abstract class NodeWidgetComponent : MonoBehaviour
    {
        public abstract Type[] ApplicableComponents { get; }
        public abstract int Depth { get; }

        public NodeWidget ParentWidget { get; set; }

        public abstract void LoadFrom(Node source);

        public bool IsApplicable (Node obj)
            => ApplicableComponents.Any(x => obj.HasComponent (x) || x.IsInstanceOfType (obj));
    }
}