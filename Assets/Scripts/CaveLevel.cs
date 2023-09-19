using d_sidescroller.Assets.Scripts.Interfaces;
using Godot.DependencyInjection.Attributes;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sidescroller.Assets.Scripts
{
	public partial class CaveLevel : Node2D
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
			_LeftBound = (Area2D)FindChild("LeftBound");
			_RightBound = (Area2D)FindChild("RightBound");
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
