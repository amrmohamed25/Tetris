using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public TetrisShape myShape;
    //available x axis=[21,22.05,23.1,24.15,19.95,18.9,16.8,15.75]
    //available y axis=[10,9,8,7,6,5,4,3,2,1,0] 
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log(myShape.childrenObjects[0].transform.position.x);
        // Debug.Log(myShape.myGeneratorObject.allObjects.Count);
        // if (checkCollision() == true && myShape.myGeneratorObject.allObjects.Count > 0)
        // {
            
            // myShape.isFrozen = 1;
        // }
        // checkRows();
    }
    private void checkRows()
    {
        // List<TetrisShape> myList = myShape.myGeneratorObject.allObjects;
        // for (int i = 0; i < myList.Count; i++)
        // {
        //     for (int j = 0; j < myList[i].childrenObjects.Count; j++)
        //     {

        //     }
        // }
    }
    private bool checkCollision()
    {
        bool isCollided = false;
        List<TetrisShape> myList = myShape.myGeneratorObject.allObjects;
        // bool flag=false;
        for (int i = 0; i < myShape.childrenObjects.Count; i++)
        {
            for (int j = 0; j < myList.Count - 1; j++)
            {
                for (int k = 0; k < myList[j].childrenObjects.Count; k++)
                {
                    // Debug.Log(Mathf.Abs(myShape.childrenObjects[i].transform.position.x));
                    // Debug.Log(myList[j].childrenObjects[k].transform.position.x);
                    // Debug.Log(Mathf.Abs(myShape.childrenObjects[i].transform.position.y));
                    // Debug.Log(myList[j].childrenObjects[k].transform.position.y);

                    if ((Mathf.Abs(myShape.childrenObjects[i].transform.position.x - myList[j].childrenObjects[k].transform.position.x) <=0.1)
                        &&
                        Mathf.Abs(myShape.childrenObjects[i].transform.position.y - myList[j].childrenObjects[k].transform.position.y) <= 1.05f)
                    {
                        return true;
                        // break;
                    }
                }
                // if (isCollided == true) { break; }
            }
            // if (isCollided == true) { break; }
        }
        return isCollided;
    }
}
