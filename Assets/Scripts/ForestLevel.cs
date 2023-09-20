using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;
using Godot.DependencyInjection.Attributes;
using System;

namespace d_sidescroller.Assets.Scripts
{
	public partial class ForestLevel : Node2D
	{
		private IMediatorLoader _mediator;
		private Area2D _LeftBound { get; set; }
		private Area2D _RightBound { get; set; }
		private Player _Player { get; set; }

		public void Init(IMediatorLoader mediator)
		{
			_mediator = mediator;
		}

		[Inject]
		public void Inject(Player player)
		{
			_Player = player;
		}

		public override void _Ready()
		{
			_LeftBound = (Area2D)FindChild("LeftBoundTrigger");
			_RightBound = (Area2D)FindChild("RightBoundTrigger");
			_LeftBound.BodyEntered += _LeftBound_BodyEntered;
			_RightBound.BodyEntered += _RightBound_BodyEntered;
		}

		private void _RightBound_BodyEntered(Node2D body)
		{
			if (body.GetInstanceId == _Player.GetInstanceId)
			{
				_mediator.LevelChangeNotify(this, "right");
			}
		}

		private void _LeftBound_BodyEntered(Node2D body)
		{
			if (body.GetInstanceId == _Player.GetInstanceId)
			{
				_mediator.LevelChangeNotify(this, "left");
			}
		}
	}
}
