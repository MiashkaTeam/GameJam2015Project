using UnityEngine;

public class Player
{
		public readonly GameObject model;
		public readonly float maxSpeed;
		public readonly int maxPowerups;
		public readonly int maxMapX;		

		private float _speed = 0.0f;
		private int _powerups = 0;
		private float _x = 0.0f;

		public bool isRunning = false;

		public Player(GameObject model, float maxSpeed, int maxPowerups, int maxMapX) {
			this.model = model;
			this.maxMapX = maxMapX;
			this.maxPowerups = maxPowerups;
			this.maxSpeed = maxSpeed;
			this.isRunning = true;
		}

		public int powerups {
			get {
				return this._powerups;
			}
		}

		public float speed {
			get {
				return this._speed;
			}
		}

		public float x {
			get {
				return this._x;
			}
		}

		public int mapPosX {
			get {
				int ret = Mathf.FloorToInt(this._x);
				if (ret > this.maxMapX) {
					ret = this.maxMapX % ret;
				}
				return ret;
			}
		}

		public void Update() {
			if (isRunning) {
				if (this._speed < this.maxSpeed) {
					this._speed = Mathf.Clamp (this._speed + this.maxSpeed / this.maxMapX, 0.0f, this.maxSpeed);
				}			
				this._x += this._speed;
				if (this._powerups < this.maxPowerups) {
					this._powerups = Mathf.Clamp (this._powerups + ((System.DateTime.UtcNow.Second % 3 == 0)?1:0), 0, this.maxPowerups);
				}
			} else {
				if (this._speed > 0.0f) {
					this._speed = Mathf.Clamp (this._speed - this.maxSpeed / this.maxMapX, 0.0f, this.maxSpeed);
				}
			}
		}

}