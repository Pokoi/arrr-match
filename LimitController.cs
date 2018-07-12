using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador de las colisiones con los límites del mundo
/// </summary>
public class LimitController : MonoBehaviour {

    /// <summary>
    /// Dependiendo de qué es lo que ha colisionado con el límite lo envía a su pool correspondiente
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bow"))
        {
            collision.transform.parent.GetComponent<ShipMovement>().setToPool();
        }

        if (collision.gameObject.CompareTag("bullet"))
        {
            collision.transform.GetComponent<BulletMovement>().setToPool();
        }
    }
}
