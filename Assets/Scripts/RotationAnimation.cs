using UnityEngine;

public class RotationAnimation : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    private float rotationRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rotationRemaining = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Perform flipping of the tile
        if (rotationRemaining > 0)
        {
            if (rotationRemaining - rotationSpeed * Time.deltaTime < 0)
            {
                // If rotation overshoots then only rotate until the 0.
                transform.eulerAngles += Vector3.up * rotationRemaining;
                rotationRemaining = 0;
            }
            else
            {
                transform.eulerAngles += Vector3.up * rotationSpeed * Time.deltaTime;
                rotationRemaining -= rotationSpeed * Time.deltaTime;
            }
        }
    }

    public void Rotate180()
    {
        // Do the flipping animation
        rotationRemaining += 180.0f;
    }
}
