using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        
        animator = gameObject.GetComponent<Animator>();
        StartCoroutine(waiter());

    }

    IEnumerator waiter()
    {
        while (true)
        {
            int waitTime = Random.Range(5, 10);
            yield return new WaitForSeconds(waitTime);
            animator.SetTrigger("Blink");
            yield return null;
        }
    }
    
}
