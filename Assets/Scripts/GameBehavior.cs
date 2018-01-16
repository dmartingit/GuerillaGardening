using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {

	public int seeds;
	public float waveStartDelay;
	public List<Plant> plants;
	public List<Wave> waves;
	public List<Banana> bananas;

	private int selectedPlant;
	private float nextWaveTime;
	private float gorillaSpawnTimer;
	private UITooltipHelper tooltipHelper;
	
	void Start () {
		this.tooltipHelper = this.gameObject.AddComponent (typeof(UITooltipHelper)) as UITooltipHelper;

		GameStats.state = GameStats.GameState.InGame;
		GameStats.seeds = this.seeds;
		GameStats.wave = -1;
		GameStats.plantList = this.plants;
		GameStats.waveList = this.waves;
		GameStats.bananaList = this.bananas;
		this.nextWaveTime = this.waveStartDelay;
		this.gorillaSpawnTimer = this.waveStartDelay;
	}

	void Update () {
		this.UpdateSelectedPlant ();
		this.UpdateWaves ();
		this.UpdateGorillaSpawner ();
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

	void UpdateWaves() {
		if (this.waves.Count == 0) {
			return;
		}

		if (GameStats.wave == this.waves.Count) {
			var gorillas = GameObject.FindGameObjectsWithTag ("Gorilla");
			if (gorillas.Length == 0) {
				GameStats.state = GameStats.GameState.LevelComplete;
			}
			return;
		}

		this.nextWaveTime -= Time.deltaTime;
		if (this.nextWaveTime < 0) {
			// Next Wave
			++GameStats.wave;

			// Do not do shit if last wave is done
			if (GameStats.wave == this.waves.Count) {
				var gorillas = GameObject.FindGameObjectsWithTag ("Gorilla");
				if (gorillas.Length == 0) {
					GameStats.state = GameStats.GameState.LevelComplete;
				}
				return;
			}

			// Show GUI
			this.tooltipHelper.SetText ("Wave " + (GameStats.wave + 1).ToString());
			this.tooltipHelper.Draw();

			// Reset Timer
			this.nextWaveTime = this.waves[GameStats.wave].time;
		}
	}

	void UpdateGorillaSpawner() {
		if (GameStats.waveList == null || GameStats.waveList.Count == 0) {
			return;
		}

		if (GameStats.wave == GameStats.waveList.Count) {
			return;
		}

		this.gorillaSpawnTimer -= Time.deltaTime;
		if (this.gorillaSpawnTimer < 0) {
			var gorillaSpawnList = GameObject.FindGameObjectsWithTag ("GorillaSpawn");
			if (gorillaSpawnList.Length == 0) {
				return;
			}

			var go = gorillaSpawnList [Random.Range (0, gorillaSpawnList.Length)];
			var gorillaSpawnScript = go.GetComponent (typeof(GorillaSpawnBehavior)) as GorillaSpawnBehavior;

			gorillaSpawnScript.Spawn ();

			var currentWave = GameStats.waveList [GameStats.wave];
			this.gorillaSpawnTimer = Random.Range (currentWave.spawnRateMin, currentWave.spawnRateMax);
		}
	}
}
