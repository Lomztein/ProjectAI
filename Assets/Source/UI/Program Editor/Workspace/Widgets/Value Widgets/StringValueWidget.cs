using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Widgets.ValueWidgets {

    public class StringValueWidget : ValueWidget {

        public InputField inputField;

        public override Type[] CompatableTypes {
            get {
                return new Type[] { typeof (string) };
            }
        }

        public override void Initialize(ValueNode valueNode) {
            base.Initialize (valueNode);
            if (ValueNode.Value != null)
                inputField.text = ValueNode.Value.ToString ();
        }

        public override void OnValueChanged() {
            ValueNode.Value = inputField.text;
        }
    }
}
