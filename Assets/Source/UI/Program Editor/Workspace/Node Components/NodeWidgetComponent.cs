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

        public abstract void LoadFrom(INodeComponent source);

        public enum Position { In = -1, Inner = 0, Out = 1 }
        public abstract Position GetPosition();

        public bool IsApplicable (object obj)
            => ApplicableComponents.Any(x => x.IsInstanceOfType(obj));
    }
}