using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {

	public int score;
	public int seeds;

	private int selectedPlant;
	
	void Start () {
		GameStats.score = this.score;
		GameStats.seeds = this.seeds;
	}

	void Update () {
		GameStats.selectedPlant = GameStats.plantList [this.selectedPlant];
	}

	void OnGUI() {
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Score: " + GameStats.score.ToString () + " Seeds: " + GameStats.seeds.ToString ());
		for (var i = 0; i < GameStats.plantList.Count; ++i) {
			Plant plant = GameStats.plantList [i];
			if (GUILayout.Toggle (this.selectedPlant == i, plant.name + " (" + plant.cost.ToString() + ")")) {
				this.selectedPlant = i;
			}
		}
		GUILayout.EndHorizontal ();
	}
}
