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
        text1 = "Este cuadro pertenece a un conjunto llamado Pinturas Negras y se puede observar en el mismo a dos villanos luchando a bastonazos enterrados hasta las rodillas. Estos duelos eran frecuentes, al igual que los de caballeros, sólo que se utilizaban armas rudimentarias. Una posible interpretación que se le ha dado es la lucha entre las dos españas entre progresistas y moderados.";
        text2 = "Goya tenía buenas opiniones acerca de los franceses y cuando España sufrió la invasión por parte de Francia éste deseaba que los españoles se empaparan de la filosofía y forma de pensar francesas. Su desengaño vino, entre otras cosas, con este hecho retratado en el cuadro. Los fusilamientos del tres de mayo se produjeron tras un motín el 2 de mayo por parte de los españoles en contra del ejército francés produciendo muertes entre los invasores. Al día siguiente los franceses pedían  venganza y mataban de forma indiscriminada a la gente sin juicio previo, cosa que Goya plasmó en este cuadro y otros similares.";
        text3 = "El cuadro representa una escena retratada donde podemos encontrar once personajes: la infanta Margarita, Isabel de Velasco, María Agustina Sarmiento de Sotomayor, Mari Bárbola, Nicolasito Pertusato, Marcela de Ulloa, un guardadamas, José Nieto Velázquez, Diego Velázquez, Felipe cuarto y su esposa Mariana de Austria, todos ellos nobles que rodeaban a la infanta o el propio autor de la obra. El hecho de que él mismo se pintase en el cuadro era innovador haciéndolo único por esta característica.";
        text4 = "El cuadro de La Rendición de Breda representa las capitulaciones del ejército español tras el sitio de los holandeses a la ciudad para tomarla. Este sitio fue reconocido por los españoles como una victoria de los holandeses incluso halagando las técnicas empleadas en el mismo. En el cuadro se puede ver a la ciudad de Breda de fondo y a los dos bandos reconociendo la derrota del bando español.";

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
