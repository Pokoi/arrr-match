using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que controla las colisiones de los proyectiles
/// </summary>
public class BulletCollisions : MonoBehaviour {


    /// <summary>
    /// Se comprueba contra qué parte del barco ha colisionado y en base a esto se le aplica un daño u otro. 
    /// Después se devuelve el proyectil a su pool
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Se comprueba contra qué parte del barco ha colisionado y en base a esto se le aplica un daño u otro. 
        if (other.gameObject.CompareTag("bow"))
        {
            other.transform.parent.GetComponent<HealthController>().SetDamage(VariablesManager.Instance.bowDamage);            
        }
        else if (other.gameObject.CompareTag("stern"))
        {
            other.transform.parent.GetComponent<HealthController>().SetDamage(VariablesManager.Instance.sternDamage);            
        }

        //Se devuelve el proyectil a su pool
        GetComponent<BulletMovement>().setToPool();
    }
    
}
