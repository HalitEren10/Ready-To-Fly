using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCounterSC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.SetInt("DeathCount", 0);
    }
    private void Update()
    {
        SceneManager.LoadScene(1);
    }
}
