using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
using Lomztein.ProjectAI.Flowchart.Nodes.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents;
using Lomztein.ProjectAI.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets {

    public class NodeWidget : Widget {

        public static GameObject[] AvailableNodeComponents => ProgramEditor.CurrentEditor.NodeWidgetComponents;
        public NodeComponent[] Components { get; private set; }

        public Vector2 Position { get { return transform.position; } set { transform.position = value; } }

        public Node Node { get; set; }
        protected override IDeletable Deletable { get { return Node; } }
        public override IFlowchartElement InnerElement { get { return Node; } }

        // Unity references.
        public Text nameHeader;
        public Button dragButton;

        public void Initialize (Node node) {

            Node = node;
            Node.OnDeleted += () => { Destroy (gameObject); };

            List<NodeComponent> createdComponents = new List<NodeComponent>();
            foreach (GameObject possibleComponentObject in AvailableNodeComponents)
            {
                Debug.Log(possibleComponentObject);
                NodeComponent possibleComponent = possibleComponentObject.GetComponent<NodeComponent>();
                if (possibleComponent.IsApplicable(InnerElement))
                {
                    NodeComponent newComponent = Instantiate(possibleComponentObject, transform).GetComponent<NodeComponent>();
                    newComponent.ParentWidget = this;
                    newComponent.LoadFrom(InnerElement);
                    createdComponents.Add(newComponent);
                }
            }
            Components = createdComponents.ToArray();

        }

        private void CreateIOHooks (RectTransform parent, GameObject prefab, IVariableHook[] hooks) {
            foreach (var io in hooks) {
                GameObject newHook = Instantiate (prefab, parent);
                newHook.GetComponent<HookAttachment> ().Initialize (io as IHook, TypeColors.GetColor (io.ValueType));
            }
        }

        public void OnMove() {
            
        }
    }

}
