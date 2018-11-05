using System;
using System.Collections;
using System.Collections.Generic;
using Lomztein.ProjectAI.Flowchart;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor.Workspace.NodeComponents
{
    public class Name : NodeComponent
    {
        public override Type[] ApplicableTypes => new Type[] { typeof(INamed) };
        public override int Depth => 0;

        public Text NameText;

        public override void LoadFrom(IFlowchartElement source)
        {
            // Easier than using if (source is INamed named)? Not really, but it does catch any errors that are unlikely to ever happen anyways.
            Preconditions.IsType (source, out INamed named);
            NameText.text = named.Name;
        }
    }
}
