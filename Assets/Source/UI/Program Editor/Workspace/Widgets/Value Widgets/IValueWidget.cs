using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Widgets.ValueWidgets {

    public interface IValueWidget {

        Type[] CompatableTypes { get; }

        HookAttachment OutputHookWidget { get; set; }
        OutputHook OutputHook { get; set; }

        ValueNode ValueNode { get; set; }

        void OnValueChanged ();

        void Initialize(ValueNode valueNode);

    }

}
