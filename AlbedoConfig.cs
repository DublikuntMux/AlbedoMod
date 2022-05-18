using System.ComponentModel;
using Albedo.Global;
using Terraria.ModLoader.Config;

namespace Albedo
{
	[BackgroundColor(90, 40, 130)]
	public class AlbedoConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		[DefaultValue(true)]
		[Label("Enable on world enter message")]
		[Tooltip("while true - enable, while false - disable")]
		public bool StartMessage { get; set; }

		public override void OnChanged() => AlbedoPlayer.StartMessage = StartMessage;
	}
}