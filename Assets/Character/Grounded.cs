using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour
{
    public bool isGrounded;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>() && collision.gameObject.GetComponent<BoxCollider2D>().isTrigger)
        {
            return;
        }
        isGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxCollider2D>() &&collision.gameObject.GetComponent<BoxCollider2D>().isTrigger)
        {
            return;
        }
        isGrounded = false;
    }
}
