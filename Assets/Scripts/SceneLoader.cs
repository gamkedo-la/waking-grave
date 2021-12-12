using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadSelectionScreen()
    {
        SceneManager.LoadScene("SelectionScreen");
    }
}
