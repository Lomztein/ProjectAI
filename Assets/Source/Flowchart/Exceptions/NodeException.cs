using Lomztein.ProjectAI.Flowchart.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Exceptions {

    public class NodeException : FlowchartException {

        public Node Node { get { return Element as Node; } protected set { Element = value; } }

        public NodeException(string _message, Node _node) : this (_message, _node, null) { }

        public NodeException(string _message, Node _node, Exception _innerException) : base (_message, _node, _innerException) { }


    }

}
