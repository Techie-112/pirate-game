using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    public captain captain;

    private void OnEnable()
    {
        if (captain.anim.GetFloat("targX") > 0)
        {
            print("right");
        }
        else
        {
            print("left");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            captain.damagePlayer();
        }
        this.gameObject.SetActive(false);
    }
}
