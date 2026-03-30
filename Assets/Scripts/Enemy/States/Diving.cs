using UnityEngine;
namespace Enemy.States
{
    public class Diving : BaseActor
    {
        #region Properties
        public override string Name => "Diving";
        #endregion

        #region Custom
        public override void Enter()
        {
            var actor = (EnemyBehavior)Data["Actor"];
            actor.transform.SetParent(null);
        }

        public override void Update()
        {
            var actor = (EnemyBehavior)Data["Actor"];

            // Use World Space so rotation doesn't break direction
            actor.transform.Translate(Vector3.down * actor.speed * Time.deltaTime, Space.World);

            if (actor.transform.position.y < -6f)
            {
                actor.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}