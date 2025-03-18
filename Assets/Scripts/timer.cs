using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    [SerializeField]Image clockhand;

    [SerializeField]float sec;
    float rotationSpeed;

    private void Update()
    {
        //sets the clockhand to do one full rotation in X seconds
        rotationSpeed = 360f / sec;

        //vector3.back makes the clockhand rotate towards the -z axis
        //multiply by deltatime so that it rotates once per X seconds rather than once per X frames
        clockhand.transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);


    }
}
