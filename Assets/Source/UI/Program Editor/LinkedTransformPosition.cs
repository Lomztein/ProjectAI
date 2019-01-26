using Lomztein.ProjectAI.Flowchart.Nodes;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.UI.Editor.ProgramEditor {

    public class LinkedTransformPosition : INodePosition {

        private readonly Transform transform;

        public LinkedTransformPosition(Transform _transform)
        {
            transform = _transform;
        }

        public double X {
            get {
                return transform.position.x;
            }

            set {
                transform.position.Set ((float)value, transform.position.y, transform.position.z);
            }
        }

        public double Y {
            get {
                return transform.position.y;
            }
            set {
                transform.position.Set (transform.position.x, (float)value, transform.position.z);
            }
        }

        public void Deserialize(JToken source)
        {
            X = (source as JObject).GetValue ("X").ToObject<double>();
            Y = (source as JObject).GetValue ("Y").ToObject<double>();
        }

        public JToken Serialize()
        {
            return new JObject()
            {
                { "X", X },
                { "Y", Y }
            };
        }
    }

}