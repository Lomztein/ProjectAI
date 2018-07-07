using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Widgets.ValueWidgets {

    public abstract class ValueWidget : Widget, IValueWidget {

        public abstract Type[] CompatableTypes { get; }

        public HookAttachment outputHookWidget;
        public HookAttachment OutputHookWidget { get { return outputHookWidget; } set { outputHookWidget = value; } }
        public OutputHook OutputHook { get { return ValueNode.Output; } set { ValueNode.Output = value; } }

        public override IFlowchartElement InnerElement { get { return ValueNode; } }
        public ValueNode ValueNode { get; set; }
        protected override IDeletable Deletable {
            get {
                return ValueNode;
            }
        }

        public abstract void OnValueChanged();

        public virtual void Initialize (ValueNode valueNode) {
            valueNode.OnDeleted += () => { Destroy (gameObject); };
            ValueNode = valueNode;
            OutputHookWidget.Initialize (OutputHook, TypeColors.GetColor (ValueNode.ValueType));
        }

    }
}
