using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Services.Network.SimplePacket;


namespace BossReigns.Net {
	class ReignBuildupPauseProtocol : SimplePacketPayload {
		public static void SendToAllClients( bool isPaused ) {
			if( Main.netMode != NetmodeID.Server ) { throw new ModLibsException( "Not a server." ); }

			var protocol = new ReignBuildupPauseProtocol( isPaused );

			SimplePacket.SendToClient( protocol, -1, -1 );
		}



		////////////////

		public bool IsPaused;



		////////////////

		private ReignBuildupPauseProtocol() { }

		private ReignBuildupPauseProtocol( bool isPaused ) {
			this.IsPaused = isPaused;
		}


		////////////////

		public override void ReceiveOnServer( int fromWho ) {
			throw new ModLibsException( "Not implemented." );
		}

		public override void ReceiveOnClient() {
			var myworld = ModContent.GetInstance<BossReignsWorld>();

			myworld.IsPaused = this.IsPaused;
		}
	}
}
