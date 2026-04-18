using UnityEngine;
using System.Collections;

public class FlechaComportamiento : MonoBehaviour
{
    private bool acertado = false;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (acertado) return;

        if (other.CompareTag("Bullseye"))
        {
            acertado = true;
            Debug.Log("🎯 ¡CENTRO! +10 puntos");
            GameManager.SumarPuntos(10);
            Destroy(gameObject);
        }
        else if (other.CompareTag("TargetBody"))
        {
            StartCoroutine(EsperarYFallar());
        }
    }

    IEnumerator EsperarYFallar()
    {
        yield return new WaitForSeconds(0.1f);
        if (!acertado)
        {
            Debug.Log("❌ Fallaste: golpeaste el anillo exterior.");
            Destroy(gameObject);
        }
    }
}