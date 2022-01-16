using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelfDestruct : MonoBehaviour
{
    public float removeSeconds = 5.0f;
    public string nextSceneName;
    public bool isGraveyard;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, removeSeconds);
    }

    private void OnDestroy() {
        if(isGraveyard) {
            PlayerStats.finishedGraveyard = true;
            CheckpointManager.instance.lastCheckpointPos = Vector2.zero;
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
