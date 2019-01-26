﻿using Lomztein.ProjectAI.Flowchart.Nodes;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using Lomztein.ProjectAI.Flowchart.Nodes.Interfaces.Hooks;
using Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments;
using Lomztein.ProjectAI.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public class ProgramEditorWorkspace : MonoBehaviour, IDragHandler {

        // Resources
        public static Resource<GameObject> ConnectionElement = new Resource<GameObject> ("UI/Flowchart/ConnectionWidget");

        // Unity references.
        public RectTransform workspace;

        public CornerAnchor upperRightAnchor;
        public CornerAnchor upperLeftAnchor;
        public CornerAnchor buttomRightAnchor;
        public CornerAnchor buttomLeftAnchor;

        public GameObject[] valueWidgets;

        // TODO: Attempt to move this out of static space, since this actively hinders multiple editors open at once.
        private HookAttachment CurrentSelectedHook { get; set; }
        private ConnectionAttachment CurrentConnectionWidget { get; set; }
        private IConnection CreatingConnection { get; set; }

        public List<IWorkspaceAnchor> Anchors { get; private set; }

        private void Awake() {
            Anchors = new List<IWorkspaceAnchor> ();
            Anchors.AddRange (new List<IWorkspaceAnchor> () { upperRightAnchor, upperRightAnchor, buttomRightAnchor, buttomLeftAnchor });
        }

        public void AddAnchor (IWorkspaceAnchor anchor) {
            Anchors.Add (anchor);
        }

        public void RemoveAnchor (IWorkspaceAnchor anchor) {
            Anchors.Remove (anchor);
        }

        public void Resize () {

            Rect finalSize = new Rect ();

            foreach (IWorkspaceAnchor anchor in Anchors) {
                Bounds bounds = anchor.GetBounds ();
            }

            Debug.Log (finalSize);
            workspace.sizeDelta = new Vector2 (finalSize.width, finalSize.height);
        }

        public void OnDrag(PointerEventData eventData) {
            Debug.Log (eventData.button);
        }

        public void OnClickedHook(HookAttachment hookWidget) {
            if (CurrentSelectedHook == null) {

                CurrentSelectedHook = hookWidget;
                CreatingConnection = CurrentSelectedHook.Hook.CreateConnection ();
                CurrentConnectionWidget = Instantiate (ConnectionElement.Get (), workspace).GetComponent<ConnectionAttachment> ();
                CurrentConnectionWidget.Initialize (CreatingConnection, CurrentSelectedHook.image.color);

            } else {

                if (CreatingConnection.Connect (CurrentSelectedHook.Hook, hookWidget.Hook)) {
                    CurrentConnectionWidget.Connect (CurrentSelectedHook, hookWidget);
                } else {

                    if (CurrentSelectedHook.Hook.IsConnectedTo (hookWidget.Hook))
                        CurrentSelectedHook.Hook.GetConnectionTo (hookWidget.Hook).Delete ();

                    Destroy (CurrentConnectionWidget.gameObject);
                }

                CurrentSelectedHook = null;
                CurrentConnectionWidget = null;
                CreatingConnection = null;
            }
        }

        private void Update() {
            if (CurrentConnectionWidget != null) {
                CurrentConnectionWidget.SetTransform (CurrentSelectedHook.transform.position, Input.mousePosition);
            }
        }
    }
}
