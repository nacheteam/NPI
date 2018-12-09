using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.RuntimeGizmos;
using UnityEngine.SceneManagement;

public class Rotation : MonoBehaviour {
    
    private Controller controller;
    public GameObject david;
    public GameObject[] waypoints;
    float z_old;
    float y_old;
    float distancia_entre_dedos_old;
    int current;
    bool extended_fingers;

    // Use this for initialization
    void Start ()
    {
        controller = new Controller();
        current = 0;
        extended_fingers = false;
    }
	
	// Update is called once per frame
	void Update () {
        Frame frame = controller.Frame();

        if (frame.Hands.Count > 0)
        {
            List<Hand> hands = frame.Hands;
            Hand hand0 = hands[0];
            Vector position = hand0.PalmPosition;
            Vector velocity = hand0.PalmVelocity;
            Vector direction = hand0.Direction;
            bool move = false;
            float velocity_x = hand0.PalmVelocity.ToVector3().x;
            var thumb0 = hand0.Fingers[0];
            var index0 = hand0.Fingers[1];
            var middle0 = hand0.Fingers[3];
            var ring0 = hand0.Fingers[4];
            var pinky0 = hand0.Fingers[4];
            if (frame.Hands.Count == 1)
            {
                Debug.Log("Direccion");
                Debug.Log(pinky0.Direction.ToVector3());
                if (hand0.IsRight)
                {
                    if (hand0.PalmNormal.ToVector3().x < 0.6 && hand0.PalmNormal.ToVector3().x > -0.3 && hand0.PalmNormal.ToVector3().z > 0)
                    {
                        david.transform.rotation = Quaternion.Euler(90 * hand0.PalmNormal.ToVector3().y, 0, 0);
                        move = true;
                    }
                    if (hand0.PalmNormal.ToVector3().z > 0 && hand0.PalmNormal.ToVector3().y > -0.3 && hand0.PalmNormal.ToVector3().y < 0.3)
                    {
                        david.transform.Rotate(0, 5 * hand0.PalmNormal.ToVector3().x, 0);
                        move = true;
                    }
                }
                // x entre 0.3 y 1, y < 0.7, z entre -0.3 y 0.3
                if (hand0.PalmNormal.ToVector3().x > 0.3 && hand0.PalmNormal.ToVector3().y < -0.7 && hand0.PalmNormal.ToVector3().z < 0.3 && hand0.PalmNormal.ToVector3().z > -0.5)
                {
                    if (!index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && extended_fingers)
                    {
                        Debug.Log("Dedos yano-extendidos");
                        david.SetActive(true);
                    }
                    else if (index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended && !extended_fingers)
                    {
                        Debug.Log("Dedos yasi-extendidos");
                        david.SetActive(false);
                    }
                }
                if (index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended)
                {
                    Debug.Log("Dedos extendidos");
                    extended_fingers = true;
                }
                if (!index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended)
                {
                    Debug.Log("Dedos no extendidos");
                    extended_fingers = false;
                }
            }
            /*************************************************************************************************
             * A DOS MANOS
             * ***********************************************************************************************/
            else if (frame.Hands.Count > 1)
            {
                Hand hand1 = hands[1];

                var thumb1 = hand1.Fingers[0];
                var index1 = hand1.Fingers[1];
                var middle1 = hand1.Fingers[3];
                var ring1 = hand1.Fingers[4];
                var pinky1 = hand1.Fingers[4];

                Debug.Log("2");
                Debug.Log(!index0.IsExtended);
                Debug.Log(!middle0.IsExtended);
                Debug.Log(!ring0.IsExtended);
                Debug.Log(pinky0.IsExtended);
                if (!index0.IsExtended && !middle0.IsExtended && pinky0.IsExtended && !move)
                {
                    if (!index1.IsExtended && !middle1.IsExtended && pinky1.IsExtended && !move)
                    {
                        Debug.Log("Atras");
                        var pinkys_distance = Vector3.Distance(pinky0.TipPosition.ToVector3(), pinky1.TipPosition.ToVector3());
                        Debug.Log(pinkys_distance);
                        if (pinkys_distance < 10){
                            SceneManager.LoadScene("Menu");
                        }
                    }
                }
                float object_vel = 0;
                if (index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && !move)
                {
                    if (index1.IsExtended && !middle1.IsExtended && !ring1.IsExtended && !pinky1.IsExtended && !move)
                    {
                        var distancia_entre_dedos_new = Vector3.Distance(index0.TipPosition.ToVector3(), index1.TipPosition.ToVector3());
                        if (distancia_entre_dedos_old > distancia_entre_dedos_new)
                        {
                            object_vel = hand0.PalmVelocity.ToVector3().x * hand1.PalmVelocity.ToVector3().y / 5000;
                            current = 1;
                        }
                        else
                        {
                            object_vel = hand0.PalmVelocity.ToVector3().x * hand1.PalmVelocity.ToVector3().y / 5000;
                            current = 0;
                        }
                        distancia_entre_dedos_old = distancia_entre_dedos_new;
                        }
                }
                if (object_vel < 0)
                    object_vel *= -1;
                david.transform.position = Vector3.MoveTowards(david.transform.position, waypoints[current].transform.position, Time.deltaTime * object_vel);
            }
            
        }
    }
}
