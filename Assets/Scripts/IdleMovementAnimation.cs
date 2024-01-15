using UnityEngine;

/// <summary>
/// Idle object movement animation.
/// </summary>
public class IdleMovementAnimation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float degrees;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float randomFactor;

    private float lastTime;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        lastTime = Mathf.Deg2Rad * 180 * (float)Random.Range(0, 2); // Randomize the starting direction
        offset = 0;
        speed += Random.Range(-speed * randomFactor, speed * randomFactor);
        degrees += Random.Range(-degrees * randomFactor, degrees * randomFactor);
    }

    // Update is called once per frame
    void Update()
    {
        float newTime = lastTime + Time.deltaTime;
        float relativeAngleDifference = Mathf.Sin(newTime * speed) - Mathf.Sin(lastTime * speed);
        float relativeRotation = relativeAngleDifference * degrees;
        if (Mathf.Abs(offset + relativeRotation) > degrees)
        {
            relativeRotation = Mathf.Sign(relativeRotation) * (degrees - Mathf.Abs(offset));
        }
        offset += relativeRotation;
        lastTime = newTime;
        transform.eulerAngles += direction * relativeRotation;
    }
}
