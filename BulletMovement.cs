using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase que controla el movimiento de los proyectiles
/// </summary>
public class BulletMovement : MonoBehaviour {

    #region Variables 

    #region Variables públicas

    /// <summary>
    /// Indica si ha sido disparada o si está en reserva
    /// </summary>
    public bool onAir;

    #endregion

    #region Variables privadas

    /// <summary>
    /// Transform de mi proyectil
    /// </summary>
    Transform tr;

    /// <summary>
    /// Rigidbody de mi proyectil
    /// </summary>
    Rigidbody2D myRb;

    /// <summary>
    /// Velocidad de movimiento de la bala
    /// </summary>
    float movementSpeed;

    #endregion

    #endregion

    #region Métodos públicos
    
    /// <summary>
    /// Activa el proyectil
    /// </summary>
    public void ActivateBullet()
    {
        onAir = true;
    }

    /// <summary>
    /// Envía al objeto a su pool
    /// </summary>
    public void setToPool()
    {
        onAir = false;

        //Establecemos su velocidad a 0 para que no se mueva en la piscina
        myRb.velocity = new Vector2(0, 0);

        //Le ponemos en la posición del pool
        tr.position = VariablesManager.Instance.objectPoolingBullets.position;
    }

    #endregion

    #region Métodos privados

    /// <summary>
    /// Inicializamos las variables
    /// </summary>
    private void Awake()
    {
        tr = transform;
        myRb = tr.GetComponent<Rigidbody2D>();
        movementSpeed = VariablesManager.Instance.bulletMovementSpeed;
        onAir = false;
    }

    /// <summary>
    /// Si está en el aire (si ha sido disparada), se le aplica una velocidad al rigidbody
    /// </summary>
    private void Update()
    {
        //Si está en el aire (si ha sido disparada)
        if (onAir)
        {
            //Se le aplica una velocidad al rigidbody        
            myRb.velocity = new Vector2(transform.position.x * movementSpeed, transform.position.y * movementSpeed);
        }
    }

    #endregion
    
}
