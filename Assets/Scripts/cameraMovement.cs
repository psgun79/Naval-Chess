using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    float y_origin = 6.5f;
    float z_origin = 4.5f;
    float lowerBound = Screen.height / 4;
    float upperBound = Screen.height * 3 / 4;
    public float speed = 0f;
    float acceleration = 0.0005f;
    float threshold = 0.00005f;
    float direction;
    public bool sniping;
    public boardSystem system;
    public GameObject menu;

    void Update()
    {
        if (sniping) y_origin = 8.5f;
        else
        {
            y_origin = 6.5f;
            if (transform.position.y > y_origin) transform.position = new Vector3(transform.position.x, y_origin, z_origin);
        }
        if (menu.activeSelf)
        {
            float xPos = system.selectedInfo.gameObject.transform.position.x;
            transform.position = new Vector3(xPos, y_origin, z_origin);
            speed = 0;
            return;
        }
        float yPos = Input.mousePosition.y;
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
        if (transform.position.x <= 0.125f) transform.position = new Vector3(0.125f, y_origin, z_origin);
        else if (transform.position.x >= 13.875) transform.position = new Vector3(13.875f, y_origin, z_origin);
    }
}
