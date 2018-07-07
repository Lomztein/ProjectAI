using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart {

    public interface IExecutor {

        void Execute (IExecutable executeable);

    }
}
