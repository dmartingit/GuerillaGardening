﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehavior : MonoBehaviour {

	public int seeds;
	public float waveStartDelay;
	public List<Plant> plants;
	public List<Wave> waves;
	public List<Banana> bananas;

	private bool waitToNextWave;
	private float nextWaveTime;
	private float gorillaSpawnTimer;
	
	void Start () {
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
		this.UpdateTimeScale ();
		this.UpdateWaves ();
		this.UpdateGorillaSpawner ();
	}

	void UpdateTimeScale() {
		switch (GameStats.state) {
		case GameStats.GameState.MainMenu:
			Time.timeScale = 1;
			break;
		case GameStats.GameState.InGame:
			Time.timeScale = 1;
			break;
		case GameStats.GameState.Pause:
			Time.timeScale = 0;
			break;
		case GameStats.GameState.GameOver:
			Time.timeScale = 0;
			break;
		case GameStats.GameState.LevelComplete:
			Time.timeScale = 0;
			break;
		default:
			break;
		}
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

		if (this.waitToNextWave) {
			var gorillas = GameObject.FindGameObjectsWithTag ("Gorilla");
			if (gorillas.Length == 0) {
				// set wait flag
				this.waitToNextWave = false;

				// Next Wave
				++GameStats.wave;

				// Do not do shit if last wave is done
				if (GameStats.wave == this.waves.Count) {
					return;
				}

				// Reset Timer
				this.nextWaveTime = this.waves[GameStats.wave].time;
			}
			return;
		}

		this.nextWaveTime -= Time.deltaTime;
		if (this.nextWaveTime < 0) {
			// Set wait flag
			this.waitToNextWave = true;
		}
	}

	void UpdateGorillaSpawner() {
		if (GameStats.waveList == null || GameStats.waveList.Count == 0) {
			return;
		}

		if (GameStats.wave == GameStats.waveList.Count) {
			return;
		}

		if (this.waitToNextWave) {
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
