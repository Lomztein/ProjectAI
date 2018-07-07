using Lomztein.ProjectAI.Flowchart.Nodes.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart.Exceptions {

    public class ConnectionException : FlowchartException {

        public IConnection Connection { get { return Element as IConnection; } protected set { Element = value; } }

        public ConnectionException(string _message, IConnection _connection) : this (_message, _connection, null) { }

        public ConnectionException(string _message, IConnection _connection, Exception _innerException) : base (_message, _connection, _innerException) { }

    }

}
