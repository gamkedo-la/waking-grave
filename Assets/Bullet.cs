using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb2D;

	// Use this for initialization
	void Start () {
		rb2D.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		// Enemy enemy = hitInfo.GetComponent<Enemy>();
		// if (enemy != null)
		// {
		// 	enemy.TakeDamage(damage);
		// }

		// Instantiate(impactEffect, transform.position, transform.rotation);

		Destroy(gameObject);
	}
}