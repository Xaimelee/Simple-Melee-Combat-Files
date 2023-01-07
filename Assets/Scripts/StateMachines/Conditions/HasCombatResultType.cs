using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using A.Controllers;
using A.Combat;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasCombatResultType", fileName = "new HasCombatResultType")]
    public class HasCombatResultType : ScriptableCondition
    {
        [SerializeField] private bool _wasAttacker = true;
		[EnumToggleButtons]
        [SerializeField] private CombatResult _combatResult = CombatResult.Hit;

        public override bool Verify(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            if (combatController.LastCombatData == null) return false;
            if (combatController.GetWasAttacker() != _wasAttacker) return false;
            return (combatController.LastCombatData.combatResult == _combatResult);
        }
    }
}



