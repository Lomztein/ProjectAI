using Lomztein.ProjectAI.Flowchart;
using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.Attachments {

    public class ConnectionAttachment : Attachment {

        public override IFlowchartElement InnerElement { get { return Connection; } }
        private IConnection Connection { get; set; }

        public Image image;

        public HookAttachment From { get; set; }
        public HookAttachment To { get; set; }

        public void UpdateGraphic () {

            SetTransform (From.transform.position, To.transform.position);

        }

        public void Update() {
            if (From != null && To != null) {
                UpdateGraphic ();
            }
        }

        public void SetTransform (Vector2 from, Vector2 to) {
            Vector2 center = (from + to) / 2f;
            float angle = Mathf.Atan2 (to.y - from.y, to.x - from.x) * Mathf.Rad2Deg;
            float length = Vector2.Distance (from, to);

            transform.position = center;
            transform.rotation = Quaternion.Euler (0, 0, angle);
            (transform as RectTransform).sizeDelta = new Vector2 (length, 15);
        }

        public void Initialize(IConnection connection, Color color) {
            Connection = connection;
            Connection.OnDeleted += () => { Destroy (gameObject); };
            image.color = color;
        }

        public void Connect (HookAttachment from, HookAttachment to) {
            From = from;
            To = to;
            UpdateGraphic ();
        }
    }
}
