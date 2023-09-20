using d_sidescroller.Assets.Scripts;
using Godot;
using System;
using System.Linq;

public partial class ArcherEnemy : Enemy
{
	private PackedScene _WeaponRes { get; set; }
	private Area2D _VisionArea { get; set; }
	private Area2D _CloseArea { get; set; }
	private ArcherStrategyExecutor _StrategyExecutor { get; set; }

	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		Hp = 100f;
		Speed = 200f;
		_WeaponRes = ResourceLoader.Load<PackedScene>("res://Assets/Objects/arrow.res");
		_VisionArea = (Area2D)FindChild("VisionArea");
		_CloseArea = (Area2D)FindChild("CloseArea");
		_StrategyExecutor = new ArcherStrategyExecutor();
		if (_VisionArea.GetOverlappingBodies().FirstOrDefault(node => node.GetType() == typeof(Player)) != null)
		{
			IsAgressive = true;
			_StrategyExecutor.SetStrategy(new AttackStrategy());
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

		if(IsAgressive)
		{
			_StrategyExecutor.Execute();
		}

		Velocity = velocity;
		MoveAndSlide();

	}

	public override float Attack<T>(PackedScene weaponRes)
	{
		Weapon weapon = weaponRes.Instantiate<T>();

		if(weapon is Arrow)
		{
			AddChild(weapon);

			///////Add code for throwing body
		}

		return 0;
	}

	private void _VisionArea_BodyEntered(Node2D body)
	{
		if(body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new AttackStrategy());
	}

	private void _VisionArea_BodyExited(Node2D body)
	{
		if (body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new GettingCloserStrategy((Player)body, this));
	}

	private void _CloseArea_BodyExited(Node2D body)
	{
		if (body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new AttackStrategy());
	}

	private void _CloseArea_BodyEntered(Node2D body)
	{
		if (body.GetType() == typeof(Player))
			_StrategyExecutor.SetStrategy(new GettingAwayStrategy((Player)body, this));
	}

	public static ArcherEnemy CreateEnemy(float x, float y)
	{
		var enemy = ResourceLoader.Load<PackedScene>("res://Assets/Objects/archer_enemy.res").Instantiate<ArcherEnemy>();
		enemy.Position = new Vector2(x: x, y: y);
		return enemy;
	}
}
