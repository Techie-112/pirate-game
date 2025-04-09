using UnityEngine;

public class waterboarding : MonoBehaviour
{
    Vector3 endpos;


    private void Start()
    {
        endpos = this.transform.position * -1;
        //print(endpos);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += move();

        if (checkPos())
            { Destroy(this.gameObject); }
    }

    Vector3 move()
    {
        Vector3 direction;
        if (endpos.y > 0)
            { direction = new Vector3(0, .1f, 0); }
        else if (endpos.y < 0)
            { direction = new Vector3(0, -.1f, 0); }
        else
            if (endpos.x > 0)
            { direction = new Vector3(.1f, 0, 0); }
            else
            { direction = new Vector3(-.1f, 0, 0); }

        return direction;

    }

    bool checkPos()
    {
        bool offScreen;
        if ((this.transform.position.y < 10 && this.transform.position.y > -10)
            || (this.transform.position.x < 5 && this.transform.position.x > -5))
        { offScreen = false; }
        else
        { offScreen = true; }

        return offScreen;
    }
}
