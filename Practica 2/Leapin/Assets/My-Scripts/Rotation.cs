using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.RuntimeGizmos;

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
        Vector3 pitch_yaw_roll = new Vector3();
        if (frame.Hands.Count > 0)
        {
            List<Hand> hands = frame.Hands;
            Hand hand0 = hands[0];
            Vector position = hand0.PalmPosition;
            Vector velocity = hand0.PalmVelocity;
            Vector direction = hand0.Direction;
            /*
            Debug.Log("Normal de la palma:");
            Debug.Log(hand0.PalmNormal.ToVector3());
            */
            //Debug.Log("Roll:");
            bool move = false;
            float velocity_x = hand0.PalmVelocity.ToVector3().x;
            //Debug.Log(hand0.PalmNormal.Roll);
            var thumb0 = hand0.Fingers[0];
            var index0 = hand0.Fingers[1];
            var middle0 = hand0.Fingers[3];
            var ring0 = hand0.Fingers[4];
            var pinky0 = hand0.Fingers[4];

            if (velocity_x < 0 && index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended && thumb0.IsExtended)
            {
                if (hand0.IsRight)
                {
                    if (hand0.PalmNormal.ToVector3().x < -0.8 && hand0.PalmNormal.ToVector3().y < 0.2 && hand0.PalmNormal.ToVector3().y > -0.2)
                    {
                        move = true;
                        david.transform.Rotate(0, velocity_x / 75 * -1, 0);
                    }
                }
                else
                {
                    if (hand0.PalmNormal.ToVector3().x > 0.8 && hand0.PalmNormal.ToVector3().y < 0.2 && hand0.PalmNormal.ToVector3().y > -0.2)
                    {
                        move = true;
                        david.transform.Rotate(0, velocity_x / 75 * -1, 0);
                    }
                }

            }
            Debug.Log("Normal de la palma");
            Debug.Log(hand0.PalmNormal.ToVector3());

            if (!index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && extended_fingers)
            {
                Debug.Log("Dedos yano-extendidos");
                if (hand0.PalmNormal.ToVector3().y <= 1.0 && hand0.PalmNormal.ToVector3().y > 0.9 && hand0.PalmNormal.ToVector3().x < 0.2 && hand0.PalmNormal.ToVector3().y > -0.2)
                    david.SetActive(true);
            }else if (index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended && !extended_fingers)
            {
                Debug.Log("Dedos yasi-extendidos");
                if (hand0.PalmNormal.ToVector3().y <= 1.0 && hand0.PalmNormal.ToVector3().y > 0.9 && hand0.PalmNormal.ToVector3().x < 0.2 && hand0.PalmNormal.ToVector3().y > -0.2)
                    david.SetActive(false);
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


            if (index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && !thumb0.IsExtended && !move)
            {
                if (index0.Direction.x > -1 && index0.Direction.x < -0.8)
                {
                    if (hand0.PalmNormal.ToVector3().x > -0.2 && hand0.PalmNormal.ToVector3().x < 0.2 && hand0.PalmNormal.ToVector3().y > -0.2 && hand0.PalmNormal.ToVector3().y < 0.2)
                    {
                        float z_new = hand0.PalmPosition.ToVector3().z;
                        float y_new = hand0.PalmPosition.ToVector3().y;
                        float velocity_palm = hand0.PalmVelocity.ToVector3().z * hand0.PalmVelocity.ToVector3().y / 1000;
                        if (y_old > y_new && z_old > z_new)
                        {
                            //Debug.Log("1");
                            david.transform.Rotate(-velocity_palm, 0, 0);
                        }
                        else if (y_old > y_new && z_old < z_new)
                        {
                            //Debug.Log("2");
                            david.transform.Rotate(velocity_palm, 0, 0);
                        }
                        else if (y_old < y_new && z_old < z_new)
                        {
                            //Debug.Log("3");
                            david.transform.Rotate(-velocity_palm, 0, 0);
                        }
                        else if (y_old < y_new && z_old > z_new)
                        {
                            //Debug.Log("4");
                            david.transform.Rotate(velocity_palm, 0, 0);
                        }
                        z_old = z_new;
                        y_old = y_new;
                        //Debug.Log("Extendido");
                        //Debug.Log(index.TipPosition);
                    }
                }
            }

            if (frame.Hands.Count > 1)
            {
                Hand hand1 = hands[1];

                var thumb1 = hand1.Fingers[0];
                var index1 = hand1.Fingers[1];
                var middle1 = hand1.Fingers[3];
                var ring1 = hand1.Fingers[4];
                var pinky1 = hand1.Fingers[4];

                velocity_x = hand1.PalmVelocity.ToVector3().x;
                if (velocity_x > 0 && index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended && thumb0.IsExtended)
                {
                    if (hand1.IsRight)
                    {
                        if (hand1.PalmNormal.ToVector3().x < -0.8 && hand1.PalmNormal.ToVector3().y < 0.2 && hand1.PalmNormal.ToVector3().y > -0.2)
                        {
                            david.transform.Rotate(0, velocity_x / 75 * -1, 0);
                        }
                    }
                    else
                    {
                        if (hand1.PalmNormal.ToVector3().x > 0.8 && hand1.PalmNormal.ToVector3().y < 0.2 && hand1.PalmNormal.ToVector3().y > -0.2)
                        {
                            david.transform.Rotate(0, velocity_x / 75 * -1, 0);
                        }
                    }
                }

                float object_vel = 0;
                // ZOOM
                if (index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && thumb0.IsExtended && !move)
                {
                    if (index1.IsExtended && !middle1.IsExtended && !ring1.IsExtended && !pinky1.IsExtended && thumb1.IsExtended && !move)
                    {
                        /*
                        //Index 0 < 0.2
                        //Index 1 < -0.8
                        //Normal y, -1 ambas
                        // velocidades ambas una positiva y otra negativa
                        Debug.Log("Bucle 0:");
                        Debug.Log("Dirección del indice0:");
                        Debug.Log(index0.Direction.x);
                        Debug.Log("Dirección del indice1:");
                        Debug.Log(index1.Direction.x);
                        Debug.Log("normal del indice0:");
                        Debug.Log(hand0.PalmNormal.ToVector3().x);
                        Debug.Log("normal del indice1:");
                        Debug.Log(hand1.PalmNormal.ToVector3().x);
                        Debug.Log("Bucle 1:");
                        */

                        if (hand0.IsRight)
                        {
                            if (index1.Direction.x > 0 && index1.Direction.x < 0.3 && index0.Direction.x > -1 && index0.Direction.x < -0.7)
                            {
                                if (hand0.PalmNormal.ToVector3().y < -0.9 && hand0.PalmNormal.ToVector3().x < 0.2 && hand0.PalmNormal.ToVector3().x > -0.2 && hand1.PalmNormal.ToVector3().y < -0.9 && hand1.PalmNormal.ToVector3().x < 0.2 && hand1.PalmNormal.ToVector3().x > -0.2)
                                {
                                    Debug.Log("Normal de la palma");
                                    Debug.Log(hand0.PalmNormal.ToVector3());
                                    Debug.Log("Normal de la palma1");
                                    Debug.Log(hand1.PalmNormal.ToVector3());
                                    var distancia_entre_dedos_new = Vector3.Distance(index0.TipPosition.ToVector3(), index1.TipPosition.ToVector3());
                                    if (distancia_entre_dedos_old > distancia_entre_dedos_new)
                                    {
                                        object_vel = hand0.PalmVelocity.ToVector3().x * hand1.PalmVelocity.ToVector3().z / 500;
                                        current = 1;
                                    }
                                    else
                                    {
                                        object_vel = hand0.PalmVelocity.ToVector3().x * hand1.PalmVelocity.ToVector3().z / 500;
                                        current = 0;
                                    }
                                    distancia_entre_dedos_old = distancia_entre_dedos_new;
                                }
                            }
                        } else
                        {
                            if (index0.Direction.x > 0 && index0.Direction.x < 0.3 && index1.Direction.x > -1 && index1.Direction.x < -0.7)
                            {
                                if (hand0.PalmNormal.ToVector3().y < -0.9 && hand0.PalmNormal.ToVector3().x < 0.2 && hand0.PalmNormal.ToVector3().x > -0.2 && hand1.PalmNormal.ToVector3().y < -0.9 && hand1.PalmNormal.ToVector3().x < 0.2 && hand1.PalmNormal.ToVector3().x > -0.2)
                                {
                                    Debug.Log("Normal de la palma");
                                    Debug.Log(hand0.PalmNormal.ToVector3());
                                    Debug.Log("Normal de la palma1");
                                    Debug.Log(hand1.PalmNormal.ToVector3());
                                    var distancia_entre_dedos_new = Vector3.Distance(index1.TipPosition.ToVector3(), index0.TipPosition.ToVector3());
                                    if (distancia_entre_dedos_old > distancia_entre_dedos_new)
                                    {
                                        object_vel = hand1.PalmVelocity.ToVector3().x * hand0.PalmVelocity.ToVector3().z / 500;
                                        current = 1;
                                    }
                                    else
                                    {
                                        object_vel = hand1.PalmVelocity.ToVector3().x * hand0.PalmVelocity.ToVector3().z / 500;
                                        current = 0;
                                    }
                                    distancia_entre_dedos_old = distancia_entre_dedos_new;
                                }
                            }
                        }
                    }
                }
                david.transform.position = Vector3.MoveTowards(david.transform.position, waypoints[current].transform.position, Time.deltaTime * object_vel);
            }
            /*
            Debug.Log("Normal de la palma:");
            Debug.Log(hand0.Direction.ToVector3());
            Debug.Log("Pitch Yaw Roll:");
            pitch_yaw_roll.Set(hand0.Direction.Pitch, hand0.Direction.Yaw, hand0.PalmNormal.Roll);
            //Si queremos mirar si está hacia arriba o hacia abajo, entonces miramos si roll es 0, abajo, si roll es 3 arriba
            Debug.Log(pitch_yaw_roll);
            */
        }
    }
}
