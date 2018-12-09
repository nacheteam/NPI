using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Leap;
using Leap.Unity;
using Leap.Unity.RuntimeGizmos;
using Leap.Unity.Attributes;
using Leap.Unity.Interaction.Internal;
using Leap.Unity.Query;
using System;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Onclick_Obs : MonoBehaviour
{
    // Use this for initialization

    void OnClick()
    {
        Debug.Log("HEI");
        //SceneManager.LoadScene("ObservationMode");
    }
}
