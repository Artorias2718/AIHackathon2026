using System.Collections.Generic;

namespace Enemy.States
{
    public abstract class Base
    {
        #region Properties
        public Dictionary<string, object> Data { get; private set; } = new Dictionary<string, object>();
        public abstract string Name { get; }
        public virtual void Enter() {}
        public virtual void Update() {}
        public virtual void Exit() {}
        #endregion

        #region Custom
        public T GetData<T>(string key)
            => (T) Data[key];
        #endregion
    }
}