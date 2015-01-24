using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	const int rows = 5;
	const int columns = 20;

	public Map map = new Map(columns, rows);
	private List<GameObject[]> renderedPaths = new List<GameObject[]>(5);
	private int pos = 0;

	private void renderPathRowFor(int x) {
		if (x < 0) {
			x = columns + x;
		}
		if (x >= columns) {
			x = x % columns;
		}
		GameObject[] data = this.map.CreateGameObjectFor(x);
		for (int y = 0; y < data.Length; y++) {
			if (data[y] == null) continue;
			data[y].transform.position = new Vector3(x, y * 3.0f, 0);
		}
		this.renderedPaths.Add(data);
	}

	public void MoveToNextMapTile() {
		if (this.renderedPaths.Count > 0) {
			for (int y = 0; y < this.renderedPaths[0].Length; y++) {
				Destroy(this.renderedPaths[0][y], 0.1f);
			}
			this.renderedPaths.RemoveAt(0);
		} else {
			this.renderPathRowFor(this.pos);
			this.renderPathRowFor(++this.pos);
		}
		this.renderPathRowFor(++this.pos);
	}

	// Use this for initialization
	void Start () {
		this.map.generateMap();
		this.MoveToNextMapTile();
		Camera.main.transform.position = new Vector3 (this.map.StartX, this.map.StartY * 3.0f + 0.4f, 0.0f);
		//Debug.Log (this.map.ToString ());
		//System.IO.File.WriteAllText ("Z:\\map.txt", this.map.ToString ());
		//Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
	
	}

}
