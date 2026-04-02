using UnityEngine;

public class SwordSwipe : MonoBehaviour
{
    public float swipeSpeed = 720f;
    public float startAngle = -90f;
    public float endAngle = 90f;

    private float currentAngle;
    private bool goingForward = true;

    void Start()
    {
        currentAngle = startAngle;
    }

    void Update()
    {
        if (goingForward)
        {
            currentAngle += swipeSpeed * Time.deltaTime;

            if (currentAngle >= endAngle)
            {
                currentAngle = endAngle;
                goingForward = false;
            }
        }
        else
        {
            currentAngle -= swipeSpeed * Time.deltaTime;

            if (currentAngle <= startAngle)
            {
                currentAngle = startAngle;
                goingForward = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
