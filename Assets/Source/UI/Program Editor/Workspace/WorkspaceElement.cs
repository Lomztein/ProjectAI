using Lomztein.ProjectAI.Flowchart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace {

    public abstract class WorkspaceElement : MonoBehaviour {

        public abstract IFlowchartElement InnerElement { get; }

        public virtual void Awake () {
            ProgramEditor.CurrentEditor.AddElement (this);
        }

        public virtual void OnDestroy () {
            ProgramEditor.CurrentEditor.RemoveElement (this);
        }

    }
}
