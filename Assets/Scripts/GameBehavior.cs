using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {
	
	public int seeds;
	public float waveStartDelay;

	private int selectedPlant;
	private float spawnTimer;
	
	void Start () {
		GameStats.seeds = this.seeds;
		this.spawnTimer = waveStartDelay;
	}

	void Update () {
		this.UpdateSelectedPlant ();
		this.UpdateSpawner ();
	}

	void OnGUI() {
		GUILayout.BeginHorizontal ();
		GUILayout.Label("Wave: " + (GameStats.wave + 1).ToString () + " Seeds: " + GameStats.seeds.ToString ());
		for (var i = 0; i < GameStats.plantList.Count; ++i) {
			Plant plant = GameStats.plantList [i];
			if (GUILayout.Toggle (this.selectedPlant == i, plant.name + " (" + plant.cost.ToString() + ")")) {
				this.selectedPlant = i;
			}
		}
		GUILayout.EndHorizontal ();
	}

	void UpdateSelectedPlant() {
		if (GameStats.plantList == null || GameStats.plantList.Count == 0) {
			return;
		}

		GameStats.selectedPlant = GameStats.plantList [this.selectedPlant];
	}

	void UpdateSpawner() {
		if (GameStats.waveList == null || GameStats.waveList.Count == 0) {
			return;
		}

		if (GameStats.wave == GameStats.waveList.Count) {
			return;
		}

		this.spawnTimer -= Time.deltaTime;
		if (this.spawnTimer < 0) {

			var gorillaSpawnList = GameObject.FindGameObjectsWithTag ("GorillaSpawn");
			if (gorillaSpawnList.Length == 0) {
				return;
			}

			var go = gorillaSpawnList [Random.Range (0, gorillaSpawnList.Length)];
			var spawnScript = (GorillaSpawnBehavior)go.GetComponent (typeof(GorillaSpawnBehavior));

			spawnScript.Spawn ();

			var currentWave = GameStats.waveList [GameStats.wave];
			this.spawnTimer = Random.Range (currentWave.spawnRateMin, currentWave.spawnRateMax);
		}
	}
}
