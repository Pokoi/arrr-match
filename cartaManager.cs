using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador de las cartas. Maneja su construcción, su activación de animaciones y su destrucción al acertar
/// </summary>
public class cartaManager : MonoBehaviour
{

    #region Variables de Componentes

    /// <summary>
    /// Animator de la carta
    /// </summary>    
    Animator miAnimator;

    /// <summary>
    /// Collider propio de la carta
    /// </summary>
    Collider miCollider;

    /// <summary>
    /// Sprite Renderer del anverso de la carta
    /// </summary>
    SpriteRenderer spriteRendererAnverso;

    /// <summary>
    /// Sprite Renderer del reverso de la carta
    /// </summary>
    SpriteRenderer spriteRendererReverso;

    /// <summary>
    /// Tablero del juego
    /// </summary>
    TableroManager elTablero;

    #endregion

    #region Variables identificadoras de la carta

    /// <summary>
    ///  Identificador de clase de la carta. Cada pareja pertenece a una clase
    /// </summary>
    byte classID = 0;

    /// <summary>
    /// Identificador de la carta dentro de su clase
    /// </summary>
    byte cardID = 0;

    #endregion

    #region Constructor e inicio

    /// <summary>
    /// Constructor de la carta.
    /// Asigna los componentes
    /// Asignamos los sprites de anverso y reverso de la carta por medio del GameManager
    /// </summary>
    private void Awake()
    {
        //Asignamos los componentes
        miAnimator = GetComponent<Animator>();
        miCollider = GetComponent<Collider>();
        spriteRendererAnverso = transform.GetChild(0).GetComponent<SpriteRenderer>();
        spriteRendererReverso = transform.GetChild(1).GetComponent<SpriteRenderer>();

        //Asignamos los sprites de anverso y reverso de la carta por medio del GameManager
        spriteRendererReverso.sprite = GameManager.Instance.AsignacionReverso();
        spriteRendererAnverso.sprite = GameManager.Instance.AsignacionAnverso(out classID, out cardID);


    }

    //Al iniciar 
    void Start()
    {
        //Activamos el componente Collider del GameObject
        miCollider.enabled = true;
    }

    #endregion

    #region Métodos públicos

    /// <summary>
    /// Getter del identificador de clase
    /// </summary>
    /// <returns></returns>
    public byte getClassID()
    {
        return classID;
    }

    /// <summary>
    /// Getter del identificador de carta
    /// </summary>
    /// <returns></returns>
    public byte getCardID()
    {
        return cardID;
    }

    /// <summary>
    /// Método que destruye la carta si el match ha sido correcto 
    /// </summary>
    public void Acierto()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Método que voltea la carta si ha sido el match incorrecto
    /// </summary>
    public void Fallo()
    {
        //Se activa la animación que gira las cartas
        miAnimator.SetTrigger("girar");

        //Se vuelve a activar el collider para que el usuario pueda seguir
        //girando esta carta
        miCollider.enabled = true;

    }   

    #endregion

    #region Métodos privados

    /// <summary>
    /// Método que controla aquello que se produce al hacer click sobre él
    /// </summary>
    private void OnMouseDown()
    {
        //Se activa la animación que gira las cartas
        miAnimator.SetTrigger("girar");

        //Se desactiva el collider para que el usuario no pueda girar la carta hasta
        //que no se compruebe el match de las cartas levantadas.
        miCollider.enabled = false;
        elTablero = transform.parent.GetComponent<TableroManager>();      

        elTablero.LevantarCarta(this);
    }


    #endregion

}
