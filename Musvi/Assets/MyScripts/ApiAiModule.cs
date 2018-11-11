/**
 * Copyright 2017 Google Inc. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Reflection;
using ApiAiSDK;
using ApiAiSDK.Model;
using ApiAiSDK.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Generic;

public class ApiAiModule : MonoBehaviour
{

    public Text text;
    public Text TextDialog;
    private ApiAiUnity apiAiUnity;
    private AudioSource aud;
    public AudioClip listeningSound;
    private TextToSpeech text_to_speech;

    private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings
    {
        NullValueHandling = NullValueHandling.Ignore,
    };

    private readonly Queue<Action> ExecuteOnMainThread = new Queue<Action>();

    // Use this for initialization
    IEnumerator Start()
    {

        // check access to the Microphone
        yield return Application.RequestUserAuthorization(UserAuthorization.Microphone);
        if (!Application.HasUserAuthorization(UserAuthorization.Microphone))
        {
            throw new NotSupportedException("Microphone using not authorized");
        }

        ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) =>
        {
            return true;
        };

        const string ACCESS_TOKEN = "f5ac8103d7da44fbb15a512990ecd204";

        var config = new AIConfiguration(ACCESS_TOKEN, SupportedLanguage.Spanish);

        apiAiUnity = new ApiAiUnity();
        apiAiUnity.Initialize(config);

        text_to_speech = GetComponent<TextToSpeech>();

        apiAiUnity.OnError += HandleOnError;
        apiAiUnity.OnResult += HandleOnResult;

        SendText("Garrotazos");
        SendText("Goya");
    }

    void HandleOnResult(object sender, AIResponseEventArgs e)
    {
        RunInMainThread(() => {
            var aiResponse = e.Response;
            if (aiResponse != null)
            {
                Debug.Log(aiResponse.Result.ResolvedQuery);

                var outText = JsonConvert.SerializeObject(aiResponse, jsonSettings);

		            text_to_speech.Speak(aiResponse.Result.Fulfillment.Speech);


            } else
            {
                Debug.LogError("Response is null");
            }
        });
    }

    void HandleOnError(object sender, AIErrorEventArgs e)
    {
        RunInMainThread(() => {
            Debug.LogException(e.Exception);
            Debug.Log(e.ToString());
            text_to_speech.Speak("Ha habido un error.");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (apiAiUnity != null)
        {
            apiAiUnity.Update();
        }

        // dispatch stuff on main thread
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue().Invoke();
        }
    }

    private void RunInMainThread(Action action)
    {
        ExecuteOnMainThread.Enqueue(action);
    }

    public void PluginInit()
    {

    }

    public void StartListening()
    {

      Debug.Log("StartListening");

      aud = GetComponent<AudioSource>();
      apiAiUnity.StartListening(aud);

    }

    public void StopListening()
    {
      try
      {
          Debug.Log("StopListening");

          apiAiUnity.StopListening();
      } catch (Exception ex)
      {
          Debug.LogException(ex);
      }
    }

    public void SendText(string text_)
    {
        AIResponse response = apiAiUnity.TextRequest(text_);

        if (response != null)
        {
            Debug.Log("Resolved query: " + response.Result.ResolvedQuery);
            var outText = JsonConvert.SerializeObject(response, jsonSettings);
            //var str_res = JObject.Parse(response.Result.ResolvedQuery);
            //var answer = str_res["result"]["resolvedQuery"].ToString();

            //text_to_speech.Speak("La respuesta es: " + respons);

            //TextDialog.text = outText;

            //text_to_speech.Speak(response.Result.Fulfillment.Speech);
        } else
        {
            Debug.LogError("Response is null");
        }

    }

    public void StartNativeRecognition()
    {
        try
        {
            apiAiUnity.StartNativeRecognition();
        } catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
