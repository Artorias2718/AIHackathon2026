using UnityEngine;
using Enemy.States;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        private StateMachine fsm;

        [Header("Movement Settings")]
        public float speed = 5f;
        public float rotateSpeed = 10f;

        [Header("Formation Settings")]
        public Transform formationTransform;
        public Vector3 localHomePosition;

        private void Start()
        {
            fsm = new StateMachine();

            // Register all possible behaviors
            fsm.AddState(CreateState<Spawning>());
            fsm.AddState(new InFormation()); // Doesn't need data usually
            fsm.AddState(CreateState<Diving>());
            fsm.AddState(CreateState<Returning>());

            // Kick off the sequence
            fsm.SetActiveState<Spawning>();
        }

        private void Update()
        {
            fsm.Update();
        }

        // Helper to inject the necessary references into the State's Data bucket
        private T CreateState<T>() where T : Base, new()
        {
            T state = new T();
            state.Data["Actor"] = this;
            state.Data["FSM"] = fsm;
            return state;
        }
        
        // This is a public trigger you can call from a FormationManager 
        // when it's time for this specific enemy to dive!
        public void TriggerDive()
        {
            fsm.SetActiveState<Diving>();
        }
    }
}