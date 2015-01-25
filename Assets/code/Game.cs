using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {

	const int rows = 5;
	const int columns = 20;

	public Map map = new Map(columns, rows);
	private List<GameObject[]> renderedPaths = new List<GameObject[]>(5);
	private int pos = 0;

	private List<Player> players = new List<Player>();

	private Timer tmrPowerUps;

	private void tmrPowerUpsElapsed() {
		for (int i = 0; i < this.players.Count; i++) {
			if (this.players[i].powerups == 5)
				continue;
			this.players[i].powerups += 1;
		}
	}

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
		this.players.Add (new Player());
		this.tmrPowerUps = new Timer(3f, true, this.tmrPowerUpsElapsed);
		StartCoroutine (this.tmrPowerUps.Start ());
		Debug.Log ( this.map.ToString ());
		//System.IO.File.WriteAllText ("Z:\\map.txt", this.map.ToString ());
		//Application.Quit ();
	}

	// Update is called once per frame
	void Update () { 
	
	}

	void OnGUI() {
		GUI.color = new Color (200, 200, 200);
		float h = 20.0f;
		GUI.Label (new Rect (10.1f, 10.8f, 90.15f, h), "<size=14>Players</size>");
		GUI.Label (new Rect (70.15f, 10.8f, 90.15f, h), "<size=14>Speed</size>");
		GUI.Label (new Rect (120.1f, 10.8f, 90.15f, h), "<size=14>Power</size>");
		GUI.color = new Color (230, 230, 230);
		for (int i = 0; i < this.players.Count; i++) {
			int n = i + 1;
			GUI.Label (new Rect (10.1f, 10.8f + h * n, 90.15f, h), "<size=12><b>#"+ n.ToString()+"</b></size>");
			GUI.Label (new Rect (70.15f, 10.8f + h * n, 90.15f, h), "<size=12>"+this.players[i].speed.ToString()+"</size>");
			GUI.Label (new Rect (120.1f, 10.8f + h * n, 90.15f, h), "<size=12>"+this.players[i].powerups.ToString()+"</size>");
		}
	}

}
