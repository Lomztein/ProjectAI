using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI {

    public class ToolTip : MonoBehaviour {

        public Text toolTipText;

        public IToolTipContext CurrentContext { get; set; }
        public string Tip { get { return CurrentContext != null ? CurrentContext.Tip : null; } }

        public static ToolTip CurrentToolTip { get; set; }

        private void Awake() {
            CurrentToolTip = this;
            Reset ();
        }

        private void Update() {
            transform.position = Input.mousePosition;
        }

        public static void Set (IToolTipContext context) {
            CurrentToolTip.CurrentContext = context;
            CurrentToolTip.UpdateText ();
        }

        public static void Reset () {
            CurrentToolTip.CurrentContext = null;
            CurrentToolTip.UpdateText ();
        }

        public void UpdateText () {
            toolTipText.text = Tip;
            CurrentToolTip.gameObject.SetActive (!string.IsNullOrEmpty (Tip));
        }

    }

}
