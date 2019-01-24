using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Flow;
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
        public NodeWidgetComponent[] Components { get; private set; }

        public Vector2 Position { get { return transform.position; } set { transform.position = value; } }

        public Node Node { get; set; }
        protected override IDeletable Deletable { get { return Node; } }
        public override IFlowchartElement InnerElement { get { return Node; } }

        // Unity references.
        public Text nameHeader;
        public Button dragButton;

        public void Initialize(Node node)
        {
            Node = node;
            Node.OnDeleted += () => { Destroy(gameObject); };

            List<NodeWidgetComponent> createdComponents = new List<NodeWidgetComponent>();

            foreach (GameObject possibleComponentObject in AvailableNodeComponents)
            {
                NodeWidgetComponent possibleComponent = possibleComponentObject.GetComponent<NodeWidgetComponent>();
                if (possibleComponent.IsApplicable(node))
                {
                    NodeWidgetComponent newComponent = Instantiate(possibleComponentObject).GetComponent<NodeWidgetComponent>();
                    newComponent.ParentWidget = this;
                    newComponent.LoadFrom(node);
                    createdComponents.Add(newComponent);

                    newComponent.transform.SetParent(transform);
                }
            }
            Components = createdComponents.ToArray();
        }

        public void OnMove() {
            
        }
    }

}
