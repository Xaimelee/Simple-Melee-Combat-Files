using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/WasHit", fileName = "new WasHit")]
    public class WasHit : ScriptableCondition
    {
        public override bool Verify(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            return combatController.GetWasHit();
        }
    }
}
