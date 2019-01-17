using Lomztein.ProjectAI.Flowchart.Nodes;
using Newtonsoft.Json.Linq;
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

        public void Deserialize(JObject source)
        {
            X = source.GetValue ("X").ToObject<double>();
            Y = source.GetValue ("Y").ToObject<double>();
        }

        public JObject Serialize()
        {
            return new JObject()
            {
                { "X", X },
                { "Y", Y }
            };
        }
    }

}