using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine.Random;

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
	
	public void generateColumn (int x)
	{
		if (x == 0) {
			this.directions [0, 3] = Direction.Default;
		} else {
			Direction r;
			Array values = Enum.GetValues(typeof(Direction));
			for (int y = 0; y < this.height; y++) {
				switch (this.directions [x - 1, y]) {
				case Direction.Default:
					this.directions [x, y] = (Direction)values.GetValue(random.Next(values.Length));
					break;
				case Direction.Missing:
					if ((x - 2) < 1 || this.directions [x - 2, y] == Direction.Missing) {
						continue;
					}						
					this.directions [x, y] = Direction.Default;
					break;
				case Direction.ToDown:
					r = (Direction)values.GetValue(random.Next(values.Length));
					if (r == Direction.ToUp) {
						r = Direction.ToDown;
					} 		
					if (y > 0) {
						this.directions [x, y - 1] = r;
					} 
					break;
				case Direction.ToUp:
					if ((y - 1) >= this.height) {
						this.directions [x, y + 1] = (Direction)values.GetValue(random.Next(values.Length));
					}
					break;
				}
			}
		}
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
		return ret;
	}

}