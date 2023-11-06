using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Fade : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private void Start()
    {;
        GameManager.Instance.Fade = this;
        animator = gameObject.GetComponent<Animator>();
    }

    public void Active(int input)
    {
        gameObject.SetActive(true);
        if (input == 0)
        {
            animator.SetBool("Active", true);
        }
        else
        {
            animator.SetBool("Active", false);
        }
        gameObject.SetActive(Convert.ToBoolean(input));
    }
}
