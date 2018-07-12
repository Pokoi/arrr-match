using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador de la puntuación. Maneja toda la fluctuación de puntos a lo largo de la partida.
/// Controla también el tiempo.
/// </summary>
public class PuntuacionManager : MonoBehaviour {

    #region Variables
    
    /// <summary>
    /// Cómputo de puntos totales 
    /// </summary>
    int puntuacionActual;

    /// <summary>
    /// Cantidad de puntos que suma un acierto
    /// </summary>
    byte puntosAcierto = 5;

    /// <summary>
    /// Tiempo total. Depende del tamaño del tablero.
    /// </summary>
    float tiempoEnSegundos;

    /// <summary>
    /// Segundos restantes que quedan.
    /// </summary>
    float tiempoActual;

    #endregion
    
    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static PuntuacionManager instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static PuntuacionManager Instance
    {
        get { return instance; }
    }

    #endregion
    
    #region Métodos públicos


    /// <summary>
    /// Método getter del tiempo restante
    /// </summary>
    /// <returns> Segundos restantes </returns>
    public float getTime()
    {
        return tiempoActual;
    }

    /// <summary>
    /// Método getter de los puntos actuales
    /// </summary>
    /// <returns>Puntuación actual</returns>
    public int getPuntos()
    {
        return puntuacionActual;
    }

    /// <summary>
    /// Método que suma o resta puntos 
    /// Se llama al destruir un barco enemigo
    /// </summary>
    /// <param name="cantidad"> Cantidad de puntos a sumar o restar</param>
    public void setPuntos()
    {
        puntuacionActual += puntosAcierto;
    }

    /// <summary>
    /// Método que restaura el tiempo y lo iguala a su máximo
    /// Es llamado desde los items de la tienda al comprar el ron
    /// </summary>
    public void restoreTime()
    {
        tiempoActual = tiempoEnSegundos;
    }

    #endregion

    #region Métodos privados    

    /// <summary>
    /// Inicializamos instance
    /// </summary>
    void Awake()
    {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que sólo haya una instancia de esta clase        
    }

    /// <summary>
    /// Start. Se ejecuta en el primer frame.
    /// Establecemos el tiempo en segundos en función del tamaño del tablero.
    /// </summary>
    private void Start()
    {
        //Establecemos el tiempo en segundos en función del tamaño del tablero.
        //Si es pequeño o mediano el tiempo es de 90 segundos
        //Si es grande (dificultad difícil), el tiempo es de 60 segundos

        if (GetComponent<TableroManager>().getSize() < 15) tiempoEnSegundos = 120;
        else if (GetComponent<TableroManager>().getSize() == 15) tiempoEnSegundos = 90;

        //Establecemos el tiempo actual como el tiempo máximo
        tiempoActual = tiempoEnSegundos;

    }

    /// <summary>
    ///Establecemos el tiempo
    ///Si el tiempo llega a cero llamamos al método que controla el final de la partida
    /// </summary>
    private void Update()
    {    
        //Establecemos el tiempo
        tiempoActual -= Time.deltaTime;
        
        //Si el tiempo llega a cero llamamos al método que controla el final de la partida
        if (tiempoActual <= 0.01) GameManager.Instance.Final();        

    }


    #endregion

}
