using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class NFCReader : MonoBehaviour {

	private string tagID;
	bool already_switched;

	public GameObject image;
	public GameObject text_;

	// Use this for initialization
	void Start () {
			already_switched = false;
	}

	// Update is called once per frame
	void Update ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
					if(!already_switched){
		            try
		            {
		                // Creamos un intent y lo obtenemos
		                AndroidJavaObject mActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
		                AndroidJavaObject mIntent = mActivity.Call<AndroidJavaObject>("getIntent");

										//Comprobamos si tiene en los extras el campo "Id"
										bool hasExtra = mIntent.Call<bool>("hasExtra", "Id");

										//Si no tiene ese extra "Id", lo dejamos pasar, ya que nunca habremos analizado ese intent
										if(!hasExtra){

												//Cogemos sus acciones
				                string sAction = mIntent.Call<String>("getAction"); // resulte are returned in the Intent object
				                if (sAction == "android.nfc.action.NDEF_DISCOVERED")
				                {
				                    Debug.Log("Tag of type NDEF");
				                }
				                else if (sAction == "android.nfc.action.TECH_DISCOVERED") // Si era un intent de un NFC
				                {
														//Entonces cambiamos la imagen, y el texto
														GetComponent<ButtonScrollingUp>().actual_pos = GetComponent<ButtonScrollingUp>().actual_pos + 1;
														if (GetComponent<ButtonScrollingUp>().actual_pos > GetComponent<ButtonScrollingUp>().images.Count) GetComponent<ButtonScrollingUp>().actual_pos = 0;
														image.GetComponent<SpriteRenderer>().sprite = GetComponent<ButtonScrollingUp>().images[GetComponent<ButtonScrollingUp>().actual_pos];
														text_.GetComponent<Text>().text = GetComponent<ButtonScrollingUp>().texts[GetComponent<ButtonScrollingUp>().actual_pos];

														//Añadimos el campo "Id" en los extras para no volver a coger este Intent
														mIntent.Call<AndroidJavaObject>("putExtra", "Id", 0);

														//Y cambiamos los contextos del dialogFlow
														switch (GetComponent<ButtonScrollingUp>().actual_pos)
														{
															case 0:
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Goya");
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Garrotazos");
																break;
															case 1:
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Velazquez");
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Meninas");
																break;
															case 2:
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Goya");
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Fusilamientos");
																break;
															case 3:
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Velazquez");
																GetComponent<ButtonScrollingUp>().ai_module.SendText("Breda");
																break;
														}
				                    return;
				                }
				                else if (sAction == "android.nfc.action.TAG_DISCOVERED")
				                {
													Debug.Log("Not supported");
				                }
				                else
				                {
				                    Debug.Log("Scan a NFC tag to make the cube disappear...");
				                    return;
				                }
										}
		            }
		            catch (Exception ex)
		            {
		                string text = ex.Message;
		                Debug.Log(text);
		            }
						}
        }

    }
}
