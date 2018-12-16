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
            //Inicializamos manos
            List<Hand> hands = frame.Hands;
            Hand hand0 = hands[0];

            //Inicializamos Dedos
            bool move = false;
            float velocity_x = hand0.PalmVelocity.ToVector3().x;
            var thumb0 = hand0.Fingers[0];
            var index0 = hand0.Fingers[1];
            var middle0 = hand0.Fingers[2];
            var ring0 = hand0.Fingers[3];
            var pinky0 = hand0.Fingers[4];

            //Si el Leap detecta una mano
            if (frame.Hands.Count == 1)
            {
                // Y esa mano es la derecha, entonces
                if (hand0.IsRight)
                {
                    //Miramos la orientación de la palma y en consecuencia lo rotamos en un eje o un otro
                    if (hand0.PalmNormal.ToVector3().x < 0.6 && hand0.PalmNormal.ToVector3().x > -0.3 && hand0.PalmNormal.ToVector3().z > 0)
                    {
                        david.transform.rotation = Quaternion.Euler(90 * hand0.PalmNormal.ToVector3().y, david.transform.rotation.eulerAngles.y, david.transform.rotation.eulerAngles.z);
                        move = true;
                    }
                    if (hand0.PalmNormal.ToVector3().z > 0 && hand0.PalmNormal.ToVector3().y > -0.3 && hand0.PalmNormal.ToVector3().y < 0.3)
                    {
                        david.transform.Rotate(0, 5 * hand0.PalmNormal.ToVector3().x, 0);
                        move = true;
                    }
                    // Miramos la orientación de la palma
                    if (hand0.PalmNormal.ToVector3().x > 0.3 && hand0.PalmNormal.ToVector3().y < -0.7 && hand0.PalmNormal.ToVector3().z < 0.3 && hand0.PalmNormal.ToVector3().z > -0.5)
                    {
                        // Si los dedos no están extendidos y antes sí, es decir hemos cerrado la mano
                        if (!index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && extended_fingers)
                        {
                            david.SetActive(true);
                        }
                        // Si los dedos están extendidos y antes no, es decir hemos abierto la mano
                        else if (index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended && !extended_fingers)
                        {
                            david.SetActive(false);
                        }
                    }
                    // Miramos si los dedos están extendidos o no
                    if (index0.IsExtended && middle0.IsExtended && ring0.IsExtended && pinky0.IsExtended)
                    {
                        extended_fingers = true;
                    }
                    if (!index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended)
                    {
                        extended_fingers = false;
                    }
                }
            }
            /*************************************************************************************************
             * A DOS MANOS
             * ***********************************************************************************************/
            else if (frame.Hands.Count > 1)
            {
                // Inicializamos mano
                Hand hand1 = hands[1];

                //Inicializamos dedos
                var thumb1 = hand1.Fingers[0];
                var index1 = hand1.Fingers[1];
                var middle1 = hand1.Fingers[2];
                var ring1 = hand1.Fingers[3];
                var pinky1 = hand1.Fingers[4];

                // Si los dedos no están extendidos excepto los meñiques de ambas manos
                if (!index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && pinky0.IsExtended && !move)
                {
                    if (!index1.IsExtended && !middle1.IsExtended && !ring0.IsExtended && pinky1.IsExtended && !move)
                    {
                        // Y los meñiques están juntos
                        var pinkys_distance = Vector3.Distance(pinky0.TipPosition.ToVector3(), pinky1.TipPosition.ToVector3());
                        if (pinkys_distance < 10){
                            // Entonces nos vamos al menú de nuevo
                            SceneManager.LoadScene("Menu");
                        }
                    }
                }
                // Si los dedos no están extendidos excepto los índices de ambas manos
                float object_vel = 0;
                if (index0.IsExtended && !middle0.IsExtended && !ring0.IsExtended && !pinky0.IsExtended && !move)
                {
                    if (index1.IsExtended && !middle1.IsExtended && !ring1.IsExtended && !pinky1.IsExtended && !move)
                    {
                        //Miramos la si se están alejando o acercando ambos índices entre sí
                        var distancia_entre_dedos_new = Vector3.Distance(index0.TipPosition.ToVector3(), index1.TipPosition.ToVector3());
                        // Calculamos una velocidad relativa a mabas manos
                        if (distancia_entre_dedos_old > distancia_entre_dedos_new)
                        {
                            object_vel = hand0.PalmVelocity.ToVector3().x * hand1.PalmVelocity.ToVector3().y / 5000;
                            // Si se acercan los índices entre sí, ponemos que el david debe dirigirse a su posición alejada (para hacer zoom out)
                            current = 1;
                        }
                        else
                        {
                            object_vel = hand0.PalmVelocity.ToVector3().x * hand1.PalmVelocity.ToVector3().y / 5000;
                            // Si se alejan los índices entre sí, ponemos que el david debe dirigirse a la posición de la cámara (para hacer zoom in)
                            current = 0;
                        }
                        distancia_entre_dedos_old = distancia_entre_dedos_new;
                        }
                }
                if (object_vel < 0)
                    object_vel *= -1;
                // Hacemos que se acerque o se aleje de la cámara según la velocidad de las manos al alejarse o acercarse
                david.transform.position = Vector3.MoveTowards(david.transform.position, waypoints[current].transform.position, Time.deltaTime * object_vel);
            }
            
        }
    }
}
