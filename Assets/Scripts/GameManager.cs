using UnityEngine;
using TMPro;                              
using UnityEngine.InputSystem;            

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("UI Elements")]
    public TMP_Text textoPuntaje;          
    public UnityEngine.UI.Image barraPotencia; 

    [Header("Disparo")]
    public GameObject prefabFlecha;        
    public Transform puntoDisparo;        

    
    private int puntaje = 0;
    private float potencia = 0f;
    private float maxPotencia = 2f;        
    private float tiempoCarga = 0f;
    private bool cargando = false;
    private bool espacioPresionado = false;

    void Awake()
    {
        instancia = this;
    }

    void Start()
    {
        ActualizarUI();
        
        if (barraPotencia != null)
            barraPotencia.fillAmount = 0f;
    }

    void Update()
    {
        
        bool espacioActual = Keyboard.current != null && Keyboard.current.spaceKey.isPressed;

        
        if (!espacioPresionado && espacioActual)
        {
            cargando = true;
            tiempoCarga = 0f;
            potencia = 0f;
        }

        
        if (cargando && espacioActual)
        {
            tiempoCarga += Time.deltaTime;
            potencia = Mathf.Min(tiempoCarga * 3f, maxPotencia);
            ActualizarBarraPotencia();
        }

        
        if (espacioPresionado && !espacioActual && cargando)
        {
            Disparar();
            cargando = false;
            potencia = 0f;
            ActualizarBarraPotencia();
        }

        espacioPresionado = espacioActual;
    }

    void Disparar()
    {
        if (prefabFlecha == null)
        {
            Debug.LogError("❌ GameManager: no tiene asignado el prefab de la flecha.");
            return;
        }
        if (puntoDisparo == null)
        {
            Debug.LogError("❌ GameManager: no tiene asignado el punto de disparo (Arco).");
            return;
        }

        GameObject nuevaFlecha = Instantiate(prefabFlecha, puntoDisparo.position, Quaternion.identity);

        if (nuevaFlecha.TryGetComponent<Rigidbody2D>(out var rb))
        {
           
            float velX = potencia * 8f;
            float velY = potencia * 4f;
            rb.linearVelocity = new Vector2(velX, velY);
        }
    }

    void ActualizarBarraPotencia()
    {
        if (barraPotencia != null)
            barraPotencia.fillAmount = potencia / maxPotencia;
    }

    public static void SumarPuntos(int puntos)
    {
        if (instancia != null)
        {
            instancia.puntaje += puntos;
            instancia.ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        if (textoPuntaje != null)
            textoPuntaje.text = "Puntos: " + puntaje;
    }
}