using UnityEngine;

namespace Enemy.States
{
    public class Spawning : BaseActor
    {
        #region Properties
        public override string Name => "Spawning";
        #endregion

        #region Custom
        public override void Update()
        {
            var actor = (EnemyBehavior)Data["Actor"];
            var fsm = (StateMachine)Data["FSM"];
    
            Vector3 targetWorldPos = actor.formationTransform.TransformPoint(actor.localHomePosition);
    
            // Move towards slot
            actor.transform.position = Vector3.MoveTowards(actor.transform.position, targetWorldPos, actor.speed * Time.deltaTime);
    
            // Rotation logic
            Vector3 direction = targetWorldPos - actor.transform.position;
            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
                actor.transform.rotation = Quaternion.Slerp(actor.transform.rotation, targetRot, actor.rotateSpeed * Time.deltaTime);
            }
    
            // Arrival
            if (Vector3.Distance(actor.transform.position, targetWorldPos) < 0.01f)
            {
                actor.transform.SetParent(actor.formationTransform);
                actor.transform.localPosition = actor.localHomePosition;
                actor.transform.localRotation = Quaternion.Euler(0, 0, 180f); // Face down
                
                fsm.SetActiveState<InFormation>();
            }
        }
        #endregion
    }
}