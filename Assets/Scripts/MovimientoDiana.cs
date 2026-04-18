using UnityEngine;

public class MovimientoDiana : MonoBehaviour
{
    public float velocidad = 3f;      
    public float limiteIzquierdo = -5f;
    public float limiteDerecho = 5f;

    private int direccion = 1;  

    void Update()
    {
        
        transform.Translate(Vector3.right * direccion * velocidad * Time.deltaTime);

        
        if (transform.position.x >= limiteDerecho)
        {
            direccion = -1;
        }
        else if (transform.position.x <= limiteIzquierdo)
        {
            direccion = 1;
        }
    }
}