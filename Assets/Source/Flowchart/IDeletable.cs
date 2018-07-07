using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lomztein.ProjectAI.Flowchart {

    public delegate void OnDeletedEvent();

    public interface IDeletable {

        void Delete();

        event OnDeletedEvent OnDeleted;

    }

}
