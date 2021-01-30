using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    float yPos;
    float lowerBound = Screen.height / 4;
    float upperBound = Screen.height * 3 / 4;
    public float speed = 0f;
    float acceleration = 0.0005f;
    float threshold = 0.00005f;
    float direction;
    public GameObject menu;

    void Update()
    {
        if (menu.activeSelf)
        {
            speed = 0;
            return;
        }
        yPos = Input.mousePosition.y;
        if (yPos >= upperBound || yPos <= lowerBound)
        {
            if (yPos >= upperBound && transform.position.x <= 0.125)
            {
                speed = 0;
            }
            else if (yPos <= lowerBound && transform.position.x >= 13.875)
            {
                speed = 0;
            }
            else
            {
                if (yPos >= upperBound) speed += (acceleration * (yPos - upperBound) / Screen.height);
                else speed -= (acceleration * (lowerBound - yPos) / Screen.height);
                transform.Translate(0, speed, 0);
            }
        }
        else
        {
            if (Mathf.Abs(speed) <= threshold)
            {
                speed = 0;
                return;
            }
            if (speed > 0)
            {
                speed -= acceleration;
                if (speed > 0 && transform.position.x + speed >= 13.875) speed = 0;
            }
            else if (speed < 0)
            {
                speed += acceleration;
                if (speed < 0 && transform.position.x + speed <= 0.125) speed = 0;
            }
            else return;
            transform.Translate(0, speed, 0);
        }
        if (transform.position.x <= 0.125f) transform.position = new Vector3(0.125f, transform.position.y, transform.position.z);
        else if (transform.position.x >= 13.875) transform.position = new Vector3(13.875f, transform.position.y, transform.position.z);
    }
}
