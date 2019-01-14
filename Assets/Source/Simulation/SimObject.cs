using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Sim
{
    public class SimObject : MonoBehaviour
    {
        public Simulation Sim { get; private set; }
        private readonly List<SimComponent> _components = new List<SimComponent>();

        public event Action OnDestroyed;

        public void Init (Simulation sim)
        {
            Sim = sim;
            CacheComponents();
        }

        private void CacheComponents ()
        {
            _components.AddRange(gameObject.GetComponentsInChildren<SimComponent>());
        }

        public void AddComponent (SimComponent component)
        {
            _components.Add(component);
        }

        public void RemoveComponent (SimComponent component)
        {
            _components.Remove(component);
        }

        public void Tick (float deltaTime)
        {
            for (int i = 0; i < _components.Count; i++)
            {
                _components[i].Tick(deltaTime);
            }
        }

        public void Destroy ()
        {
            OnDestroyed?.Invoke();
        }
    }
}
