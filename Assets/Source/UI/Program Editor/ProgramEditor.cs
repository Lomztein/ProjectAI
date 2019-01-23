using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Exceptions;
using Lomztein.ProjectAI.Flowchart.Nodes.Prefabs;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets;
using Lomztein.ProjectAI.Unit;
using Lomztein.ProjectAI.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public class ProgramEditor : MonoBehaviour {
        
        // Resources.
        public static Resource<GameObject> PrefabButtonElement = new Resource<GameObject> ("UI/Flowchart/NodePrefabButton");
        public static Resource<GameObject> NodeWidget { get { return NodePrefabButton.NodeElement; } }

        // Cached resources.
        public static ProgramEditor CurrentEditor { get; set; }
        public IProgrammable CurrentProgrammable { get; private set; }
        public Program CurrentProgram { get { return CurrentProgrammable.Program; } }

        private List<WorkspaceElement> AllActiveElements { get; set; }
        private Dictionary<WorkspaceElement, ErrorSign> ActiveErrorSigns { get; set; }

        // Unity-related references.
        public GameObject[] NodeWidgetComponents;
        public RectTransform nodePrefabList;
        public RectTransform eventParent;
        public GameObject toEdit;
        public ProgramEditorWorkspace workspace;

        public List<NodeWidget> eventWidgets = new List<NodeWidget> ();

        public void Awake() {
            CurrentEditor = this;
            NodeWidgetComponents = NodeWidgetComponents.ToList().OrderBy(x => x.GetComponent<NodeWidgetComponent>().Depth).ToArray();
        }

        public void AddElement (WorkspaceElement element) {
            AllActiveElements.Add (element);
        }

        public void RemoveElement (WorkspaceElement element) {
            AllActiveElements.Remove (element);
        }

        public void Initialize() {

            AllActiveElements = new List<WorkspaceElement> ();
            ActiveErrorSigns = new Dictionary<WorkspaceElement, ErrorSign> ();

            CurrentProgrammable = toEdit.GetComponent<IProgrammable> ();
            CreateEventList ();

            INodePrefab[] prefabs = CurrentProgrammable.GetAvailableNodePrefabs ();
            foreach (INodePrefab prefab in prefabs) {
                GameObject obj = Instantiate (PrefabButtonElement.Get (), nodePrefabList);
                NodePrefabButton prefabButton = obj.GetComponent<NodePrefabButton> ();
                prefabButton.Initialize (prefab, workspace.workspace as RectTransform);
            }

            Executor.CurrentExecutor.OnFlowchartException += OnFlowchartExceptionCaught;
        }

        private void OnFlowchartExceptionCaught(FlowchartException exception) {
            WorkspaceElement exceptionElement = AllActiveElements.Find (x => x.InnerElement == exception.Element);

            if (!ActiveErrorSigns.ContainsKey (exceptionElement)) {
                ErrorSign sign = ErrorSign.CreateSign (exceptionElement.transform.position, transform, exception.Message);
                ActiveErrorSigns.Add (exceptionElement, sign);
                sign.button.onClick.AddListener (() => { ActiveErrorSigns.Remove (exceptionElement); });
            }

            Debug.LogError ("Flowchart exception was caught - " + exception.Message, exceptionElement);
        }

        private void CreateEventList() {
            foreach (var eventNode in CurrentProgram.EventNodes) {

                GameObject newEventWidget = Instantiate (NodeWidget.Get (), eventParent);
                NodeWidget nodeWidget = newEventWidget.GetComponent<NodeWidget> ();
                nodeWidget.Initialize (eventNode);

                newEventWidget.GetComponentInChildren<Button> ().interactable = false;
                newEventWidget.AddComponent<LayoutElement> ();

            }
        }

    }

}
