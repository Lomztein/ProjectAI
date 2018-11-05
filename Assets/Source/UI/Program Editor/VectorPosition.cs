using Lomztein.ProjectAI.Flowchart.Nodes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public class VectorPosition : INodePosition {

        private Vector2 position;

        public VectorPosition (float x, float y) {
            position = new Vector2 (x, y);
        }

        public double X {
            get {
                return position.x;
            }

            set {
                position.x = (float)value;
            }
        }

        public double Y {
            get {
                return position.y;
            }
            set {
                position.y = (float)value;
            }
        }
    }

}