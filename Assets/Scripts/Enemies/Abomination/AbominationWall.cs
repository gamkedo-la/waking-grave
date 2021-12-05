using UnityEngine;

public class AbominationWall : MonoBehaviour
{

	[SerializeField] private float speed;
	private Rigidbody2D rb2D;

	// Use this for initialization
	public void SetDirection( bool isRight ) {
		rb2D = GetComponent<Rigidbody2D>();
		rb2D.velocity = transform.right * (isRight ? 1 : -1) * speed;
	}

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Wall")){
            Destroy(gameObject);
        }
    }
}
