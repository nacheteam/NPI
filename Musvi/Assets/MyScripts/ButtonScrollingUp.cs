using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonScrollingUp : MonoBehaviour {
    string text1;
    string text2;
    string text3;
    string text4;
    public int actual_pos;
    public Camera cam;
    public ScrollRect myScrollRect;
    public GameObject sphere;
    public GameObject image;
    public Text text;
    public int sum;
    public Sprite image1;
    public Sprite image2;
    public Sprite image3;
    public Sprite image4;
    float full_sum;
    private float nextActionTime = 0.0f;
    public float period = 0.5f;
    float actual_time;
    public List<Sprite> images;
    public List<string> texts;
    bool first;
    bool waiting;
    bool listening;
    private ApiAiModule ai_module;

    // Use this for initialization
    void Start ()
    {
        sum = 0;
        images = new List<Sprite>();
        texts = new List<string>();
        text1 = "Duelo a garrotazos o La riña​ es una de las Pinturas negras que Francisco de Goya realizó para la decoración de los muros de la casa —llamada la Quinta del Sordo— que el pintor adquirió en 1819. La obra ocupaba un lugar en el muro de la izquierda mirando desde la puerta de la planta alta de la casa, compartiendo la pared con Las Parcas y dejando en medio una ventana.";
        text2 = "Visión fantástica o Asmodea es una de las Pinturas negras que formaron parte de la decoración de los muros de la casa —llamada la Quinta del Sordo— que Francisco de Goya adquirió en 1819. La obra ocupaba un lugar junto a la Procesión del Santo Oficio en un muro de la planta alta de la casa, situado a la derecha de la puerta, enfrentado a la pared donde estaban Las Parcas.";
        text3 = "Las meninas (como se conoce a este cuadro desde el siglo XIX) o La familia de Felipe IV (según se describe en el inventario de 1734) se considera la obra maestra del pintor del Siglo de Oro español Diego Velázquez. Acabado en 1656, según Antonio Palomino, fecha unánimemente aceptada por la crítica, corresponde al último periodo estilístico del artista, el de plena madurez.";
        text4 = "Para entender desde un punto de vista histórico esta obra de Velázquez, hay que remontarse a lo que estaba sucediendo desde finales del siglo XVI y principios del XVII. Los Países Bajos (liderados por su noble más importante, Guillermo de Orange) estaban inmersos en la guerra de los ochenta años o guerra de Flandes, en la que luchaban por independizarse de España.";
        images.Add(image1);
        images.Add(image3);
        images.Add(image2);
        images.Add(image4);
        texts.Add(text1);
        texts.Add(text3);
        texts.Add(text2);
        texts.Add(text4);
        actual_pos = 0;
        waiting = false;
        first = true;
        ai_module = new ApiAiModule();
        listening = false;
        InvokeRepeating("ExecuteAfterTime", 2, 2);
    }
    void ExecuteAfterTime()
    {
        RaycastHit hit;
        Vector3 pos = cam.transform.position;
        if (Physics.Raycast(pos, cam.transform.forward, out hit, 10000))
        {
            if (hit.transform.name == "NextImage")
            {
                sum = 2;
                actual_pos+=sum;
                if (actual_pos > images.Count && (actual_pos % 2) == 0)
                    actual_pos = 1;
                else if (actual_pos > images.Count  && (actual_pos % 2) != 0)
                    actual_pos = 0;
                image.GetComponent<SpriteRenderer>().sprite = images[actual_pos];
                text.GetComponent<Text>().text = texts[actual_pos];
            }
            else if (hit.transform.name == "PreviousImage")
            {
                sum = 2;
                actual_pos-=sum;
                if (actual_pos < -1)
                    actual_pos = 3;
                else if (actual_pos < 0)
                    actual_pos = 4;
                image.GetComponent<SpriteRenderer>().sprite = images[actual_pos];
                text.GetComponent<Text>().text = texts[actual_pos];
            }
            myScrollRect.verticalNormalizedPosition = full_sum;
            Debug.Log(hit.transform.name);
        }
    }
        // Update is called once per frame
    void Update(){
    if(Input.touchCount == 1){
        if(Input.GetTouch(0).phase == TouchPhase.Began && !listening)
        {
            ai_module.StartNativeRecognition();
            listening = true;
            text.GetComponent<Text>().text = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
        } else if (Input.GetTouch(0).phase == TouchPhase.Began && listening){
            listening = false;
            //ai_module.StopListening();
            //ai_module.SendText();
            text.GetComponent<Text>().text = "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB\nBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB";
        }
    }

        Vector3 pos = cam.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(pos, cam.transform.forward, out hit, 10000))
        {
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
