using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBehavior : MonoBehaviour {

	public string sceneGameOver;
	public string sceneLevelComplete;

	void Start () {
	}

	void Update () {
		switch (GameStats.state) {
		case GameStats.GameState.InGame:
			break;
		case GameStats.GameState.GameOver:
			SceneManager.LoadScene (this.sceneGameOver);
			break;
		case GameStats.GameState.LevelComplete:
			SceneManager.LoadScene (this.sceneLevelComplete);
			break;
		default:
			break;
		}
	}
}
