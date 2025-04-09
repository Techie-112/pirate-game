using System.Collections.Generic;
using UnityEngine;

public class tentacle : MonoBehaviour
{
    //tentbones is just short for tentacle bones
    //aka the white thingies you see when you click on the tentacle object
    [SerializeField]private List<GameObject> tentBones;

    private void Start()
    {
        createJoints();
    }
    private void createJoints()
    {
        tentBones = new List<GameObject>();
        getBones(transform);

        Rigidbody2D lastBone = null;

        for (int i = 0; i < tentBones.Count; i++)
        {
            //add colliders to detect if the player gets hit by the tentacle
            tentBones[i].AddComponent<BoxCollider2D>();
            //resize so the colliders are the sameish size as the width of the tentacle
            tentBones[i].GetComponent<BoxCollider2D>().size = new Vector2(.05f, .05f);

            //add hinge joints so the tentacle moves like a tentacle. i guess?
            //icl i think i wasted my time with this but oh well.
            tentBones[i].AddComponent<HingeJoint2D>();

            //connect the bones to each other
            if (lastBone != null)
                tentBones[i].GetComponent<HingeJoint2D>().connectedBody = lastBone;

            lastBone = tentBones[i].GetComponent<Rigidbody2D>();

        }
    }

    private void getBones(Transform bones)
    {
        //get all the bones in a list
        tentBones.Add(bones.gameObject);
        //get the transform position of all the bones
        //they're all nested in each other hence the child
        foreach (Transform child in bones)
        { getBones(child); }
    }
}
