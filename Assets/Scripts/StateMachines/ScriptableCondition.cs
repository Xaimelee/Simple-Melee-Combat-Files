using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.StateMachines
{
    public abstract class ScriptableCondition : ScriptableObject
    {
        public abstract bool Verify(StateComponent statesComponent);
    }
}