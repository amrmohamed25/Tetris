using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<TetrisShape> allObjects;
    void Start(){
        
    }
    public void addNewTetrisShape(TetrisShape myShape)
    {
        allObjects.Add(myShape);
    }
}
