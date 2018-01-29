using UnityEngine;

public class GemBehavior : MonoBehaviour {

	public Gem gem;

	void Start () {
		GameStats.seeds += this.gem.seeds;

		var audio = this.gameObject.AddComponent<AudioSource> ();
		if (this.gem.sound != null) {
			audio.clip = this.gem.sound;
			audio.Play ();
		}

		Destroy (this.gameObject, this.gem.lifeTime);
	}

}