using System;
using TheLastLand._Project.Scripts.StateMachines.Common;

namespace TheLastLand._Project.Scripts.StateMachines
{
    public class FuncPredicate : IPredicate
    {
        private readonly Func<bool> _func;

        public FuncPredicate(Func<bool> func)
        {
            _func = func;
        }

        public bool Evaluate() => _func.Invoke();
    }
}