using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.StateMachines
{
    public abstract class ScriptableAction : ScriptableObject
    {
        public abstract void Act(StateComponent statesComponent);
    }
}