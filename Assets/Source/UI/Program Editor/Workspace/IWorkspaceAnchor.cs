using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public interface IWorkspaceAnchor {

        Vector2 Position { get; }
        Vector2 Size { get; }

    }

}
