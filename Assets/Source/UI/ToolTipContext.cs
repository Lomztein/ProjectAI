using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Lomztein.ProjectAI.UI {

    public class ToolTipContext : MonoBehaviour, IToolTipContext {

        public string toolTip;
        public string Tip { get { return toolTip; } set { toolTip = value; } }

        public void OnPointerEnter(PointerEventData eventData) {
            ToolTip.Set (this);
        }

        public void OnPointerExit(PointerEventData eventData) {
            ToolTip.Reset ();
        }
    }

}
