using UnityEngine;

public class Player: MonoBehaviour
{
		public GameObject model;
		public float maxSpeed;
		public int maxPowerups;
		public int maxMapX;
		public Vector3 eyesCamPos;

		private float _speed = 0.0f;
		private int _powerups = 0;
		private int _mapPosX = 0;
		private int _round = 1;

		public bool isRunning = false;

		public delegate void TileChangedHandler(Player player);

		public event TileChangedHandler TileChanged;

		public Player() {
			
		}

		public Player(GameObject model, float maxSpeed, int maxPowerups, int maxMapX, Vector3 eyesCamPos, Vector3 pos) {
			this.model = model;
			this.maxMapX = maxMapX;
			this.maxPowerups = maxPowerups;
			this.maxSpeed = maxSpeed;
			this.isRunning = true;
			this.eyesCamPos = eyesCamPos;
			this.transform.position = pos;			
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

		public float round {
			get {
				return this._round;
			}
		}

		public float x {
			get {
				return this.transform.position.x;
			}
		}

		public int mapPosX {
			get {
				return this._mapPosX;
			}
		}

		public void Start() {
			GameObject t = Instantiate(this.model, this.transform.position, Quaternion.identity) as GameObject;			
			t.transform.parent = this.transform;
			this.isRunning = true;
		}

		public void Update() {
			if (isRunning) {
				if (this._speed < this.maxSpeed) {
					this._speed = Mathf.Clamp (this._speed + this.maxSpeed / this.maxMapX, 0.0f, this.maxSpeed);
				}				
				if (this._powerups < this.maxPowerups) {
					this._powerups = Mathf.Clamp (this._powerups + ((System.DateTime.UtcNow.Second % 3 == 0)?1:0), 0, this.maxPowerups);
				}
			} else {
				if (this._speed > 0.0f) {
					this._speed = Mathf.Clamp (this._speed - this.maxSpeed / this.maxMapX, 0.0f, this.maxSpeed);
				}
			}
			if (this._speed > 0.0f) {
				this.transform.position += new Vector3 (this._speed, 0.0f, 0.0f);
				int tmpMapPosX = Mathf.FloorToInt(this.transform.position.x);
				if (tmpMapPosX > this.maxMapX) {
					this._round = Mathf.FloorToInt(tmpMapPosX / this.maxMapX);
					tmpMapPosX = tmpMapPosX - this._round * this.maxMapX;
				}
				if (tmpMapPosX != this._mapPosX) {
					this._mapPosX = tmpMapPosX;
					this.TileChanged(this);
				}
			}
		}

}