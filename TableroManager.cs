using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Controlador del Tablero. 
/// Gestiona la creación del tablero y la contabilización de cartas restantes
/// </summary>
public class TableroManager : MonoBehaviour
{

    #region Variables

    #region Variables serializadas

    [Tooltip("Carta a instanciar")]
    /// <summary>
    /// Prefab de la carta a instanciar
    /// </summary>
    [SerializeField]
    GameObject prefabCarta;

    [Tooltip("Distancia vertical entre las filas de cartas")]
    /// <summary>
    /// Distancia vertical entre las filas de cartas
    /// </summary>
    [SerializeField]
    float distanciaVertical = 1.1f;

    #endregion

    #region Variables privadas

    /// <summary>
    /// Número de cartas que quedan por levantar
    /// </summary>
    byte cartasRestantes = 0;

    /// <summary>
    /// Vector con la posición de la anterior carta instanciada
    /// </summary>
    Vector3 ultimaPosicion;

    /// <summary>
    /// Número de filas, en función del tamaño escogido por el usuario
    /// </summary>
    byte filas;

    /// <summary>
    /// Número de columnas, en función tamaño escogido por el usuario
    /// </summary>
    byte columnas;

    /// <summary>
    /// Posicion inicial dependiendo del tamaño escogido por el usuario
    /// </summary>
    Transform inicioEasy, inicioMedium, inicioDifficult;

    /// <summary>
    /// Primera carta levantanda por el usuario
    /// </summary>
    cartaManager primeraCarta;

    /// <summary>
    /// Segunda carta levantada por el usuario
    /// </summary>
    cartaManager segundaCarta;

    /// <summary>
    /// Variable que lleva el recuento de las cartas que se han levantado 
    /// </summary>
    byte contadorCartasLevantadas = 0;
       
    #endregion

    #endregion

    #region Constructor

    //Constructor
    private void Awake()
    {
        //Inicialización de las posiciones iniciales por la jerarquía
        inicioEasy = transform.GetChild(0).transform;
        inicioMedium = transform.GetChild(1).transform;
        inicioDifficult = transform.GetChild(2).transform;
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    /// Generación del tablero siguiendo el tamaño escogido por el usuario
    /// </summary>
    /// <param name="numero"> Número de filas que desea el usuario </param>
    public void GenerarTablero()
    {
        //Reseteamos el valor de las cartas restantes
        cartasRestantes = 0;

        //Recorro las filas
        for (int index = 0; index < filas; index++)
        {
            //Establezco la posición del primer elemento de esta fila
            if (columnas == 8) ultimaPosicion = PosicionInicial(index, inicioEasy.position);
            else if (columnas == 12) ultimaPosicion = PosicionInicial(index, inicioMedium.position);
            else if (columnas == 15) ultimaPosicion = PosicionInicial(index, inicioDifficult.position);

            //Recorro las columnas de esta fila
            for (int index2 = 0; index2 < columnas; index2++)
            {
                //Instanciamos la carta
                GameObject instancia = Instantiate(prefabCarta, ultimaPosicion, Quaternion.identity);

                //Asignamos un nuevo valor a la posición usada para las instancias. Nuevo valor en función del tamaño de su collider
                ultimaPosicion = new Vector3(instancia.transform.position.x + instancia.transform.GetComponent<Collider>().bounds.size.x, instancia.transform.position.y, instancia.transform.position.z);

                //Para organizar la jerarquía establezco la carta instanciada como hija del tablero
                instancia.transform.parent = transform;

                //Aumento el recuento de cartas
                cartasRestantes++;
            }
        }
    }

    /// <summary>
    /// Método que controla las cartas que se han levantado
    /// </summary>
    /// <param name="carta">Carta que ha pulsado el usuario</param>
    public void LevantarCarta(cartaManager carta)
    {
        if (contadorCartasLevantadas == 0) primeraCarta = carta;
        else segundaCarta = carta;
        contadorCartasLevantadas++;

        if (contadorCartasLevantadas == 2)
        {
            Invoke("CompareMatch", 0.7f);
            contadorCartasLevantadas = 0;                       
        }
    }

    /// <summary>
    /// Método que restaura el valor del contador de las cartas levantadas
    /// </summary>
    public void RestoreContadorCartasLevantadas()
    {
        contadorCartasLevantadas = 0;
    }

    /// <summary>
    /// Método llamado por el GameManager si el match ha sido correcto
    /// </summary>
    public void MatchCorrecto()
    {
        primeraCarta.Acierto();
        segundaCarta.Acierto();
        cartasRestantes -= 2;
        if (cartasRestantes == 0) GameManager.Instance.Final();
        
    }

    /// <summary>
    /// Método llamado por el GameManager si el match ha sido incorrecto
    /// </summary>
    public void MatchIncorrecto()
    {
        primeraCarta.Fallo();
        segundaCarta.Fallo();
    }

    /// <summary>
    /// Getter de la cantidad de cartas restantes
    /// </summary>
    /// <returns></returns>
    public byte getCartasRestantes()
    {
        return cartasRestantes;
    }

    /// <summary>
    /// Asignación de los valores de filas y columnas del tablero fácil. 
    /// Se invoca desde su botón
    /// </summary>
    public void setSizeEasy()
    {
        filas = 3;
        columnas = 8;
    }

    /// <summary>
    /// Asignación de los valores de filas y columnas del tablero medio. 
    /// Se invoca desde su botón
    /// </summary>
    public void setSizeMedium()
    {
        filas = 4;
        columnas = 12;
    }

    /// <summary>
    /// Asignación de los valores de filas y columnas del tablero difícil. 
    /// Se invoca desde su botón
    /// </summary>
    public void setSizeDifficult()
    {
        filas = 4;
        columnas = 15;
    }

    /// <summary>
    /// Método getter que devuelve el tamaño del tablero en función de las columnas
    /// </summary>
    /// <returns>Devuelve el número de columnas del tablero</returns>
    public byte getSize()
    {
        return columnas;
    }

    #endregion

    #region Métodos privados

    /// <summary>
    /// Establece la posición del primer elemento
    /// </summary>
    /// <param name="numero"> Fila que estoy recorriendo </param>
    /// <param name="vector"> Posición del primer elemento de todo el tablero</param>
    /// <returns></returns>
    private Vector3 PosicionInicial(int numero, Vector3 vector)
    {
        return (vector - (numero * new Vector3(0, distanciaVertical, 0)));
    }

    /// <summary>
    /// Método invocado desde Levantar Carta pasados 0.7 segundos para que dé tiempo a ver el anverso de la carta
    /// </summary>
    private void CompareMatch()
    {
        GameManager.Instance.CompareMatch(primeraCarta, segundaCarta);
    }


    #endregion

}
