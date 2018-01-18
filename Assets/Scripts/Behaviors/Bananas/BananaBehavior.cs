using UnityEngine;

public class BananaBehavior : MonoBehaviour {

	void OnCollisionEnter (Collision col) {
		var go = col.gameObject;
		if (go.tag == "Gorilla") {
			GameStats.state = GameStats.GameState.GameOver;
			Destroy (go.transform.parent.gameObject);
			Destroy (this.gameObject);
		}
	}

}