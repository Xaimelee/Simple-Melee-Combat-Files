using System;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;
using A.Managers;
using Sirenix.OdinInspector;

namespace A.StateMachines
{
    public class StateComponent : MonoBehaviour
    {
        [SerializeField] private ScriptableStateMachine _stateMachine;
		[ReadOnly]
        [SerializeField] private ScriptableState _currentState;

        private Dictionary<Type, Component> _cachedComponents = new();

        public ScriptableState CurrentState { get => _currentState; }
        /// <summary>
        /// T1: Previous State, T2: Current State
        /// </summary>
        public delegate void OnStateChanged(ScriptableState previousState, ScriptableState nextState);
        public event OnStateChanged onStateChanged;

        private void Awake()
        {
            _cachedComponents = new Dictionary<Type, Component>();
        }

        private void Start()
        {
            if (!_stateMachine.InitialState)
            {
                Debug.LogError($"<b><color=white>{_stateMachine.name}</color></b> has no initial state attached to it, the state machine can't be initialized.", this);
                return;
            }

            _currentState = _stateMachine.InitialState;
            _currentState.Begin(this);
            onStateChanged?.Invoke(_currentState, _currentState);
        }

        private void FixedUpdate()
        {
            if (!_currentState)
                return;

            _currentState.UpdatePhysics(this);
        }

        private void Update()
        {
            if (!_currentState)
                return;

            _currentState.UpdateState(this);
        }

        private void LateUpdate()
        {
            if (!_currentState)
                return;

            CheckTransitions();
        }

        public void CheckTransitions()
        {
            ScriptableState nextState = _stateMachine.CheckTransitions(this, _currentState);
            if (nextState != _stateMachine.EmptyState)
            {
                _currentState.End(this);
                ScriptableState previousState = CurrentState;
                _currentState = nextState;
                _currentState.Begin(this);

                onStateChanged?.Invoke(previousState, nextState);
            }
        }

        public new T GetComponent<T>() where T : Component
        {
            if (_cachedComponents.ContainsKey(typeof(T))) return _cachedComponents[typeof(T)] as T;

            var component = base.GetComponent<T>();
            if (component != null)
            {
                _cachedComponents.Add(typeof(T), component);
            }
            return component;
        }

    }
}