using Lomztein.ProjectAI.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Lomztein.ProjectAI.UI {

    public class ErrorSign : MonoBehaviour {

        public static Resource<GameObject> ErrorSignPrefab = new Resource<GameObject> ("UI/ErrorSign");

        public ToolTipContext toolTip;
        public Button button;

        public static ErrorSign CreateSign (Vector3 position, Transform parent, string message) {

            GameObject newSignObject = Instantiate (ErrorSignPrefab.Get (), parent);
            newSignObject.transform.position = position;
            ErrorSign sign = newSignObject.GetComponent<ErrorSign> ();

            sign.toolTip.Tip = message;
            sign.button.onClick.AddListener (() => { Destroy (newSignObject); ToolTip.Reset (); });

            return sign;

        }

    }
}
