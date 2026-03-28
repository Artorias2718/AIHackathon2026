using UnityEngine;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        #region Properties
        public enum State { Spawning, InFormation, Diving, Returning }
        public State currentState = State.Spawning;

        [Header("Movement Settings")]
        public float speed = 5f;
        public float rotateSpeed = 10f;

        private Transform formationTransform;
        private Vector3 localHomePosition;
        #endregion

        #region UnityEngine
        private void Update()
        {
            switch (currentState)
            {
                case State.Spawning:
                    HandleSpawning();
                    break;
                case State.InFormation:
                    HandleInFormation();
                    break;
                case State.Diving:
                    HandleDiving();
                    break;
            }
        }
        #endregion

        #region Custom
        public void SetHome(Transform formation, Vector3 localPos)
        {
            formationTransform = formation;
            localHomePosition = localPos;
            currentState = State.Spawning;
        }

        private void HandleSpawning()
        {
            // Calculate the world position of the slot in the moving formation
            Vector3 targetWorldPos = formationTransform.TransformPoint(localHomePosition);

            // Move towards the slot
            transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, speed * Time.deltaTime);

            // Rotate to face the direction of travel
            Vector3 direction = targetWorldPos - transform.position;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }

            // Check if we arrived
            if (Vector3.Distance(transform.position, targetWorldPos) < 0.1f)
            {
                // Parent the enemy so it moves with the formation 'Sway' automatically
                transform.SetParent(formationTransform);
                transform.localPosition = localHomePosition;
                transform.localRotation = Quaternion.identity;
                currentState = State.InFormation;
            }
        }

        private void HandleInFormation()
        {
            // The formation parent handles the movement; we just stay put.
            // You could add a tiny 'wiggle' animation here if you like.
        }

        private void HandleDiving()
        {
            // Unparent so we can move independently of the formation
            if (transform.parent != null) transform.SetParent(null);

            // Simple downward movement (you can replace this with a Bezier curve later)
            transform.Translate(Vector2.down * speed * Time.deltaTime);

            // Recycle if off-screen
            if (transform.position.y < -6f) 
            {
                gameObject.SetActive(false);
            }
        }
        #endregion
    }
}