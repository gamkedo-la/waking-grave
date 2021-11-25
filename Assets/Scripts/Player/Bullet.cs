using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private float speed = 20f;
	[SerializeField] private int damage = 40;
	private Rigidbody2D rb2D;

	// Use this for initializationç
	public void SetDirection( bool isRight ) {
		rb2D = GetComponent<Rigidbody2D>();
		rb2D.velocity = transform.right * (isRight ? 1 : -1) * speed;
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		// Enemy enemy = hitInfo.GetComponent<Enemy>();
		// if (enemy != null)
		// {
		// 	enemy.TakeDamage(damage);
		// }

		// Instantiate(impactEffect, transform.position, transform.rotation);
		Debug.Log("hitInfo.gameObject.name: " + hitInfo.gameObject.name);
		if (hitInfo.gameObject.name != "Player" || hitInfo.gameObject.name != "Floor")
        {
			Destroy(gameObject);
		}
	}
}