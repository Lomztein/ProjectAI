using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public static class ProgramEditorExtensions {

        public static Bounds GetBounds (this IWorkspaceAnchor anchor) {

            return new Bounds {
                center = anchor.Position,
                size = anchor.Size
            };

        }

    }
}
