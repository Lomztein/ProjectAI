using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public class CornerAnchor : MonoBehaviour, IWorkspaceAnchor {

        public Vector2 Position { get { return transform.position; } }
        public Vector2 Size { get { return (transform as RectTransform).sizeDelta; } }

    }
}
