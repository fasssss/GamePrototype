using d_sidescroller.Assets.Scripts;
using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;
using Godot.DependencyInjection.Attributes;
using System;
using System.Threading;

public partial class Main : Node, IMediatorLoader
{
	private Player _Player { get; set; }
	private Transition _Transition { get; set; }
	private CanvasLayer _CanvasLayer { get; set; }

	[Inject]
	public void Inject(Player player, Transition transition)
	{
		_Player = player;
		_Transition = transition;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_Player.Position = new Vector2(x: -50, y: -150);
		AddChild(_Player);
		LevelLoader levelLoader = new ForestLoader(this);
		AddChild(levelLoader.LoadLevel());
		_CanvasLayer = (CanvasLayer)FindChild("CanvasLayer");
		_CanvasLayer.AddChild(_Transition);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void LevelChangeNotify(Node2D levelCurrent, string exitPosition)
	{
		LevelLoader levelLoader;

		_Transition.FadeOut();
		if (levelCurrent.GetType() == typeof(ForestLevel))
		{
			if (exitPosition == "left") 
			{
				levelLoader = new CaveLoader(this);
				levelCurrent.CallDeferred("free");
				CallDeferred("add_child", levelLoader.LoadLevel());
				_Player.Position = new Vector2(x: 1270, y: -150);
			}
		}

		if (levelCurrent.GetType() == typeof(CaveLevel))
		{
			if (exitPosition == "right")
			{
				levelLoader = new ForestLoader(this);
				levelCurrent.CallDeferred("free");
				CallDeferred("add_child", levelLoader.LoadLevel());
				_Player.Position = new Vector2(x: -2670, y: -150);
			}
		}
		_Transition.FadeIn();
	}
}
