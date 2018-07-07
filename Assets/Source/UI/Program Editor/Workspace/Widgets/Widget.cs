using Lomztein.ProjectAI.Flowchart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Widgets {

    /// <summary>
    /// A widget is any object that represents a major, independant part of the flowchart, primarily nodes.
    /// </summary>
    public abstract class Widget : WorkspaceElement, IBeginDragHandler, IDragHandler, IPointerClickHandler {

        private Vector2 DragOffset { get; set; }
        protected virtual IDeletable Deletable { get { return null; } }

        public void OnBeginDrag(PointerEventData eventData) {
            DragOffset = transform.position - Input.mousePosition;
        }

        public void OnDrag(PointerEventData eventData) {
            transform.position = (Vector2)Input.mousePosition + DragOffset;
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Right) {
                Deletable.Delete ();
            }
        }
    }

}
