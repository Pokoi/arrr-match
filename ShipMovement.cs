using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del barco
/// </summary>
public class ShipMovement : MonoBehaviour {

    #region Variables privadas

    /// <summary>
    /// Rigidbody de mi barco
    /// </summary>
    Rigidbody2D myRb;

    /// <summary>
    /// Velocidad de movimiento del barco
    /// </summary>
    float movementSpeed;

    /// <summary>
    /// Transform del barco
    /// </summary>
    Transform tr;

    #endregion

    #region Variables públicas

    /// <summary>
    /// Indica si puede moverse o si está en reserva
    /// </summary>
    public bool movementAllowed;

    #endregion

    /// <summary>
    /// Inicializamos las variables
    /// </summary>
    private void Awake() {
        myRb = GetComponent<Rigidbody2D>();
        tr = transform;
        movementAllowed = false;
    }

    /// <summary>
    /// Le asignamos una velocidad random dentro de la horquilla de velocidades
    /// </summary>
    void Start () {
        movementSpeed = Random.Range(VariablesManager.Instance.minMovementSpeed, VariablesManager.Instance.maxMovementSpeed);
	}
	
    /// <summary>
    /// Si su movimiento está permitido, se le aplica la velocidad al barco
    /// </summary>
	void Update () {        
        if(movementAllowed) myRb.velocity = new Vector2(-movementSpeed, myRb.velocity.y);
	}

    /// <summary>
    /// Envía al objeto a su pool y restaura su salud
    /// </summary>
    public void setToPool()
    {
        movementAllowed = false;
        myRb.velocity = new Vector2(0,0);
        GetComponent<HealthController>().RestoreHealh();
        tr.position = VariablesManager.Instance.objectPoolingShips.position;        
    }


}
