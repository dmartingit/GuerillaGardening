using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehavior : MonoBehaviour {

	public List<Plant> plants;
	public List<Wave> waves;
	public List<Banana> bananas;
	public float waveStartDelay;
	public float nextWaveDelay;
	private float timer;

	void Start () {
		/*if (waves.Count == 0) {
			return;
		}*/

		GameStats.currentWave = 0;
		GameStats.waves = waves.Count;
		//timer = waves[0].time;

		GameStats.plantList = plants;
		GameStats.bananaList = bananas;
	}

	void Update () {
		if (waves.Count == 0) {
			return;
		}

		if (GameStats.currentWave == GameStats.waves) {
			return;
		}

		timer -= Time.deltaTime;
		if (timer < 0) {
			// Next Wave
			++GameStats.currentWave;

			// Do not do shit if last wave is done
			if (GameStats.currentWave == GameStats.waves) {
				return;
			}

			// Update Enemies
			GameStats.gorillaList.Clear ();
			GameStats.gorillaList = waves[GameStats.currentWave].gorillas;

			// Reset Timer
			timer = waves[GameStats.currentWave].time;
		}
	}
}
