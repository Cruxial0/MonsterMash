using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set text to scene name
        this.GetComponent<TextMeshProUGUI>().text = SceneManager.GetActiveScene().name;
    }
}
