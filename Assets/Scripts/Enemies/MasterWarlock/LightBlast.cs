using UnityEngine;

public class LightBlast : MonoBehaviour
{
    public GameObject nextLightblast;
    private void OnEnable() {
        GetComponent<Animator>().Play("Lightblast");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            other.GetComponent<PlayerController>().GetDamaged(transform.position.x, false);
        }
    }

    private void ActivateNext() {
        if(nextLightblast) {
            nextLightblast.SetActive(true);
        } else {
            GameObject.Find("MasterWarlock").GetComponent<WarlockSM>().finishedLightblast = true;
        }
    }

    private void Disable() {
        gameObject.SetActive(false);
    }
}
