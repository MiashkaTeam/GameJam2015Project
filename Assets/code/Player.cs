using UnityEngine;

public class Player
{
		public float speed = 0.0f;
		public int powerups = 1;

		private void tmrPowerUps_Elapsed () {
			this.powerups++;
		}

}