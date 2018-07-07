using Lomztein.ProjectAI.Flowchart.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lomztein.ProjectAI.Flowchart {

    // TODO: Completely refactor Executor class and interface, to better allow for multiple types of executors. It is currently a mess between static and non-static space.
    public class Executor : MonoBehaviour {

        public static Executor CurrentExecutor { get; set; }

        public static int TickRate { get { return (int)(1 / Time.fixedDeltaTime); } }
        public List<Program> TickingPrograms { get; private set; }

        private Queue<IExecutable> ExecutionQueue { get; set; }
        private bool IsExecuting { get; set; }

        public delegate void FlowchartExceptionEvent (FlowchartException exception);
        public event FlowchartExceptionEvent OnFlowchartException;

        // Events
        public delegate void OnTickEvent(float deltaTime);
        public event OnTickEvent OnTick;

        public void Awake() {
            CurrentExecutor = this;
            TickingPrograms = new List<Program> ();
            ExecutionQueue = new Queue<IExecutable> ();
        }

        public void AddProgram (Program program) {
            TickingPrograms.Add (program);
        }

        public void RemoveProgram (Program program) {
            TickingPrograms.Remove (program);
        }

        private void FixedUpdate() {
            Tick ();
        }

        public void Tick () {
            foreach (Program program in TickingPrograms) {
                program.ExecuteEvent ("Tick", Time.fixedDeltaTime);
            }

            if (OnTick != null)
                OnTick (Time.fixedDeltaTime);
        }

        public void RootExecute (IExecutable executable) {

            EnqueueOnCurrent (executable);

            try {
                ExecuteAll ();
            } catch (FlowchartException exception) {

                if (OnFlowchartException != null)
                    OnFlowchartException (exception);

            } catch (Exception criticalException) {
                Debug.LogException (criticalException);
            } finally {
                IsExecuting = false;
            }
        }

        public static void EnqueueOnCurrent (IExecutable executable) {
            CurrentExecutor.EnqueueExecutable (executable);
        }

        public static void EnqueueAllOnCurrent (IEnumerable<IExecutable> executables) {
            foreach (var executable in executables)
                EnqueueOnCurrent (executable);
        }

        // TODO: Execution logic could use a major refacoring, as it has been quite spaghettified at the moment.
        public void ExecuteAll (ExecutionMetadata metadata = null) {

            if (metadata == null) // If null, then create a fresh one.
                metadata = new ExecutionMetadata ();

            if (IsExecuting)
                return;

            IsExecuting = true;

            while (ExecutionQueue.Count != 0) {
                IExecutable executable = ExecutionQueue.Dequeue ();
                executable.Execute (metadata);
            }

            IsExecuting = false;
        }

        public void EnqueueExecutable (IExecutable executable) {
            ExecutionQueue.Enqueue (executable);
        }

    }

}
