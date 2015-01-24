using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Map {
	
	private Direction[,] directions;
	private	int[,] blockLevels;
	private int _width, _height;
	
	public enum Direction: int {
		Missing = 0,
		Default = 1,
		ToUp = 2,
		ToDown = 3
	}
	
	public struct Item {
		public Direction direction;
		public float blockLevel;
		
		public Item(Direction direction, float blockLevel) {
			this.direction = direction;
			this.blockLevel = blockLevel;
		}
		
		public Item(Direction direction) {
			this.direction = direction;
			this.blockLevel = 0;
		}	
	}
	
	public Map (int width, int height) {
		this.directions = new Direction[width, height];
		this.blockLevels = new int[width, height];
		this._width = width;
		this._height = height;
	}

	public int width {
		get {
			return this._width;
		}
	}
	
	public int height {
		get {
			return this._height;
		}
	}

	public void generateMap() {
		for (int x = 0; x < this.width; x++) {
			this.generateColumn(x);
		}
	}

	protected Direction GetRandomDirection(params Direction[] PossibleValues) {
		return PossibleValues[Random.Range(0, PossibleValues.Length)];
	}
	
	public void generateColumn (int x)
	{
		if (x == 0) {
			this.directions [0, Mathf.FloorToInt(this.height / 2.0f)] = Direction.Default;
		} else {
			int t = 0;
			for (int y = 0; y < this.height; y++) {
				switch (this.directions [x - 1, y]) {
					case Direction.Default:
						this.directions [x, y] = this.GetRandomDirection(
							Direction.Default,
							Direction.Missing,
							Direction.ToDown,
							Direction.ToUp
						);
						break;
					case Direction.Missing:
						t = x - 2;
						if (t < 0 || this.directions [t, y] == Direction.Missing) continue;
						this.directions [x, y] = Direction.Default;
						break;
					case Direction.ToUp:
						t = y - 1;
						if (t < 0) continue;
						this.directions [x, t] = this.GetRandomDirection(
							Direction.Default,
							Direction.Missing,
							Direction.ToUp
						);
						break;
					case Direction.ToDown:
						t = y + 1;
						if (t >= this.height) continue;
						this.directions [x, t] = this.GetRandomDirection(
							Direction.Default,
							Direction.Missing,
							Direction.ToDown
						);
						break;
				}
			}
		}
	}

	public GameObject CreateGameObjectFor(int x, int y) {
		if (this.directions [x, y] == Direction.Missing)
			return null;
		GameObject ret = GameObject.CreatePrimitive(PrimitiveType.Cube);
		ret.AddComponent<Animation>();
		ret.AddComponent<MeshFilter>();
		ret.AddComponent<MeshCollider>();
		ret.AddComponent<MeshRenderer>();
		ret.transform.localScale = new Vector3(2.0f, 0.1f, 1.0f);
		ret.transform.localPosition = new Vector3 (0.0f, (float)y, 0.0f);
		switch (this.directions[x, y]) {
			case Direction.Default:
				// do nothing
			break;
			case Direction.ToDown:
				ret.transform.Rotate(new Vector3(0, 0, 30));
			break;
			case Direction.ToUp:
				ret.transform.Rotate(new Vector3(0, 0, -30));
			break;
		}
		return ret;
	}

	public override string ToString ()
	{
		string ret = "";
		
		for (int y = 0; y < this.height; y++) {
			for (int x = 0; x < this.width; x++) {
				switch (this.directions[x, y]) {
					case Direction.Default:
						ret += "_";
						break;
					case Direction.Missing:
						ret += " ";
						break;
					case Direction.ToDown:
						ret += "\\";
						break;
					case Direction.ToUp:
						ret += "/";
						break;
					}
			}
			ret += "\n";
		}
		return ret.Substring(0, ret.Length-1);
	}

}