using UnityEngine;

namespace Enemy.States
{
    public class Firing : BaseActor
    {
        #region Properties
        public override string Name => "Firing";
        #endregion

        #region Custom
        public override void Enter()
        {
            var weapon = Actor.GetComponent<Weapon>();
            if (weapon != null)
            {
                weapon.Fire();
            }
            
            FSM.SetActiveState<Diving>();
        }
        #endregion
    }
}