using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del HUD del combate
/// </summary>
public class HUDManager : MonoBehaviour {

    #region Variables privadas

    /// <summary>
    /// Prefab de la barra de vida
    /// </summary>
    GameObject prefabBarra;

    /// <summary>
    /// Cantidad de barcos
    /// </summary>
    byte shipCount;

    /// <summary>
    /// Lista de los barcos instanciados
    /// </summary>
    List<GameObject> bars = new List<GameObject>();

    /// <summary>
    /// Spawn manager
    /// </summary>
    SpawnManager spawnManager;

    /// <summary>
    /// Diferencia de posición en Y entre la barra de vida y el barco
    /// </summary>
    float BarsYDifference;

    /// <summary>
    /// Texto depuración del tiempo
    /// </summary>
    Text timeText;

    /// <summary>
    /// Texto depuración de los tiros faltantes
    /// </summary>
    Text shotText;

    /// <summary>
    /// Texto depuración de los puntos actuales
    /// </summary>
    Text pointsText;

    #endregion

    #region Variables públicas

    /// <summary>
    /// Spawn de los barcos
    /// </summary>
    public GameObject spawn;

    #endregion

    
    /// <summary>
    /// Inicializamos las variables
    /// </summary>
    private void Awake()
    {
        prefabBarra = VariablesManager.Instance.prefabBarra;
        shipCount = VariablesManager.Instance.shipCount;
        spawnManager = spawn.GetComponent<SpawnManager>();
        BarsYDifference = VariablesManager.Instance.barsYDifference;
        timeText = VariablesManager.Instance.timeText;
        shotText = VariablesManager.Instance.shotText;
        pointsText = VariablesManager.Instance.pointsText;

    }

    /// <summary>
    /// Creamos las instancias de las barras de vida
    /// </summary>
    private void Start()
    {
        for (int index = 0; index < shipCount; index++)
        {
            GameObject instancia = Instantiate(prefabBarra, transform.position, Quaternion.identity);
            instancia.transform.SetParent(transform);
            bars.Add(instancia);
        }
    }

    /// <summary>
    /// Recorre todas las barras de vida y las coloca en la posición en pantalla equivalente a la posición del mundo de los barcos
    /// Actualiza la información de la vida de las barras en base a la vida de los barcos
    /// Se depura el tiempo restante, los disparos restantes y los puntos obtenidos
    /// </summary>
    void Update () {

        //Recorre todas las barras de vida y las coloca en la posición en pantalla equivalente a la posición del mundo de los barcos
        //Actualiza la información de la vida de las barras en base a la vida de los barcos
        for (int index = 0; index < shipCount; index++) { 
            bars[index].transform.position = Camera.main.WorldToScreenPoint(spawnManager.ships[index].transform.position);
            bars[index].transform.position = new Vector3(bars[index].transform.position.x, bars[index].transform.position.y + BarsYDifference, bars[index].transform.position.z);
            bars[index].transform.GetChild(1).GetComponent<Image>().fillAmount = spawnManager.ships[index].GetComponent<HealthController>().getHealth() / VariablesManager.Instance.maxHealth;
        }

        //Se depura el tiempo restante, los disparos restantes y los puntos obtenidos
        if (PuntuacionManager.Instance.getTime() > 0.01) timeText.text = PuntuacionManager.Instance.getTime().ToString();
        shotText.text = GameManager.Instance.disparosRestantes.ToString();
        pointsText.text = PuntuacionManager.Instance.getPuntos().ToString();

    }
}
