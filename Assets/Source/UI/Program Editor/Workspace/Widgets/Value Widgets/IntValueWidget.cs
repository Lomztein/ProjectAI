using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Widgets.ValueWidgets {

    public class IntValueWidget : StringValueWidget {

        public override Type[] CompatableTypes {
            get {
                return new Type[] { typeof (int), typeof (long) };
            }
        }

        public override void OnValueChanged() {
            ValueNode.Value = int.Parse (inputField.text);
        }
    }
}
