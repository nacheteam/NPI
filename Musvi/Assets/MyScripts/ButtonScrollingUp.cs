using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScrollingUp : MonoBehaviour {

    string text1;
    string text2;
    string text3;
    string text4;
    public Camera cam;
    public ScrollRect myScrollRect;
    public GameObject sphere;
    public GameObject image;
    public Text text;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    float full_sum;
    public List<Sprite> images;
    public List<string> texts;
    public ApiAiModule ai_module;
    public int actual_pos;
    public TextToSpeech tts;

    // Use this for initialization
    void Start ()
    {
        //Inicializamos:
        //Inicializamos la clase de text to speech
        tts = GetComponent<TextToSpeech>();

        //Inicializamos una lista de sprites (imágenes)
        images = new List<Sprite>();

        //Inicializamos una lista de string
        texts = new List<string>();

        //Inicializamos los textos
        text1 = "Duelo a garrotazos o La riña​ es una de las Pinturas negras que Francisco de Goya realizó para la decoración de los muros de la casa —llamada la Quinta del Sordo— que el pintor adquirió en 1819. La obra ocupaba un lugar en el muro de la izquierda mirando desde la puerta de la planta alta de la casa, compartiendo la pared con Las Parcas y dejando en medio una ventana.";
        text2 = "Visión fantástica o Asmodea es una de las Pinturas negras que formaron parte de la decoración de los muros de la casa —llamada la Quinta del Sordo— que Francisco de Goya adquirió en 1819. La obra ocupaba un lugar junto a la Procesión del Santo Oficio en un muro de la planta alta de la casa, situado a la derecha de la puerta, enfrentado a la pared donde estaban Las Parcas.";
        text3 = "Las meninas (como se conoce a este cuadro desde el siglo XIX) o La familia de Felipe IV (según se describe en el inventario de 1734) se considera la obra maestra del pintor del Siglo de Oro español Diego Velázquez. Acabado en 1656, según Antonio Palomino, fecha unánimemente aceptada por la crítica, corresponde al último periodo estilístico del artista, el de plena madurez.";
        text4 = "Para entender desde un punto de vista histórico esta obra de Velázquez, hay que remontarse a lo que estaba sucediendo desde finales del siglo XVI y principios del XVII. Los Países Bajos (liderados por su noble más importante, Guillermo de Orange) estaban inmersos en la guerra de los ochenta años o guerra de Flandes, en la que luchaban por independizarse de España.";

        //Añadimos las imnagenes y los textos a sus respectivos vectores
        images.Add(image1);
        images.Add(image3);
        images.Add(image2);
        images.Add(image4);
        texts.Add(text1);
        texts.Add(text3);
        texts.Add(text2);
        texts.Add(text4);

        //Inicializamos la posición actual de los cuadros a los qe estamos mirando
        actual_pos = 0;

        //Inicializamos el modulo de dialog flow
        ai_module = GetComponent<ApiAiModule>();

        //Invocamos de forma repetitiva el método ExecuteAfterTime
        InvokeRepeating("ExecuteAfterTime", 2, 2);
    }

    //Comprobamos cada 2 segundos si estamos apuntando al botón NextImage, o PreviousImage, en caso de hacerlo cambiamos la imagen
    void ExecuteAfterTime()
    {
        //Inicializamos las variables necesarias para hacer el raycasting
        int sum;
        RaycastHit hit;
        Vector3 pos = cam.transform.position;

        //lanzamos un rayo desde la cámara hacia delante y si golpeamos algún objeto con un el rayo nos devuelve true
        if (Physics.Raycast(pos, cam.transform.forward, out hit, 10000))
        {
            //Si el nombre del objeto golpeado es NextImage o PreviousImage, entonces cambiamos el contenido de los vectores images y texts
            if (hit.transform.name == "NextImage")
            {
                sum = 2;
                actual_pos+=sum;
                if (actual_pos >= images.Count && (actual_pos % 2) == 0)
                    actual_pos = 0;
                else if (actual_pos >= images.Count  && (actual_pos % 2) != 0)
                    actual_pos = 1;
                image.GetComponent<SpriteRenderer>().sprite = images[actual_pos];
                text.GetComponent<Text>().text = texts[actual_pos];
            }
            else if (hit.transform.name == "PreviousImage")
            {
                sum = 2;
                actual_pos-=sum;
                if (actual_pos < 0 && (actual_pos % 2) == 0)
                    actual_pos = 2;
                else if (actual_pos < 0 && (actual_pos % 2) != 0)
                    actual_pos = 3;
                image.GetComponent<SpriteRenderer>().sprite = images[actual_pos];
                text.GetComponent<Text>().text = texts[actual_pos];
            }

            //Según cuál sea el cuadro y el autor cambiamos el contexto del dialogflow
            switch (actual_pos)
            {
              case 0:
                ai_module.SendText("Goya");
                ai_module.SendText("Garrotazos");
                break;
              case 1:
                ai_module.SendText("Velazquez");
                ai_module.SendText("Meninas");
                break;
              case 2:
                ai_module.SendText("Goya");
                ai_module.SendText("Fusilamientos");
                break;
              case 3:
                ai_module.SendText("Velazquez");
                ai_module.SendText("Breda");
                break;
            }
        }
    }
        // Update is called once per frame
    void Update(){
        // Si recibimos alguno toque en pantalla, entonces llamamos al reconocimiento de voz
        if(Input.touchCount == 1){
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
              ai_module.StartNativeRecognition();
            }
        }

        Vector3 pos = cam.transform.position;
        RaycastHit hit;

        //lanzamos un rayo desde la cámara hacia delante y si golpeamos algún objeto con un el rayo nos devuelve true
        if (Physics.Raycast(pos, cam.transform.forward, out hit, 10000))
        {
            //Si el nombre del objeto golpeado es ScrollUp o ScrollDown, entonces hacemos scroll hacia arriba o hacia abajo
            if (hit.transform.name == "ScrollUp")
            {
                full_sum = myScrollRect.verticalNormalizedPosition + 0.002f;
                if (full_sum > 1.0f)
                {
                    full_sum = 1.0f;
                }
            }
            else if (hit.transform.name == "ScrollDown")
            {
                full_sum = myScrollRect.verticalNormalizedPosition - 0.002f;
                if (full_sum < 0.0f)
                {
                    full_sum = 0.0f;
                }
            }
            myScrollRect.verticalNormalizedPosition = full_sum;
        }
    }
}
