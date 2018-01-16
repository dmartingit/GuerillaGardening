using UnityEngine;

[System.Serializable]
public struct Plant {
	public Transform model;
	public Bullet bullet;
	public Gem gem;
	public string name;
	public int cost;
	public int health;
	public float attackspeed;
	public float movementspeed;
	public float gemSpawnRate;
}