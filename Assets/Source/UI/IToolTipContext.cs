using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.EventSystems;

namespace Lomztein.ProjectAI.UI {

    public interface IToolTipContext : IPointerEnterHandler, IPointerExitHandler {

        string Tip { get; set; }

    }
}
