using UnityEngine;

public class Game : MonoBehaviour {

	const int rows = 5;
	const int columns = 20;

	public Map map = new Map(columns, rows);
	public int pos = 0;

	// Use this for initialization
	void Start () {
		this.map.generateMap();
		GameObject gInstance;
		for (int x = 0; x < this.map.width; x++) {
			for (int y = 0; y < this.map.height; y++) {
				gInstance = this.map.CreateGameObjectFor(x, y);
				if (gInstance == null) continue;
				gInstance.transform.position = new Vector3(x, y * 3.0f, 0);
			}
		}
		//Debug.Log (this.map.ToString ());
		//System.IO.File.WriteAllText ("Z:\\map.txt", this.map.ToString ());
		//Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
	
	}

}
