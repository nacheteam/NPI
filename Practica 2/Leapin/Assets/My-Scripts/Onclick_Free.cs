using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Onclick_Free : MonoBehaviour
{

    // Use this for initialization
    void OnClick()
    {
        SceneManager.LoadScene("FreeMode");
    }
}
