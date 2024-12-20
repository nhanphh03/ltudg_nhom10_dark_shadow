using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityF : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.rb.velocity = Vector3.zero;
        }
    }
}
