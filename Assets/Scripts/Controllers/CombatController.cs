using A.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using A.Combat;
using A.Global;

namespace A.Controllers
{
	[RequireComponent(typeof(StateComponent))]
	public class CombatController : MonoBehaviour
	{
		public CombatEntityData combatEntityData = null;
		public CombatWeaponData combatWeaponData = null;
		public CombatArmourData combatArmourData = null;

		public EntityAnimatorController EntityAnimatorController { get => _entityAnimatorController; }
		public EntityAudioController EntityAudioController { get => _entityAudioController; }
		public CombatData LastCombatData { get => _lastCombatData; }
		public GlobalFactions Faction { get => combatEntityData.Faction; }
		public string CurrentState { get => _currentState; }
		public float StunTimer
		{
			set
			{
				value = Mathf.Clamp(value, 0f, 1f);
				_currentStunTimer = value;
			}
			get
			{
				return _currentStunTimer;
			}
		}
		public int CurrentHitPoints { get => _currentHitPoints; }

		[SerializeField] private EntityAnimatorController _entityAnimatorController = null;
		[SerializeField] private EntityAudioController _entityAudioController = null;
		[ReadOnly]
		[SerializeField] private string _currentState = "Null";
		[ReadOnly]
		[SerializeField] private int _currentHitPoints = 0;
		[ReadOnly]
		[SerializeField] private float _currentStunTimer = 0.4f;

		private StateComponent _stateComponent = null;
		private CombatData _lastCombatData = null;
		private int _startingHitPoints = 0;

		private void Awake()
		{
			_currentHitPoints = CombatHelpers.RollHitPoints(
				combatEntityData.hitDice, 
				combatEntityData.hitPointModifier,
				combatEntityData.startingLevel
			);
			_startingHitPoints = _currentHitPoints;
			_stateComponent = GetComponent<StateComponent>();
		}

		private void OnEnable()
		{
			_stateComponent.onStateChanged += SetCurrentState;
		}

		private void OnDisable()
		{
			_stateComponent.onStateChanged -= SetCurrentState;
		}

		private void SetCurrentState(ScriptableState previousState, ScriptableState nextState)
		{
			_currentState = nextState.name;
			if (CurrentState == "Idle")
			{
				_lastCombatData = null;
				return;
			}

			foreach (CombatStateSound combatStateSound in combatWeaponData.combatStateSounds)
			{
				if (combatStateSound.combatState == CurrentState)
				{
					_entityAudioController.Play(combatStateSound.audioClip);
				}
			}
			foreach (CombatStateSound combatStateSound in combatArmourData.combatStateSounds)
			{
				if (combatStateSound.combatState == CurrentState)
				{
					_entityAudioController.Play(combatStateSound.audioClip);
				}
			}
			foreach (CombatStateSound combatStateSound in combatEntityData.combatStateSounds)
			{
				if (combatStateSound.combatState == CurrentState)
				{
					_entityAudioController.Play(combatStateSound.audioClip);
				}
			}
			if (_lastCombatData == null) return;

			if (CurrentState == "Hit")
			{
				TakeDamage(
					_lastCombatData.attacker.combatWeaponData.damageDice,
					_lastCombatData.attacker.combatWeaponData.damageAdditiveModifier
				);
			}
		}

		public void SetCombatData(CombatData newCombatData)
		{
			_lastCombatData = newCombatData;
			if (_lastCombatData == null) return;

			foreach (CombatResultSound combatResultSound in combatWeaponData.combatResultSounds)
			{
				if (combatResultSound.combatResult == _lastCombatData.combatResult &&
					GetWasAttacker() == combatResultSound.isAttacker)
				{
					_entityAudioController.Play(combatResultSound.audioClip);
				}
			}
			foreach (CombatResultSound combatResultSound in combatArmourData.combatResultSounds)
			{
				if (combatResultSound.combatResult == _lastCombatData.combatResult &&
					GetWasAttacker() == combatResultSound.isAttacker)
				{
					_entityAudioController.Play(combatResultSound.audioClip);
				}
			}
			foreach (CombatResultSound combatResultSound in combatEntityData.combatResultSounds)
			{
				if (combatResultSound.combatResult == _lastCombatData.combatResult &&
					GetWasAttacker() == combatResultSound.isAttacker)
				{
					_entityAudioController.Play(combatResultSound.audioClip);
				}
			}
			if (GetWasAttacker() && _lastCombatData.combatResult == CombatResult.Blocked)
			{
				StunTimer = 0.6f + 0.6f * _lastCombatData.defender.combatWeaponData.stunDurationMultiplier;
			}
			else if (GetWasDefender() && _lastCombatData.combatResult == CombatResult.Blocked)
			{
				StunTimer = 0.5f + 0.5f * combatWeaponData.stunDurationMultiplier;
			}
			else if (GetWasHit())
			{
				StunTimer = 0.5f + 0.5f * _lastCombatData.attacker.combatWeaponData.stunDurationMultiplier;
			}
			else if (GetWasAttacker() && _lastCombatData.combatResult == CombatResult.Hit)
			{
				StunTimer = 0.6f + 0.6f * _lastCombatData.attacker.combatWeaponData.stunDurationMultiplier;
			}
			else
			{
				StunTimer = 0.4f;
			}
		}

		public void TakeDamage(CombatDice dice, int modifier)
		{
			float damageRoll = CombatHelpers.RollDamage(dice, modifier);
			float damage = damageRoll;
			if (_lastCombatData.combatRollResult == CombatRollResult.Failed)
			{
				damage += damage * -combatArmourData.armourDamageReduction;
			}
			//: Rounded down when converted, an example; 5% reduction of 6 returns 5 which is an effective reduction of 16%.
			//: Make Health float based to avoid this hidden buff to reduction modifiers.
			_currentHitPoints -= (int)damage;
			if (_currentHitPoints <= 0)
			{
				Debug.Log("Reset Hitpoints");
				_currentHitPoints = _startingHitPoints;
			}
		}

		public bool GetWasHit()
		{
			if (LastCombatData == null) return false;
			return _lastCombatData.defender == this && _lastCombatData.combatResult == CombatResult.Hit;
		}

		public bool GetWasAttacker()
		{
			if (LastCombatData == null) return false;
			return _lastCombatData.attacker == this;
		}

		public bool GetWasDefender()
		{
			if (LastCombatData == null) return false;
			return _lastCombatData.defender == this;
		}
	}
}
