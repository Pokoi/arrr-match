using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador de la vida de los barcos
/// </summary>
public class HealthController : MonoBehaviour {

    #region Variables

    /// <summary>
    /// Vida máxima de los barcos
    /// </summary>
    byte maxHealth;

    /// <summary>
    /// Vida actual del barco
    /// </summary>
    byte currentHealth;

    #endregion

    /// <summary>
    /// Inicialización de las variables
    /// </summary>
    private void Awake()
    {
        currentHealth = maxHealth = VariablesManager.Instance.maxHealth;
    }

    /// <summary>
    /// Controlador del daño que recibe el barco
    /// Al llegar su vida a cero se envía el barco al pool y se suman los puntos 
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(byte damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            PuntuacionManager.Instance.setPuntos();
            GetComponent<ShipMovement>().setToPool();
        }        
    }

    /// <summary>
    /// Restaura la vida del barco a su vida total
    /// </summary>
    public void RestoreHealh()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Devuelve la vida actual del barco
    /// </summary>
    /// <returns></returns>
    public float getHealth()
    {
        return currentHealth;
    }
}
