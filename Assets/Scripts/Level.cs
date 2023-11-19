using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> gameObjects;

    public void StartCounting()
    {
        foreach(GameObject gameObject in gameObjects)
        {
            if (!gameObject.GetComponent<Ball>().isClicked) return;
        }

        GameManager.Instance.startTimer = true;
    }

    public void Reset()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.GetComponent<Ball>().ResetPos();
        }
    }

    public void Stop()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.GetComponent<Ball>().isClicked = false;
        }
    }

    public static List<GameObject> GetAllChilds(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}
