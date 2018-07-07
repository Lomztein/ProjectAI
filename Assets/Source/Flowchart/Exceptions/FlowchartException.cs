using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Exceptions {

    public class FlowchartException : Exception {

        public IFlowchartElement Element { get; protected set; }

        public FlowchartException(string _message, IFlowchartElement _element) : this (_message, _element, null) { }

        public FlowchartException (string _message, IFlowchartElement _element, Exception _innerException) : base (_message, _innerException) {
            Element = _element;
        }

    }

}
