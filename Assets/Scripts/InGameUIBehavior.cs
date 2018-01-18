﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIBehavior : MonoBehaviour {

	public GUISkin skin;
	public string mainMenuName;
	public string nextLevelName;

	private UITooltipHelper tooltipHelper;

	private int lastWave = -1;
	private int selectedPlant;
	private Rect pauseMenuRect = new Rect();
	private Rect gameOverMenuRect = new Rect();
	private Rect levelCompleteRect = new Rect();

	void Start () {
		this.tooltipHelper = this.gameObject.AddComponent (typeof(UITooltipHelper)) as UITooltipHelper;
		this.tooltipHelper.skin = this.skin;

		// Show GUI
		this.tooltipHelper.SetText ("Start");
		this.tooltipHelper.Draw();
	}

	void Update () {
		this.UpdateToggles ();
		this.UpdateWave ();
		this.UpdateSelectedPlant ();
		this.UpdateTooltipHelper ();
	}

	void OnGUI() {
		GUI.skin = this.skin;

		var state = GameStats.state;
		if (state == GameStats.GameState.Pause) {
			this.showPauseMenu ();
			return;
		} else if (state == GameStats.GameState.GameOver) {
			this.showGameOverMenu ();
			return;
		} else if (state == GameStats.GameState.LevelComplete) {
			this.showLevelCompleteMenu ();
			return;
		}

		GUILayout.BeginHorizontal ();
		{
			GUILayout.Space (100);
			this.showGameStats ();

			GUILayout.Space (50);
			this.showPlantSelection ();
		}
		GUILayout.EndHorizontal ();
	}

	void UpdateToggles () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (GameStats.state == GameStats.GameState.InGame) {
				GameStats.state = GameStats.GameState.Pause;
			} else if (GameStats.state == GameStats.GameState.Pause) {
				GameStats.state = GameStats.GameState.InGame;
			}
		}
	}

	void UpdateWave() {
		if (this.lastWave == GameStats.wave) {
			return;
		}

		// Wave changed
		++this.lastWave;

		if (GameStats.wave == GameStats.waveList.Count) {
			return;
		}

		// Show GUI
		this.tooltipHelper.SetText ("Wave " + (GameStats.wave + 1).ToString());
		this.tooltipHelper.Draw();
	}

	void UpdateSelectedPlant() {
		if (GameStats.plantList == null || GameStats.plantList.Count == 0) {
			return;
		}

		GameStats.selectedPlant = GameStats.plantList [this.selectedPlant];
	}
		
	void UpdateTooltipHelper() {
		switch (GameStats.state) {
		case GameStats.GameState.MainMenu:
			this.tooltipHelper.visible = false;
			break;
		case GameStats.GameState.InGame:
			this.tooltipHelper.visible = true;
			break;
		case GameStats.GameState.Pause:
			this.tooltipHelper.visible = false;
			break;
		case GameStats.GameState.GameOver:
			this.tooltipHelper.visible = false;
			break;
		case GameStats.GameState.LevelComplete:
			this.tooltipHelper.visible = false;
			break;
		default:
			break;
		}
	}

	void showGameStats() {
#if UNITY_IOS
		if (GUILayout.Button ("Menu")) {
			if (GameStats.state == GameStats.GameState.InGame) {
				GameStats.state = GameStats.GameState.Pause;
			} else if (GameStats.state == GameStats.GameState.Pause) {
				GameStats.state = GameStats.GameState.InGame;
			}
		}
#endif
		GUILayout.Label ("Wave: " + (GameStats.wave + 1).ToString () + " Seeds: " + GameStats.seeds.ToString ());
	}

	void showPlantSelection() {
		for (var i = 0; i < GameStats.plantList.Count; ++i) {
			Plant plant = GameStats.plantList [i];
			if (GUILayout.Toggle (this.selectedPlant == i, plant.name + " (" + plant.cost.ToString () + ")")) {
				this.selectedPlant = i;
			}
		}
	}

	void showPauseMenu() {
		var win = GUILayout.Window (0, this.pauseMenuRect, new GUI.WindowFunction(this.renderPauseMenu), "");
		this.pauseMenuRect.position = new Vector2 ((Screen.width - win.width) / 2, (Screen.height - win.height) / 2);
	}

	void showGameOverMenu() {
		var win = GUILayout.Window (0, this.gameOverMenuRect, new GUI.WindowFunction(this.renderGameOverMenu), "");
		this.gameOverMenuRect.position = new Vector2 ((Screen.width - win.width) / 2, (Screen.height - win.height) / 2);
	}

	void showLevelCompleteMenu() {
		var win = GUILayout.Window (0, this.levelCompleteRect, new GUI.WindowFunction(this.renderLevelCompleteMenu), "");
		this.levelCompleteRect.position = new Vector2 ((Screen.width - win.width) / 2, (Screen.height - win.height) / 2);
	}

	void renderPauseMenu (int id) {
		if (GUILayout.Button ("Continue")) {
			if (GameStats.state == GameStats.GameState.InGame) {
				GameStats.state = GameStats.GameState.Pause;
			} else if (GameStats.state == GameStats.GameState.Pause) {
				GameStats.state = GameStats.GameState.InGame;
			}
		}

		if (GUILayout.Button ("Return to Main Menu")) {
			GameStats.state = GameStats.GameState.MainMenu;
			SceneManager.LoadScene (this.mainMenuName);
		}
	}

	void renderGameOverMenu (int id) {
		GUILayout.Label ("Game Over");
		if (GUILayout.Button ("Retry")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}

		if (GUILayout.Button ("Return to Main Menu")) {
			GameStats.state = GameStats.GameState.MainMenu;
			SceneManager.LoadScene (this.mainMenuName);
		}
	}

	void renderLevelCompleteMenu (int id) {
		GUILayout.Label ("Level Complete");
		if (GUILayout.Button ("Continue")) {
			GameStats.state = GameStats.GameState.MainMenu;
			SceneManager.LoadScene (this.nextLevelName);
		}

		if (GUILayout.Button ("Return to Main Menu")) {
			GameStats.state = GameStats.GameState.MainMenu;
			SceneManager.LoadScene (this.mainMenuName);
		}
	}
}
