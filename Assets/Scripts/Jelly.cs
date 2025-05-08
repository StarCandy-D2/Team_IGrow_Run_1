using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    public Animator animator;
    int score = 0;
    bool hasEaten = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasEaten)
        {
            hasEaten = true;
            Debug.Log(++score);
            Debug.Log("Á©¸® ¸Ô±â");
            animator.SetBool("IsEat", true);

            Destroy(this.gameObject, 0.2f);
        }

    }
}
