using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	public bool IsInvincible { get; private set; }

	public float Hp
	{
		get { return _hp; }
		set
		{
			_hp = value;
			this.Modulate = new Color(1, 1, 1, 0.5f);
			IsInvincible = true;
			this.CollisionMask = _defaultCollisionMask;
			this.CollisionLayer = _defaultCollisionLayer;
			_invincibilityTimer.Start(_invincibilityTime);
		}
	}

	[Export]
	private float _hp = 100f;
	private Timer _invincibilityTimer;
	private float _invincibilityTime = 2f;
	private const uint _defaultCollisionLayer = 0b00000000_00000000_00000000_00000101;
	private const uint _defaultCollisionMask = 0b00000000_00000000_00000000_00000101;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		IsInvincible = false;
		_invincibilityTimer = (Timer)FindChild("InvincibilityTimer");
		_invincibilityTimer.Timeout += InvincibilityEnd;
		_invincibilityTimer.OneShot = true;
	}

	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			//_playerSprite.Scale = new Vector2(x: Math.Abs(_playerSprite.Scale[0]) * direction[0], y: _playerSprite.Scale.Y);
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void InvincibilityEnd()
	{
		this.Modulate = new Color(1, 1, 1, 1);
		this.CollisionMask = 0b00000000_00000000_00000000_00000111;
		this.CollisionLayer = 0b00000000_00000000_00000000_00000111;
		IsInvincible = false;
	}
}
