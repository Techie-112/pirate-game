using UnityEngine;

public class waterboarding : MonoBehaviour
{
    public int spawnPoint;


    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, -.1f, 0);

        if (checkPos() == true)
            { Destroy(this.gameObject); }
    }

    bool checkPos()
    {
        bool offScreen = false;

        if(Mathf.Abs(transform.position.y) > 8.5)
        { offScreen = true; }
        else
        { offScreen = false; }

        return offScreen;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<puddle>())
        {
            Destroy(collision.gameObject);
        }
    }
}
