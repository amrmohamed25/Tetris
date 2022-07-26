using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void onCollisionEnter(Collision collisionInfo){
        Debug.Log(collisionInfo.collider.name);
    }
}
