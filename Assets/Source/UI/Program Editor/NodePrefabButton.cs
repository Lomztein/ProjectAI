using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Prefabs;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets;
using Lomztein.ProjectAI.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public class NodePrefabButton : MonoBehaviour {

        public INodePrefab Prefab { get; private set; }

        public static Resource<GameObject> NodeElement = new Resource<GameObject> ("UI/Flowchart/Node Components/NodeWidget");

        private RectTransform ParentWorkspace { get; set; }

        // Unity-related fields, cannot be properties because Unity.
        public Button button;
        public Text headerText;
        public Text descriptionText;

        public void Initialize(INodePrefab prefab, RectTransform workspace) {

            headerText.text = prefab.Name;
            descriptionText.text = prefab.Description;

            Prefab = prefab;
            ParentWorkspace = workspace;
            button.onClick.AddListener (() => OnClicked ());
        }

        private void OnClicked () {
            Node createdNode = Prefab.Create (ProgramEditor.CurrentEditor.CurrentProgram);
            GameObject newNodeWidget = Instantiate (NodeElement.Get (), ParentWorkspace);
            NodeWidget nodeWidget = newNodeWidget.GetComponent<NodeWidget> ();
            nodeWidget.Initialize (createdNode);
        }

    }
}
