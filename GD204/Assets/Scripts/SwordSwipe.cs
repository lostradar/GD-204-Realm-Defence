using UnityEngine;

public class SwordSwipe : MonoBehaviour
{
    public float swipeSpeed = 720f;
    public float startAngle = -90f;
    public float endAngle = 90f;

    private float currentAngle;
    private bool goingToEnd = true;

    void Start()
    {
        currentAngle = startAngle;
        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void Update()
    {
        float targetAngle = goingToEnd ? endAngle : startAngle;

        currentAngle = Mathf.MoveTowards(
            currentAngle,
            targetAngle,
            swipeSpeed * Time.deltaTime
        );

        if (Mathf.Approximately(currentAngle, targetAngle))
        {
            goingToEnd = !goingToEnd;
        }

        transform.localRotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
