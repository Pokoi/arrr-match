using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controlador del juego. Maneja la temática escogida por el usuario así como la asignación aleatoria de las cartas.
/// También controla si los emparejamientos del usuario son correctos.
/// </summary>
public class GameManager : MonoBehaviour
{

    #region Singleton

    /// <summary>
    /// Campo privado que referencia a esta instancia
    /// </summary>
    static GameManager instance;

    /// <summary>
    /// Propiedad pública que devuelve una referencia a esta instancia
    /// Pertenece a la clase, no a esta instancia
    /// Proporciona un punto de acceso global a esta instancia
    /// </summary>
    public static GameManager Instance
    {
        get { return instance; }
    }

    #endregion

    #region Variables 

    #region Variables serializadas
    [Tooltip("Colección de sprites de reversos")]
    /// <summary>
    /// Sprite del Reverso. Varía en función de la temática de juego escogida
    /// </summary>
    [SerializeField]
    List<Sprite> spriteReverso = new List<Sprite>();

    [Tooltip("Colección de sprites de la temática 1")]
    /// <summary>
    /// Sprite del anverso. Varía en función de la temática de juego escogida
    /// Tematica1
    /// </summary>
    [SerializeField]
    List<Sprite> spriteAnversoCapitales = new List<Sprite>();

    [Tooltip("Colección de sprites de la temática 2")]
    /// <summary>
    /// Sprite del anverso. Varía en función de la temática de juego escogida
    /// Tematica2
    /// </summary>
    [SerializeField]
    Sprite[] spriteAnversoEnfermedades;

    [Tooltip("Objeto que contiene el script del TableroManager")]
    /// <summary>
    /// Tablero del juego
    /// </summary>
    [SerializeField]
    TableroManager elTablero;

    [Tooltip("Objeto que contiene el script de PuntuaciónManager")]
    /// <summary>
    /// Manager de la puntuación
    /// </summary>
    [SerializeField]
    PuntuacionManager puntos;

    [Tooltip("Canvas de final de partida")]
    /// <summary>
    /// Canvas de final de partida
    /// </summary>
    [SerializeField]
    GameObject finalPartida;

    /// <summary>
    /// Cantidad de disparos restantes
    /// </summary>
    public byte disparosRestantes;    

    #endregion

    #region Variables privadas

    /// <summary>
    /// Array que guarda las posiciones que se han asignado a las cartas
    /// </summary>
    List<byte> posicionesAsignadas = new List<byte>();

    /// <summary>
    /// Enumeración de las distintas temáticas
    /// </summary>
    enum temas { capitales, enfermedades, juegos }

    /// <summary>
    /// Variable que guarda la temática escogida por el usuario
    /// </summary>
    temas TematicaEscogida;

    

    #endregion

    #endregion

    #region Constructor

