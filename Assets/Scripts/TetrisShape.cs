













using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
public class TetrisShape : MonoBehaviour
{
    public Generator myGeneratorObject;
    public int isFrozen = 0;
    public GameObject parentObject;
    // private float currentRot=0;
    private float ElapsedTime = 0.0f;
    private bool isStatic = false;
    public List<Rigidbody> childrenObjects;
    // Start is called before the first frame update
    //available x axis=[21,22.05,23.1,24.15,19.95,18.9,16.8,15.75]
    //available y axis=[10,9,8,7,6,5,4,3,2,1,0] 

    private List<string> xAxis = new List<string>();
    private List<float> yAxis = new List<float>();
    void Start()
    {
    }



    void checkChildren()
    {

        if (childrenObjects.Count == 0)
        {
            GameObject myObject2 = this.gameObject;
            Destroy(myObject2);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Profiler.BeginSample("Hello");
        checkChildren();
        if (isStatic == false)
        {
            if (checkLowestPoint() || !checkDown())
            {


                // if (isStatic == false)
                // {
                for (int i = 0; i < childrenObjects.Count; i++)
                {
                    float temp = (float)childrenObjects[i].transform.position.y;
                    if (!yAxis.Contains(temp))
                    {
                        yAxis.Add(temp);
                    }
                }
                checkRows();
                (myGeneratorObject.GetComponent<Generator>() as Generator).Generate();
                isStatic = true;
                // 
                // }
            }
            else
            {
                ElapsedTime = ElapsedTime + Time.deltaTime;
                if (ElapsedTime >= 1.0f)
                {
                    transform.Translate(0, -1.05f, 0, Space.World);
                    ElapsedTime = 0.0f;
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    parentObject.transform.Rotate(0.0f, 0.0f, 90.0f, Space.World);
                    if (!checkRotation())
                    {
                        parentObject.transform.Rotate(0.0f, 0.0f, -90.0f, Space.World);
                    }
                }

                if (Input.GetKeyDown(KeyCode.D) && checkBoundaries(true))
                {
                    // Debug.Log(transform.position.x);
                    if (checkRight())
                    {
                        transform.Translate(1.05f, 0, 0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.Translate(0, -1.05f, 0);
                }
                // Debug.Log(transform.position.x);
                if (Input.GetKeyDown(KeyCode.A) && checkBoundaries(false))
                {
                    if (checkLeft())
                    {
                        transform.Translate(-1.05f, 0, 0);
                    }
                }
            }
        }
        Profiler.EndSample();
    }


    private bool checkLowestPoint()
    {
        Profiler.BeginSample("Lowest");
        float lowestPoint = 200.0f;
        for (int i = 0; i < childrenObjects.Count; i++)
        {
            if (lowestPoint > childrenObjects[i].transform.position.y)
            {
                lowestPoint = childrenObjects[i].transform.position.y;
            }
        }
        if (lowestPoint <= 1)
        {
            Profiler.EndSample();
            return true;
        }
        Profiler.EndSample();
        return false;
    }
    private bool checkRotation()
    {
        for (int i = 0; i < childrenObjects.Count; i++)
        {
            if (childrenObjects[i].transform.position.x > 26 || childrenObjects[i].transform.position.x < 16.5)
                return false;
        }
        return true;
    }
    private bool checkBoundaries(bool flag)
    {

        for (int i = 0; i < childrenObjects.Count; i++)
        {
            if ((childrenObjects[i].transform.position.x + 1.05 > 26)&&flag || (childrenObjects[i].transform.position.x - 1.05 < 14.8f)&&!flag)
                return false;
        }
        return true;
    }
    private void checkRows()
    {
        Profiler.BeginSample("Check Rows");
        List<TetrisShape> myList = myGeneratorObject.allObjects;
        List<Rigidbody> objectsReadyToDelete = new List<Rigidbody>();
        for (int i = 0; i < yAxis.Count; i++)
        {
            objectsReadyToDelete.Clear();
            for (int j = 0; j < myList.Count; j++)
            {
                for (int k = 0; k < myList[j].childrenObjects.Count; k++)
                {

                    if (!xAxis.Contains(myList[j].childrenObjects[k].transform.position.x.ToString()) && Mathf.Abs(yAxis[i] - myList[j].childrenObjects[k].transform.position.y) <= 0.2f)
                    {

                        xAxis.Add(myList[j].childrenObjects[k].transform.position.x.ToString());
                        objectsReadyToDelete.Add(myList[j].childrenObjects[k]);
                    }
                }

            }
            if (xAxis.Count == 10)
            {
                (myGeneratorObject.GetComponent<Generator>() as Generator).playAudio();
                float currentY = 0;
                for (int j = 0; j < objectsReadyToDelete.Count; j++)
                {
                    for (int k = 0; k < myList.Count; k++)
                    {
                        for (int m = 0; m < myList[k].childrenObjects.Count; m++)
                        {
                            if (Mathf.Abs(objectsReadyToDelete[j].transform.position.x - myList[k].childrenObjects[m].transform.position.x) <= 0.2
                             && Mathf.Abs(objectsReadyToDelete[j].transform.position.y - myList[k].childrenObjects[m].transform.position.y) <= 0.2
                            )
                            {
                                myList[k].childrenObjects.RemoveAt(m);

                                // break;
                            }
                        }

                    }
                    GameObject myObject = objectsReadyToDelete[j].gameObject;
                    currentY = objectsReadyToDelete[j].transform.position.y;
                    Destroy(myObject);

                }
                for (int k = 0; k < myList.Count; k++)
                {
                    for (int m = 0; m < myList[k].childrenObjects.Count; m++)
                    {
                        if (myList[k].childrenObjects[m].transform.position.y > currentY)
                        {
                            myList[k].childrenObjects[m].transform.Translate(0, -1.05f, 0, Space.World);
                        }

                    }
                }
            }
            // Debug.Log("Count=" + xAxis.Count);
            xAxis.Clear();
        }
        Profiler.EndSample();
    }

    public bool checkLeft()
    {
        Profiler.BeginSample("Check Left");
        for (int i = 0; i < childrenObjects.Count; i++)
        {
            for (int j = 0; j < myGeneratorObject.allObjects.Count - 1; j++)
            {
                for (int k = 0; k < myGeneratorObject.allObjects[j].childrenObjects.Count; k++)

                {
                    if ((childrenObjects[i].transform.position.x - myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.x <= 1.05f)
                    &&
                    childrenObjects[i].transform.position.x - myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.x > 0
                    &&
                    (childrenObjects[i].transform.position.y == myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.y))
                    {
                        Profiler.EndSample();
                        return false;
                    }
                }
            }
        }
        Profiler.EndSample();
        return true;
    }
    public bool checkRight()
    {
        Profiler.BeginSample("Check Right");
        for (int i = 0; i < childrenObjects.Count; i++)
        {
            for (int j = 0; j < myGeneratorObject.allObjects.Count - 1; j++)
            {
                for (int k = 0; k < myGeneratorObject.allObjects[j].childrenObjects.Count; k++)

                {
                    if ((childrenObjects[i].transform.position.x - myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.x >= -1.05f &&
                    childrenObjects[i].transform.position.x - myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.x < 0)
                    &&
                    (childrenObjects[i].transform.position.y == myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.y))
                    {
                        Profiler.EndSample();
                        return false;
                    }
                }
            }
        }
        Profiler.EndSample();
        return true;
    }
    public bool checkDown()
    {
        // Debug.Log("Ya rbbbb");
        Profiler.BeginSample("Check Down");
        for (int i = 0; i < childrenObjects.Count; i++)
        {
            for (int j = 0; j < myGeneratorObject.allObjects.Count - 1; j++)
            {
                for (int k = 0; k < myGeneratorObject.allObjects[j].childrenObjects.Count; k++)

                {
                    if (Mathf.Abs(childrenObjects[i].transform.position.y - myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.y) <= 1.05f
                    && Mathf.Abs(childrenObjects[i].transform.position.x - myGeneratorObject.allObjects[j].childrenObjects[k].transform.position.x) <= 0.1f
                    )
                    {
                        Profiler.EndSample();
                        return false;
                    }
                }
            }
        }
        return true;
    }

}
