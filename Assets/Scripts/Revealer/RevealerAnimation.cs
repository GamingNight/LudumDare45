using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealerAnimation : MonoBehaviour
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
            int waitTime = Random.Range(10, 25);
            yield return new WaitForSeconds(waitTime);
            animator.SetTrigger("Look");
            yield return null;
        }
    }
}