    //Constructor
    void Awake()
    {
        //Asigna esta instancia al campo instance
        if (instance == null)
            instance = this;
        else
            Destroy(this);  //Garantiza que sólo haya una instancia de esta clase

   
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    /// Asignación de la temática según lo escogido por el usuario
    /// </summary>
    /// <param name="num">Opción escogida por el usuario</param>
    public void setTematica(string tema)
    {
        if (tema == "capitales") TematicaEscogida = temas.capitales;
        else if (tema == "enfermedades") TematicaEscogida = temas.enfermedades;
        elTablero.GenerarTablero();
    }

    /// <summary>
    /// Asigna el sprite del reverso de la carta en función de la temática escogida por el usuario
    /// </summary>
    /// <returns> Devuelve el sprite del reverso </returns>
    public Sprite AsignacionReverso()
    {
        return spriteReverso[(int)TematicaEscogida];
    }

    /// <summary>
    /// Asigna el sprite del anverso de la carta en función de la temática escogida por el usuario
    /// </summary>
    /// <param name="idClass"> Identificador de clase de la carta. Cada pareja pertenece a una clase</param>
    /// <param name="idCard"> Identificador de la carta dentro de la pareja</param>
    /// <returns></returns>
    public Sprite AsignacionAnverso(out byte idClass, out byte idCard)
    {
        //Sprite que devolverá el método
        Sprite spriteToReturn = null;

        //Posición del sprite de esta carta dentro del array de los anversos
        //Se genera de manera aleatoria
        //Dependiendo de este número saldrán los idClass y idCard
        byte posicionSprite = 0;

        //Comprobamos si la lista está llena (si se ha dado una vuelta entera)
        //Si está lleno lo limpiamos vaciándolo
        //posicionesAsignadas es una lista de bytes
        if (posicionesAsignadas.Count == 12)
        {
            posicionesAsignadas.Clear();
        }
        
        //Asignamos un valor a la posicion del sprite dentro de los márgenes del array de los anversos
        //Si esta posición ya ha sido asignada (está contenida en la lista), se vuelve a generar otra      
        posicionSprite = (byte)Random.Range(0, spriteAnversoCapitales.Count);
        while (ContenidoEnLista(posicionSprite)) { posicionSprite = (byte)Random.Range(0, spriteAnversoCapitales.Count); }

        //Añadimos la posición que se ha escogido aleatoriamente a la lista que guarda las posiciones asignadas
        posicionesAsignadas.Add(posicionSprite);

        //Asignamos a la variable que guarda el sprite que devolver a la carta el sprite que está en esa posición
        //obtenida aleatoriamente anteriormente
        if (TematicaEscogida == temas.capitales) spriteToReturn = spriteAnversoCapitales[posicionSprite];
        else if (TematicaEscogida == temas.enfermedades) spriteToReturn = spriteAnversoEnfermedades[posicionSprite];

        //Asignamos los valores del idClass y del idCard
        //Esto es posible ya que en el array de los anversos están dispuestos consecutivamente
        if (posicionSprite % 2 == 0)
        {
            idClass = (byte)((posicionSprite / 2) + 1);
            idCard = 1;
        }
        else
        {
            idClass = (byte)((posicionSprite + 1) / 2);
            idCard = 2;
        }

        return spriteToReturn;
    }

    /// <summary>
    /// Método que comprueba si las dos cartas levantadas son pareja 
    /// </summary>
    /// <param name="primeraCarta"> Primera carta levantada</param>
    /// <param name="segundaCarta">Segunda carta levantada </param>
    public void CompareMatch(cartaManager primeraCarta, cartaManager segundaCarta)
    {
        //Comprobamos que pertenezcan a la misma clase y no sean la misma
        if (primeraCarta.getClassID() == segundaCarta.getClassID() && primeraCarta.getCardID() != segundaCarta.getCardID())
        {
            elTablero.MatchCorrecto();
            Acierto();                 
        }

        else
        {
            elTablero.MatchIncorrecto();           
        }

    }

    /// <summary>
    /// Método que controla el final de la partida.
    /// </summary>
    public void Final()
    {
        //Si no se ha acabado el tiempo se vuelve a generar otro tablero
        if (puntos.getTime() > 0)
        {
            elTablero.GenerarTablero();
        }

        //Si se ha acabado el tiempo se activa el mensaje de final de partida y se desactiva el tablero 
        else
        {
            elTablero.gameObject.SetActive(false);
            finalPartida.SetActive(true);
           
        }
    }

    /// <summary>
    /// Método que controla lo que sucede al acertar un match
    /// </summary>
    public void Acierto()
    {
        disparosRestantes += VariablesManager.Instance.bulletsMatch;
    }

    #endregion

    #region Métodos privados

    /// <summary>
    /// Método que comprueba si la posición aleatoria está en la lista de posiciones ya asignadas
    /// </summary>
    /// <param name="numero"> Posición asignada aleatoriamente </param>
    /// <returns></returns>
    private bool ContenidoEnLista(byte numero)
    {
        //Variable que vamos a devolver en el return
        bool contenido = false;

        //Recorremos la lista y comprobamos si el elemento de esa posición es igual al número generado aleatoriamente
        foreach (byte elemento in posicionesAsignadas)
        {
            if (elemento == numero) contenido = true;
        }

        return contenido;

    }

    #endregion



}
