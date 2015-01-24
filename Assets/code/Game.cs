using UnityEngine;

public class Game : MonoBehaviour {

	const int rows = 5;
	const int columns = 20;

	public Map map = new Map(columns, rows);
	public int pos = 0;

	// Use this for initialization
	void Start () {
		this.map.generateMap();
		//Debug.Log (this.map.ToString ());
		System.IO.File.WriteAllText ("Z:\\map.txt", this.map.ToString ());
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
	
	}

}
