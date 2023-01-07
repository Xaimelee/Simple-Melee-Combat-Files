using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasAttackDecision", fileName = "new HasAttackDecision")]
    public class HasAttackDecision : ScriptableCondition
    {
        public override bool Verify(StateComponent statesComponent)
        {
            AIController aiController = statesComponent.GetComponent<AIController>();
            return aiController.canAttack;
        }
    }
}

