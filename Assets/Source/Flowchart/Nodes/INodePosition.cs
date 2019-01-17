using Lomztein.ProjectAI.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart.Nodes {

    public interface INodePosition : IJsonSerializable {

        double X { get; set; }
        double Y { get; set; }

    }

}