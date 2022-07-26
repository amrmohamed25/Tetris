using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] objectPrefabArray;
    public AudioSource myAudio;
    private TetrisShape trackedShape;
    public List<TetrisShape> allObjects;
    // Start is called before the first frame update
    void Start()
    {
        // myObjects.Add()

        // Debug.Log(objectPrefabArray.Length);
        if (objectPrefabArray.Length > 0)
        {
            int rand = Random.Range(0, objectPrefabArray.Length);
            trackedShape = Instantiate(objectPrefabArray[rand], transform.position, Quaternion.identity).GetComponent<TetrisShape>();
            trackedShape.myGeneratorObject = this;
            addNewTetrisShape(trackedShape);

        }
    }

    public void playAudio(){
        myAudio.Play();
    }
    public void addNewTetrisShape(TetrisShape myShape)
    {
        allObjects.Add(myShape);
    }


    public void Generate()
    {
        int rand = Random.Range(0, objectPrefabArray.Length);
        trackedShape = Instantiate(objectPrefabArray[rand], transform.position, Quaternion.identity).GetComponent<TetrisShape>();
        trackedShape.GetComponent<TetrisShape>().myGeneratorObject = this;
        allObjects.Add(trackedShape);
        
        // Debug.Log("Generating New Shape");
    }
}
