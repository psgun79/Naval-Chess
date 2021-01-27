using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraMovement : MonoBehaviour
{
    public Text mousePos;
    float yPos;
    float lowerBound = Screen.height / 4;
    float upperBound = Screen.height * 3 / 4;
    float speed = 0f;
    float acceleration = 0.0005f;
    float direction;

    void Update()
    {
        mousePos.text = Input.mousePosition.x.ToString() + " / " + Input.mousePosition.y.ToString() + " / " + Input.mousePosition.z.ToString();
        yPos = Input.mousePosition.y;
        if (yPos >= upperBound || yPos <= lowerBound)
        {
            if (yPos >= upperBound && transform.position.x <= 0.125) return;
            else if (yPos <= lowerBound && transform.position.x >= 13.875) return;
            else
            {
                if (yPos >= upperBound) speed += (acceleration * (yPos - upperBound) / Screen.height);
                else speed -= (acceleration * (lowerBound - yPos) / Screen.height);
                transform.Translate(0, speed, 0);
                //transform.Translate(0, (yPos - Screen.height / 2) * acceleration, 0);
            }
        }
        else{
            if (speed > 0)
            {
                speed -= acceleration;
                if (speed < 0) speed = 0;
            }
            else if (speed < 0)
            {
                speed += acceleration;
                if (speed > 0) speed = 0;
            }
            else return;
            transform.Translate(0, speed, 0);
        }
    }
}
