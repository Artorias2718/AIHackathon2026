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
            if(!Data.ContainsKey("Timer"))
            {
                float side = Random.value > 0.5f ? 1f : -1f;

                Data["Timer"] = 0f;
                Data["StartPos"] = Actor.transform.position;
                Data["ControlPos"] = Actor.transform.position + new Vector3(5f * side, -3f, 0);
                Data["EndPos"] = new Vector3(Actor.transform.position.x, -7f, 0);
                Data["HasFired"] = false;
                
                Actor.transform.SetParent(null); // Break away from the swaying formation
            }
        }

        public override void Update()
        {
            float t = (float)Data["Timer"];
            t += Time.deltaTime * 0.5f; // Adjust for dive speed
            Data["Timer"] = t;

            Vector3 p0 = (Vector3)Data["StartPos"];
            Vector3 p1 = (Vector3)Data["ControlPos"];
            Vector3 p2 = (Vector3)Data["EndPos"];

            // Quadratic Bezier Formula: (1-t)^2*P0 + 2(1-t)t*P1 + t^2*P2
            Vector3 positionOnCurve = Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;

            // Update rotation to look where it's flying
            Vector3 dir = positionOnCurve - Actor.transform.position;
            if (dir != Vector3.zero)
            {
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                Actor.transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if(!GetData<bool>("HasFired") && t >= 0.4f)
            {
                Data["HasFired"] = true;
                FSM.SetActiveState<Firing>();
                return;
            }

            Actor.transform.position = positionOnCurve;

            if (t >= 1f) FSM.SetActiveState<Returning>();
        }
        #endregion
    }
}