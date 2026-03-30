namespace Enemy.States
{
    public abstract class BaseActor: Base
    {
        // Shorthand helpers using your new GetData<T> method
        protected EnemyBehavior Actor => GetData<EnemyBehavior>("Actor");
        protected StateMachine FSM => GetData<StateMachine>("FSM");
    }
}