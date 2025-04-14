using UnityEngine;

public class pusherBox_controller : MonoBehaviour
{
    public Transform target;
    public float fRadius = .5f;
    private Transform pivot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pivot = new GameObject().transform;
        transform.parent = pivot;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v3Pos = Camera.main.WorldToScreenPoint(target.position);
        v3Pos = Input.mousePosition - v3Pos;
        float angle = Mathf.Atan2(v3Pos.y, v3Pos.x) * Mathf.Rad2Deg;
        v3Pos = Quaternion.AngleAxis(angle, Vector3.forward) * (Vector3.right * fRadius);
        transform.position = target.position + v3Pos;

    }
}