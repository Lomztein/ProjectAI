using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Widgets.ValueWidgets {

    public class BoolValueWidget : ValueWidget {

        public Toggle toggle;

        public override Type[] CompatableTypes {
            get {
                return new Type[] { typeof (bool) };
            }
        }

        public override void Initialize(ValueNode valueNode) {
            base.Initialize (valueNode);
            if (valueNode.Value != null)
                toggle.isOn = (bool)valueNode.Value;
        }

        public override void OnValueChanged() {
            ValueNode.Value = toggle.isOn;
        }
    }
}
