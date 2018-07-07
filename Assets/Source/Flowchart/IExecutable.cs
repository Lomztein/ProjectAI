using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart {
    public interface IExecutable {

        void Execute(ExecutionMetadata metadata);

    }
}
