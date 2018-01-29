using UnityEngine;

[System.Serializable]
public struct Gem {
	public Transform model;
	public AudioClip sound;
	public int seeds;
	public float lifeTime;
}