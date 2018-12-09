using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Onclick_Play : MonoBehaviour {
    void OnClick()
    {
        Debug.Log("HEI");
        SceneManager.LoadScene("PlayMode");
    }
}
