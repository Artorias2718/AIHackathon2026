using UnityEngine;

namespace Enemy.States
{
    public class Returning : BaseActor
    {
        public override string Name => "Returning";

        public override void Enter()
        {
            // 1. Teleport to the top of the screen (e.g., Y = 7)
            // Keep the same X position for a natural "vertical wrap" 
            Vector3 wrapPos = Actor.transform.position;
            wrapPos.y = 7f; 
            Actor.transform.position = wrapPos;
            
            // 2. Ensure we aren't parented so we can move freely back to slot
            Actor.transform.SetParent(null);
        }

        public override void Update()
        {
            // Calculate where the "Home" slot is in World Space right now
            Vector3 targetWorldPos = Actor.formationTransform.TransformPoint(Actor.localHomePosition);

            // Move towards the slot
            Actor.transform.position = Vector3.MoveTowards(
                Actor.transform.position, 
                targetWorldPos, 
                Actor.speed * Time.deltaTime
            );

            // Rotate to face the slot as we return
            Vector3 direction = targetWorldPos - Actor.transform.position;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
                Actor.transform.rotation = Quaternion.Slerp(Actor.transform.rotation, targetRot, Actor.rotateSpeed * Time.deltaTime);
            }

            // Arrival logic: Re-join the formation
            if (Vector3.Distance(Actor.transform.position, targetWorldPos) < 0.1f)
            {
                Actor.transform.SetParent(Actor.formationTransform);
                Actor.transform.localPosition = Actor.localHomePosition;
                Actor.transform.localRotation = Quaternion.Euler(0, 0, 180f); // Face player
                
                FSM.SetActiveState<InFormation>();
            }
        }
    }
}