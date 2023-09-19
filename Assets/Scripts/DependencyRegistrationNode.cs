using Godot;
using Godot.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace d_sidescroller.Assets.Scripts
{
	public partial class DependencyRegistrationNode : Node, IServicesConfigurator
	{
		public void ConfigureServices(IServiceCollection services)
		{
			var Player = ResourceLoader.Load<PackedScene>("res://Assets/Objects/Player.res").Instantiate<Player>();
            var TransitionNode = ResourceLoader.Load<PackedScene>("res://Assets/Objects/transition.tscn").Instantiate<Transition>();
            services.AddGodotServices();
			services.AddSingleton(Player);
            services.AddSingleton(TransitionNode);
		}
	}
}
