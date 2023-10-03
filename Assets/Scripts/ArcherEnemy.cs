using d_sidescroller.Assets.Scripts;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ArcherEnemy : Enemy
{
	private Dictionary<Type, PackedScene> _WeaponRes { get; set; }
	private Area2D _VisionArea { get; set; }
	private Area2D _CloseArea { get; set; }
	private ArcherStrategyExecutor _StrategyExecutor { get; set; }

	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Hp = 100f;
		Speed = 200f;
		BaseActionSpeed = 1f;
		_WeaponRes = new Dictionary<Type, PackedScene> {
			{ typeof(Arrow), ResourceLoader.Load<PackedScene>("res://Assets/Objects/arrow.res") } 
		};
		_VisionArea = (Area2D)FindChild("VisionArea");
		_CloseArea = (Area2D)FindChild("CloseArea");
		_StrategyExecutor = new ArcherStrategyExecutor();
		var playerInVision = _VisionArea.GetOverlappingBodies().FirstOrDefault(node => node.GetType() == typeof(Player));

		if (playerInVision != null)
		{
			IsAgressive = true;
			_StrategyExecutor.SetStrategy(new ArcherAttackStrategy((Player)playerInVision, this));
		}

		_VisionArea.BodyEntered += _VisionArea_BodyEntered;
		_VisionArea.BodyExited += _VisionArea_BodyExited;
		_CloseArea.BodyEntered += _CloseArea_BodyEntered;
		_CloseArea.BodyExited += _CloseArea_BodyExited;

	}

	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;

		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}

		Velocity = velocity;

		if (IsAgressive)
		{
			_StrategyExecutor.Execute();
		}

		MoveAndSlide();

	}

	private void _VisionArea_BodyEntered(Node2D body)
	{
		IsAgressive = true;
		if(body.GetType() == typeof(Player))
		{
			GD.PrintRich(IsAgressive.ToString());
			_StrategyExecutor.SetStrategy(new ArcherAttackStrategy((Player)body, this));
		}

	}

	private void _VisionArea_BodyExited(Node2D body)
	{
		if (body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new ArcherGettingCloserStrategy((Player)body, this));
	}

	private void _CloseArea_BodyExited(Node2D body)
	{
		if (body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new ArcherAttackStrategy((Player)body, this));
	}

	private void _CloseArea_BodyEntered(Node2D body)
	{
		if (body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new ArcherGettingAwayStrategy((Player)body, this));
	}

	public static ArcherEnemy CreateEnemy(float x, float y)
	{
		var enemy = ResourceLoader.Load<PackedScene>("res://Assets/Objects/archer_enemy.res").Instantiate<ArcherEnemy>();
		enemy.Position = new Vector2(x: x, y: y);
		return enemy;
	}
}
