using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public abstract class NodeComponent : MonoBehaviour
    {
        public abstract Type[] ApplicableTypes { get; }
        public abstract int Depth { get; }

        public NodeWidget ParentWidget { get; set; }

        public abstract void LoadFrom(Node source);

        public bool IsApplicable (object obj)
            => ApplicableTypes.Any(x => x.IsInstanceOfType(obj));
    }
}