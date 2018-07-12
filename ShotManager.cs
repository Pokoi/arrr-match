using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador del disparo
/// </summary>
public class ShotManager : MonoBehaviour {

    #region Variables privadas

    /// <summary>
    /// Prefab de la bala que vamos a disparar
    /// </summary>
    GameObject prefabBullet;

    /// <summary>
    /// Transform del punto de spawn de los proyectiles
    /// </summary>
    Transform spawnProyectiles;

    /// <summary>
    /// Transform del object pool de proyectiles
    /// </summary>
    Transform objectPoolingBullets;

    /// <summary>
    /// Cantidad de proyectiles a spawnear
    /// </summary>
    byte bulletsCount;

    /// <summary>
    /// Lista de los barcos instanciados
    /// </summary>
    List<GameObject> bullets = new List<GameObject>();

    /// <summary>
    /// Transform del padre de los proyectiles. Sirve para organizar la jerarquía
    /// </summary>
    Transform bulletParent;

    #endregion

    /// <summary>
    /// Inicializamos las variables
    /// </summary>
    private void Awake() {

        prefabBullet = VariablesManager.Instance.prefabBullet;
        spawnProyectiles = transform.GetChild(0).transform;
        bulletsCount = VariablesManager.Instance.bulletsCount;
        objectPoolingBullets = VariablesManager.Instance.objectPoolingBullets;
        bulletParent = VariablesManager.Instance.bulletsParent;
    }

    /// <summary>
    /// Crea la instancia de los proyectiles en base al número máximo de los proyectiles
    /// </summary>
    void Start () {

        //Crea la instancia de los proyectiles en base al número máximo de los proyectiles
        for (int index = 0; index < bulletsCount; index++)
        {
            GameObject instancia = Instantiate(prefabBullet, objectPoolingBullets.position, Quaternion.identity);
            instancia.transform.SetParent(bulletParent);
            bullets.Add(instancia);
        }

        GameManager.Instance.disparosRestantes = 0;

    }

    /// <summary>
    /// Cuando el usuario hace click derecho del ratón y quedan disparos restantes.
    /// Buscamos entre todas las balas aquella que esté en el pool
    /// Y colocamos esa bala en el punto de spawn de los proyectiles y activamos su movimiento.
    /// Reducimos el número de disparos restantes
    /// </summary>
    void LateUpdate () {

        // Cuando el usuario hace click derecho del ratón y quedan disparos restantes
        if (Input.GetMouseButtonDown(1) && GameManager.Instance.disparosRestantes!= 0) {

            byte index;

            //Buscamos entre todas las balas aquella que esté en el pool
            for (index = 0; index < bullets.Count && bullets[index].GetComponent<BulletMovement>().onAir; index++) ;

            //Y colocamos esa bala en el punto de spawn de los proyectiles y activamos su movimiento.
            //Reducimos el número de disparos restantes
            if (index < bullets.Count)
            {
                bullets[index].transform.position = spawnProyectiles.position;
                bullets[index].transform.eulerAngles = transform.eulerAngles;
                bullets[index].GetComponent<BulletMovement>().ActivateBullet();
                GameManager.Instance.disparosRestantes--;
            }
        }
    }
}
