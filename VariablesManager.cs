using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase accesible desde el editor con el compendio de variables accesibles para el diseñador
/// </summary>
public class VariablesManager : MonoBehaviour {


    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static VariablesManager instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static VariablesManager Instance {
        get { return instance; }
    }

    #endregion

    #region Variables accesibles desde el editor

    #region Ships

    [Header("____[SHIPS]____")]
    [Space]

    [Header("Prefab del barco que vamos a crear")]
    /// <summary>
    /// Prefab del barco que vamos a crear
    /// </summary>
    public GameObject prefabBarco;

    [Header("Velocidad mínima con la que se mueve el barco")]
    /// <summary>
    /// Velocidad mínima con la que se mueve el barco
    /// </summary>
    public float minMovementSpeed;

    [Header("Velocidad máxima con la que se mueve el barco")]
    /// <summary>
    /// Velocidad máxima con la que se mueve el barco
    /// </summary>
    public float maxMovementSpeed;

    [Header("Cantidad de barcos a spawnear")]
    /// <summary>
    /// Cantidad de barcos a spawnear
    /// </summary>
    public byte shipCount;

    [Header("Transform del GameObject donde se mantienen los barcos en object pooling")]
    /// <summary>
    /// Transform del GameObject donde se mantienen los barcos en object pooling
    /// </summary>
    public Transform objectPoolingShips;

    [Header("Cantidad mínima de segundos entre cada invoke")]
    /// <summary>
    /// Cantidad mínima de segundos entre cada invoke
    /// </summary>
    public float minTime;

    [Header("Cantidad máxima de segundos entre cada invoke")]
    /// <summary>
    /// Cantidad máxima de segundos entre cada invoke
    /// </summary>
    public float maxTime;

    [Header("Transform del padre de los barcos. Sirve para organizar la jerarquía")]
    /// <summary>
    /// Transform del padre de los barcos. Sirve para organizar la jerarquía
    /// </summary>
    public Transform shipsParent;

    [Header("Vida máxima de los barcos")]
    /// <summary>
    /// Vida máxima de los barcos
    /// </summary>
    public byte maxHealth;

    [Header("Daño que recibe el barco al ser golpeado en la proa")]
    /// <summary>
    /// Daño que recibe el barco al ser golpeado en la proa
    /// </summary>
    public byte bowDamage;

    [Header("Daño que recibe el barco al ser golpeado en la popa")]
    /// <summary>
    /// Daño que recibe el barco al ser golpeado en la popa
    /// </summary>
    public byte sternDamage;

    #endregion

    #region Bullets

    [Space]
    [Header("____[BULLETS]____")]
    [Space]

    [Header("Prefab de la bala que vamos a disparar")]
    /// <summary>
    /// Prefab de la bala que vamos a disparar
    /// </summary>
    public GameObject prefabBullet;

    [Header("Velocidad del proyectil")]
    /// <summary>
    /// Velocidad del proyectil
    /// </summary>
    public float bulletMovementSpeed;

    [Header("Cantidad de proyectiles a spawnear")]
    /// <summary>
    /// Cantidad de proyectiles a spawnear
    /// </summary>
    public byte bulletsCount;
   
    [Header("Transform del GameObject donde se mantienen los proyectiles en object pooling")]
    /// <summary>
    /// Transform del GameObject donde se mantienen los proyectiles en object pooling
    /// </summary>
    public Transform objectPoolingBullets;   

    [Header("Transform del padre de los proyectiles. Sirve para organizar la jerarquía")]
    /// <summary>
    /// Transform del padre de los proyectiles. Sirve para organizar la jerarquía
    /// </summary>
    public Transform bulletsParent;

    [Header("Proyectiles que ganas al acertar")]
    /// <summary>
    /// Proyectiles que ganas al acertar
    /// </summary>
    public byte bulletsMatch;

    #endregion

    #region HUD

    [Space]
    [Header("____[HUD]____")]
    [Space]

    [Header("Prefab de la barra de vida")]
    /// <summary>
    /// Prefab de la barra de vida
    /// </summary>
    public GameObject prefabBarra;

    [Header("Diferencia de posición en Y entre la barra de vida y el barco")]
    /// <summary>
    /// Diferencia de posición en Y entre la barra de vida y el barco
    /// </summary>
    public float barsYDifference;

    [Header("Texto depuración del tiempo")]
    /// <summary>
    /// Texto depuración del tiempo
    /// </summary>
    public Text timeText;

    [Header("Texto depuración de los tiros faltantes")]
    /// <summary>
    /// Texto depuración de los tiros faltantes
    /// </summary>
    public Text shotText;

    [Header("Texto depuración de los puntos conseguidos")]
    /// <summary>
    /// Texto depuración de los puntos actuales
    /// </summary>
    public Text pointsText;

    #endregion

    #endregion

    /// <summary>
    /// Inicializamos instance
    /// </summary>
    void Awake() {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que sólo haya una instancia de esta clase        
    }


}
