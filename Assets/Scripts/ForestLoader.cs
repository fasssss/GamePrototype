using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sidescroller.Assets.Scripts
{
    public class ForestLoader: LevelLoader
    {
        private IMediatorLoader _mediator;
        public ForestLoader(IMediatorLoader mediator) 
        {
            _mediator = mediator;
        }

        public override Node2D LoadLevel()
        {
            var unpackedLevel = ResourceLoader.Load<PackedScene>("res://Assets/Objects/forest_level.tscn").Instantiate<ForestLevel>();
            unpackedLevel.Init(_mediator);
            return unpackedLevel;
        }
    }
}
