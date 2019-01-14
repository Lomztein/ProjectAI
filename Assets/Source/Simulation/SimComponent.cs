using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Sim
{
    public abstract class SimComponent : MonoBehaviour {

        public SimObject Parent { get; private set; }

        private void OnCreate ()
        {

        }

        public abstract void Init();
        public abstract void Tick(float deltaTime);
        public abstract void Kill();

    }
}
