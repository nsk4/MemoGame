using UnityEngine;

/// <summary>
/// Object spin and move animation.
/// </summary>
public class SpinMoveAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationCount;

    private float rotationRemaining;
    private bool alreadySwapped;
    private Vector3 destination;

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
                transform.eulerAngles += Vector3.forward * rotationRemaining;
                rotationRemaining = 0;
            }
            else
            {
                transform.eulerAngles += rotationSpeed * Time.deltaTime * Vector3.forward;
                rotationRemaining -= rotationSpeed * Time.deltaTime;
            }

            if (!alreadySwapped && rotationRemaining < 360.0f * rotationCount / 2f)
            {
                alreadySwapped = true;
                transform.position = destination;

            }
        }
    }

    /// <summary>
    /// Start the spin move followed by movign the object to the destination.
    /// </summary>
    /// <param name="destination">Destination to move to</param>
    public void SpinMove(Vector3 destination)
    {
        rotationRemaining += 360.0f * rotationCount;
        alreadySwapped = false;
        this.destination = destination;
    }
}
