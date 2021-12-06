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

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.name != "Player" || other.gameObject.name != "Floor")
      {
			Debug.Log("hit: " + other.gameObject.name);
			rb2D.velocity = Vector2.zero;
			Destroy(gameObject);
		}
	}
}