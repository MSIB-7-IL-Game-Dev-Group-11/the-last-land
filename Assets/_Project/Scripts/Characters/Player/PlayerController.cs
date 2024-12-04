using TheLastLand._Project.Scripts.Characters.Player;
using TheLastLand._Project.Scripts.Characters.Player.Datas;
using TheLastLand._Project.Scripts.GameSystems.Interactor.Common;
using TheLastLand._Project.Scripts.Input;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController
{
    public readonly PlayerData Data;

    private const float ZeroF = 0f;

    private InputReader PlayerInput { get; }
    private CinemachineVirtualCamera VirtualCamera { get; }
    private readonly PlayerMediator _playerMediator;

    private PlayerStateData StateData { get; }
    private IInteractable _interactable;

    // Components
    private readonly SpriteRenderer _characterSprite;
    private readonly Rigidbody2D _rigidbody;
    private readonly GroundCheck _groundCheck;

    // Timer
    private readonly PlayerTimerConfigurator _timerConfigurator;

    public PlayerController(PlayerData data, PlayerMediator playerMediator,
        PlayerComponent components, PlayerStateData stateData,
        PlayerTimerConfigurator timerConfigurator)
    {
        Data = data;
        StateData = stateData;

        _playerMediator = playerMediator;

        PlayerInput = components.PlayerInput;
        VirtualCamera = components.VirtualCamera;
        _characterSprite = components.CharacterSprite;
        _rigidbody = components.Rigidbody;
        _groundCheck = components.GroundCheck;
        _timerConfigurator = timerConfigurator;

        InitializeCamera();
    }

    public void RegisterEvents()
    {
        PlayerInput.Jump += OnJump;
        PlayerInput.Move += OnMove;
        PlayerInput.Dash += OnDash;
        PlayerInput.Interact += OnInteract;
    }

    public void DeregisterEvents()
    {
        PlayerInput.Jump -= OnJump;
        PlayerInput.Move -= OnMove;
        PlayerInput.Dash -= OnDash;
        PlayerInput.Interact -= OnInteract;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        StateData.MovementDirection = context.ReadValue<Vector2>();
        StateData.IsWalking = context.performed;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started when _groundCheck.IsTouching
                                               && !_timerConfigurator.JumpTimer.IsRunning
                                               && !_timerConfigurator.JumpCooldownTimer.IsRunning
                                               || _timerConfigurator.JumpCoyoteTimer.IsRunning:
            {
                if (!_playerMediator.HasSufficientStamina(Data.Jump.StaminaCost)) return;

                _playerMediator.UseStamina(Data.Jump.StaminaCost);
                _timerConfigurator.JumpCoyoteTimer.Stop();

                _timerConfigurator.JumpTimer.Reset(Data.Jump.Duration);
                _timerConfigurator.JumpTimer.Start();

                break;
            }
            case InputActionPhase.Canceled when _timerConfigurator.JumpTimer.IsRunning:
            {
                _timerConfigurator.JumpTimer.Stop();
                break;
            }
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (!context.started
            || _timerConfigurator.DashTimer.IsRunning
            || _timerConfigurator.DashCooldownTimer.IsRunning) return;
        if (!_playerMediator.HasSufficientStamina(Data.Dash.StaminaCost)) return;

        _playerMediator.UseStamina(Data.Dash.StaminaCost);

        _timerConfigurator.DashTimer.Reset(Data.Dash.Duration);
        _timerConfigurator.DashTimer.Start();
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.started || _interactable == null) return;
        _interactable.Interact();
    }

    private void InitializeCamera()
    {
        VirtualCamera.Follow = _rigidbody.gameObject.transform;
        VirtualCamera.LookAt = _rigidbody.gameObject.transform;
    }

    public void Jump()
    {
        if (!_timerConfigurator.JumpTimer.IsRunning) return;

        _rigidbody.AddForce(StateData.CurrentJumpVelocity * Vector2.up, ForceMode2D.Force);
    }

    public void Move(float speedModifier)
    {
        var targetSpeed = StateData.MovementDirection.x
                          * (_timerConfigurator.DashTimer.IsRunning
                              ? Data.Dash.Force
                              : Data.BaseSpeed);

        var speedDifference = targetSpeed - _rigidbody.velocity.x;

        var accelerationRate = Mathf.Abs(targetSpeed) > 0.01f
            ? Data.Acceleration
            : Data.Deceleration;

        var mode = _timerConfigurator.DashTimer.IsRunning ? ForceMode2D.Impulse : ForceMode2D.Force;

        StateData.CurrentMoveVelocity = Mathf.Pow(
                                            Mathf.Abs(speedDifference) * accelerationRate,
                                            speedModifier
                                        )
                                        * Mathf.Sign(speedDifference);

        _rigidbody.AddForce(StateData.CurrentMoveVelocity * Vector2.right, mode);
    }

    public void FlipCharacterSprite()
    {
        _characterSprite.flipX = StateData.MovementDirection.x switch
        {
            > ZeroF => false,
            < ZeroF => true,
            _ => _characterSprite.flipX
        };
    }

    public void HandleInteraction(Collider2D other, bool isEntering)
    {
        if (!isEntering)
        {
            _interactable = null;
            return;
        }

        _interactable = other.GetComponent<IInteractable>();
    }
}