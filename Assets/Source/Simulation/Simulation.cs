using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Sim
{

    public class Simulation : MonoBehaviour {

        private readonly List<SimObject> _objects = new List<SimObject> ();

        public int ticksPerSecond = 50;
        private float tickBacklog;

        private void Awake()
        {
            CacheFromWorld();
        }

        public void CacheFromWorld ()
        {
            SimObject[] all = FindObjectsOfType<SimObject>();
            foreach (SimObject obj in all)
            {
                obj.Init(this);
                AddObject(obj);
            }
        }

        public SimObject Instantiate(GameObject obj, Vector3 position, Quaternion rotation)
        {
            GameObject gameObject = Object.Instantiate(obj, position, rotation);
            SimObject[] simObjects = gameObject.GetComponentsInChildren<SimObject>();
            if (simObjects.Length == 0)
            {
                throw new NonSimObjectInstantiated();
            }
            foreach (SimObject simObject in simObjects)
            {
                simObject.Init(this);
                AddObject(simObject);
            }
            return simObjects[0];
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;

            tickBacklog += deltaTime * ticksPerSecond;
            int toTick = Mathf.FloorToInt(tickBacklog);

            for (int i = 0; i < toTick; i++)
            {
                Tick(deltaTime);
            }

            tickBacklog = 0;
        }

        private void Tick (float deltaTime)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                _objects[i].Tick(deltaTime);
            }
        }

        private void AddObject (SimObject obj)
        {
            _objects.Add(obj);
        }

        private void RemoveObject (SimObject obj)
        {
            _objects.Remove(obj);
        }


        public void OnObjectDestroyed(SimObject obj)
        {
            RemoveObject(obj);
        }
    }
}