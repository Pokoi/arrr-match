using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador de las físicas
/// </summary>
public class PhysicsManager : MonoBehaviour {

	/// <summary>
    /// Ignora las colisiones de las siguientes capas
    /// </summary>
	void Start () {
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(8, 9, true);
        Physics2D.IgnoreLayerCollision(8, 10, true);

    }	

}
