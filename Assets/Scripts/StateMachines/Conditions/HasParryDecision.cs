using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasParryDecision", fileName = "new HasParryDecision")]
    public class HasParryDecision : ScriptableCondition
    {
        public override bool Verify(StateComponent statesComponent)
        {
            AIController aiController = statesComponent.GetComponent<AIController>();
            return aiController.canParry;
        }
    }
}
