using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzPulsando : MonoBehaviour
{
    private static float VALOR_ADICAO = 0.3f;
    private static float FREQUENCIA = 0.7f;
    [SerializeField] private Light luz;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        PiscarLuz();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PiscarLuz() {
        coroutine = PiscarLuzCoroutine();
        StartCoroutine(coroutine);
    }

    private IEnumerator PiscarLuzCoroutine() {
        while(true) {
            luz.intensity = Mathf.Abs(Mathf.Cos(Time.time * FREQUENCIA)) + VALOR_ADICAO;
            yield return null;
        }
    }

    public void PararLuz() {
        StopCoroutine(coroutine);
        StartCoroutine(PararLuzCoroutine());
    }

    private IEnumerator PararLuzCoroutine() {
        while(luz.intensity>VALOR_ADICAO) {
            luz.intensity -= Time.deltaTime;
            yield return null;
        }
        luz.intensity = VALOR_ADICAO;
    }
}
