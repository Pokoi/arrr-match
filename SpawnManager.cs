using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador del spawn de barcos
/// </summary>
public class SpawnManager : MonoBehaviour {

    #region Variables privadas

    /// <summary>
    /// Prefab del barco que vamos a crear
    /// </summary>
    GameObject prefabBarco;

    /// <summary>
    /// Box Collider 2D del punto de spawn
    /// </summary>
    BoxCollider2D myColl;

    /// <summary>
    /// Cantidad de barcos a spawnear
    /// </summary>
    byte shipCount;

    /// <summary>
    /// Transform del object pool de barcos
    /// </summary>
    Transform objectPoolingShips;

    /// <summary>
    /// Segundos entre cada invoke
    /// </summary>
    float invokeRate;

    /// <summary>
    /// Transform del padre de los barcos. Sirve para organizar la jerarquía
    /// </summary>
    Transform shipParent;

    #endregion

    #region Variables públicas

    /// <summary>
    /// Lista de los barcos instanciados
    /// </summary>
    public List<GameObject> ships = new List<GameObject>();

    #endregion
   

    /// <summary>
    /// Inicializamos las variables
    /// </summary>
    private void Awake() {

        prefabBarco = VariablesManager.Instance.prefabBarco;
        myColl = GetComponent<BoxCollider2D>();
        shipCount = VariablesManager.Instance.shipCount;
        objectPoolingShips = VariablesManager.Instance.objectPoolingShips;
        shipParent = VariablesManager.Instance.shipsParent;
    }


    /// <summary>
    /// Crea la instancia de los barcos en base al número máximo de los barcos
    /// Calcula un intervalo random entre cada spawn de barco
    /// Cada dicho intervalo de tiempo hace spawn un barco
    /// </summary>
    void Start () {

        //Crea la instancia de los barcos en base al número máximo de los barcos
        for (int index = 0; index < shipCount; index++)
        {
            GameObject instancia = Instantiate(prefabBarco, objectPoolingShips.position, Quaternion.Euler(transform.eulerAngles.x, -90, transform.eulerAngles.z));
            instancia.transform.SetParent(shipParent);
            ships.Add(instancia);
        }

        //Calcula un intervalo random entre cada spawn de barco
        invokeRate = Random.Range(VariablesManager.Instance.minTime, VariablesManager.Instance.maxTime);

        //Cada dicho intervalo de tiempo hace spawn un barco
        InvokeRepeating("SetPositionShip", 0, invokeRate);
    }
	

    /// <summary>
    /// Calcula una posición random dentro de los límites del collider del spawn teniendo en cuenta su centro y su posición en el mundo
    /// </summary>
    /// <returns></returns>
    Vector2 CalculatePosition()
    {
        return new Vector2(Random.Range(-myColl.bounds.extents.x, myColl.bounds.extents.x) + myColl.offset.x + transform.position.x, 
                           Random.Range(-myColl.bounds.extents.y, myColl.bounds.extents.y) + myColl.offset.y + transform.position.y);
    }

    /// <summary>
    /// Establece la posición del barco al ser llamado.
    /// Se calcula un nuevo intervalo random entre cada spwan
    /// </summary>
    void SetPositionShip()
    {
        byte index;
        for (index = 0; index < ships.Count && ships[index].GetComponent<ShipMovement>().movementAllowed; index++);
        if (index < ships.Count)
        {
            ships[index].transform.position = CalculatePosition();
            ships[index].GetComponent<ShipMovement>().movementAllowed = true;
        }

        //Se calcula un nuevo intervalo random entre cada spawn
        invokeRate = Random.Range(VariablesManager.Instance.minTime, VariablesManager.Instance.maxTime);
    }
}
