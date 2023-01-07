using UnityEngine;
using A.Managers;

namespace A.Controllers
{
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour
	{
		public InputManager InputManager { get => _inputManager; }

		[SerializeField] private Transform _cameraTarget = null;
		[SerializeField] private Transform _cameraTransform = null;

		private CharacterController _characterController = null;
		private InputManager _inputManager = null;
		private CombatController _combatController = null;

		private readonly float _cameraPitchMin = -15f;
		private readonly float _cameraPitchMax = 25f;
		private readonly float _moveSpeed = 1.5f;
		private readonly float _rotationSpeed = 10f;
		private readonly float _gravity = -16f;

		private Vector3 _verticalVelocity = new();
		private bool _isPlayerGrounded = false;
		private float _rotateDirection = 0f;
		private float _cameraTargetPitch = 0f;

		private void Awake()
		{
			Cursor.lockState = CursorLockMode.Locked;
			_characterController = GetComponent<CharacterController>();
			_combatController = GetComponent<CombatController>();
		}

		private void Start()
		{
			_inputManager = InputManager.Instance;
		}

		private void Update()
		{
			Gravity();
			Move();
		}

		private void LateUpdate()
		{
			Rotate();
		}

		private void Gravity()
		{
			_isPlayerGrounded = _characterController.isGrounded;
			if (!_isPlayerGrounded) return;
			if (_verticalVelocity.y > 0f) return;

			_verticalVelocity.y += _gravity * Time.deltaTime;
		}

		private void Move()
		{
			Vector3 inputDirection = new(_inputManager.GetPlayerMovement().x, 0f, _inputManager.GetPlayerMovement().y);
			_combatController.EntityAnimatorController.Direction = inputDirection;
			inputDirection = _cameraTransform.forward * inputDirection.z + _cameraTransform.right * inputDirection.x;
			inputDirection.y = 0;
			_characterController.Move(inputDirection.normalized * (_moveSpeed * Time.deltaTime) + new Vector3(0f, _verticalVelocity.y, 0f) * Time.deltaTime);
		}

		private void Rotate()
		{   //: Is any input detected.
			if (_inputManager.GetPlayerCamera().sqrMagnitude < 0.01f) return;

			_cameraTargetPitch -= _inputManager.GetPlayerCamera().y * _rotationSpeed * Time.deltaTime;
			_rotateDirection = _inputManager.GetPlayerCamera().x * _rotationSpeed * Time.deltaTime;
			_cameraTargetPitch = ClampAngle(_cameraTargetPitch, _cameraPitchMin, _cameraPitchMax);
			_cameraTarget.localRotation = Quaternion.Euler(_cameraTargetPitch, 0.0f, 0.0f);
			transform.Rotate(Vector3.up * _rotateDirection);
		}

		private float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f) angle += 360f;
			if (angle > 360f) angle -= 360f;
			return Mathf.Clamp(angle, min, max);
		}
	}
}
