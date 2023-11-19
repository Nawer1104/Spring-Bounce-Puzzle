using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform target;

    public Transform startPos;

    Transform targetTogo;

    public float speed;

    public bool isClicked = false;

    public float distance;

    public GameObject vfx;

    Vector3 resetPoint;

    private void Start()
    {
        targetTogo = target;

        resetPoint = transform.position;
    }

    private void OnMouseDown()
    {
        if (isClicked) return;

        isClicked = true;

        GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].StartCounting();
    }

    private void Update()
    {
        if (isClicked)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTogo.position, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Target"))
        {
            targetTogo = startPos;
        }
        else if(collision != null && collision.gameObject.CompareTag("Start")) 
        {
            targetTogo = target;
        }
        else if (collision != null && collision.gameObject.CompareTag("Ball"))
        {
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].Reset();
            GameManager.Instance.Pause();
        }
    }

    public void ResetPos()
    {
        GameObject vfxFail = Instantiate(vfx, transform.position, Quaternion.identity) as GameObject;
        Destroy(vfxFail, 1f);
        isClicked = false;
        transform.position = resetPoint;
    }
}
