
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitDownside : MonoBehaviour
{
    bool downside = false;
    bool new_downside = false;

    void QuitingDownside()
    {
      //Si está casi horizontal u horizontal simplemente apagamos la app, por si se han olvidado de quitar la App.
      new_downside = Input.gyro.attitude.eulerAngles.x < 10 || Input.gyro.attitude.eulerAngles.x > 350 && Input.gyro.attitude.eulerAngles.y < 10 || Input.gyro.attitude.eulerAngles.y > 350;
      if(new_downside && downside){
          Application.Quit();
      }
      downside = Input.gyro.attitude.eulerAngles.x < 10 || Input.gyro.attitude.eulerAngles.x > 350 && Input.gyro.attitude.eulerAngles.y < 10 || Input.gyro.attitude.eulerAngles.y > 350;
    }

    void Start()
    {
      if (SystemInfo.supportsGyroscope)
      {
          Input.gyro.enabled = true;
      }
      InvokeRepeating("QuitingDownside", 1, 3);
    }

    void Update()
    {
    }

}
