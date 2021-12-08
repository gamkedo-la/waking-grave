using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private int damage;
	private int direction;

	// Use this for initializationç
	public void SetDirection( bool isRight ) {
		direction = isRight ? 1 : -1;
	}

	private void Update() {
		if(direction != 0) {
			transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		// if not isTrigger means its a solid object and should be deleted
		if(!other.CompareTag("Player") && !other.isTrigger) {
			EnemyHealthManager temp = other.GetComponent<EnemyHealthManager>();
			if(temp) {
				temp.GetDamaged(damage);
			}
			Destroy(gameObject);
		}
	}
}