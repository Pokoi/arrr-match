using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador del movimiento del cañón
/// </summary>
public class CanonMovement : MonoBehaviour {
    
    #region Variables privadas

    /// <summary>
    /// Posición en el mundo del lugar en la pantalla en la que el usuario ha hecho click
    /// </summary>
    Vector3 posicionClickada;
    
    /// <summary>
    /// Transform del cañón
    /// </summary>
    Transform tr;

    #endregion

    /// <summary>
    /// Inicializamos el transform del cáñón
    /// </summary>
    void Start () {
         tr = transform;
    }
	    
    /// <summary>
    /// Cuando el usuario hace click derecho del ratón y hay disparos disponibles, 
    /// se calcula la posición en el mundo equivalente a la posición en pantalla donde ha hecho click el usuario y
    /// aplicamos la rotación de l cañón hacia esa posición
    /// </summary>
	void Update () {

        // Cuando el usuario hace click derecho del ratón y hay disparos disponibles
        if (Input.GetMouseButtonDown(1) && GameManager.Instance.disparosRestantes != 0)
        {
            //Se calcula la posición en el mundo equivalente a la posición en pantalla donde ha hecho click el usuario
            posicionClickada = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, tr.position.z - Camera.main.transform.position.z));

            //Aplicamos la rotación del cañón hacia esa posición
            tr.LookAt(posicionClickada, transform.up);
        }

	}
}
