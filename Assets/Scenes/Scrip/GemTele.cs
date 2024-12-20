using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemTele : MonoBehaviour
{
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController controller = collision.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.rb.position = new Vector3(x, y, z);
        }
    }
}
