using UnityEngine;

namespace A.Managers
{
	public class InputManager : MonoBehaviour
	{
		public static InputManager Instance { get; private set; }

		private Controls _controls;

		private void OnEnable()
		{
			_controls.Enable();
		}

		private void OnDisable()
		{
			_controls.Disable();
		}

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}

			_controls = new Controls();
		}

		public Vector2 GetPlayerMovement()
		{
			return _controls.Player.Movement.ReadValue<Vector2>();
		}

		public Vector2 GetPlayerCamera()
		{
			return _controls.Player.Camera.ReadValue<Vector2>();
		}

		public bool HasPlayerAttacked()
		{
			return _controls.Player.Attack.triggered;
		}

		public bool HasPlayedParried()
		{
			return _controls.Player.Parry.triggered;
		}
	}
}
