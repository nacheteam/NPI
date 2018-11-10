using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class NFCReader : MonoBehaviour {

	private string tagID;
	private Text tag_output_text;
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
		                // Create new NFC Android object
		                AndroidJavaObject mActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity"); // Activities open apps
		                AndroidJavaObject mIntent = mActivity.Call<AndroidJavaObject>("getIntent");
										bool hasExtra = mIntent.Call<bool>("hasExtra", "Id");
										AndroidJavaObject extras = mIntent.Call<AndroidJavaObject>("getExtras");

										//text_.GetComponent<Text>().text = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA\nAAAAAAACCC\nAB\n" + (hasExtra).ToString() + "\n" + (extras != null).ToString();

										if(!hasExtra){
				                string sAction = mIntent.Call<String>("getAction"); // resulte are returned in the Intent object
				                if (sAction == "android.nfc.action.NDEF_DISCOVERED")
				                {
				                    Debug.Log("Tag of type NDEF");
				                }
				                else if (sAction == "android.nfc.action.TECH_DISCOVERED")
				                {
														GetComponent<ButtonScrollingUp>().actual_pos = GetComponent<ButtonScrollingUp>().actual_pos + 1;
														if (GetComponent<ButtonScrollingUp>().actual_pos > GetComponent<ButtonScrollingUp>().images.Count) GetComponent<ButtonScrollingUp>().actual_pos = 0;
														image.GetComponent<SpriteRenderer>().sprite = GetComponent<ButtonScrollingUp>().images[GetComponent<ButtonScrollingUp>().actual_pos];
														mIntent.Call<AndroidJavaObject>("putExtra", "Id", 0);
														text_.GetComponent<Text>().text = GetComponent<ButtonScrollingUp>().texts[GetComponent<ButtonScrollingUp>().actual_pos];

														/*
														AndroidJavaObject newIntent = mActivity.Call<AndroidJavaObject>("getIntent");
														int FLAG_ACTIVITY_NEW_TASK = mIntent.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");
														int FLAG_ACTIVITY_CLEAR_TOP = mIntent.GetStatic<int>("FLAG_ACTIVITY_CLEAR_TOP");
														sAction = "";

														int orOP = FLAG_ACTIVITY_NEW_TASK | FLAG_ACTIVITY_CLEAR_TOP;
														mIntent.Call<AndroidJavaObject>("addFlags", orOP);
														*/
														//mActivity.Call("finish");
				                    return;
				                }
				                else if (sAction == "android.nfc.action.TAG_DISCOVERED")
				                {
													tag_output_text.text += "Not supported";
				                }
				                else
				                {
				                    tag_output_text.text = "Scan a NFC tag to make the cube disappear...";
				                    return;
				                }
										}
		            }
		            catch (Exception ex)
		            {
		                string text = ex.Message;
		                tag_output_text.text = text;
		            }
						}
        }

    }
}
