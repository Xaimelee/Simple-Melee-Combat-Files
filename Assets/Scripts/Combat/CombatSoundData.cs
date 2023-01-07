using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace A.Combat
{
	[System.Serializable]
	public struct CombatStateSound
	{
		public AudioClip[] audioClip;
		[ValueDropdown("States")]
		public string combatState;

#if UNITY_EDITOR
		private static readonly string[] States = new string[]
		{
			"Idle",
			"AttackHeld",
			"Attacked",
			"Parried",
			"Stunned",
			"Hit"
		};
#endif
	}

	[System.Serializable]
	public struct CombatResultSound
	{
		public AudioClip[] audioClip;
		[EnumToggleButtons]
		public CombatResult combatResult;
		public bool isAttacker;
	}
}
