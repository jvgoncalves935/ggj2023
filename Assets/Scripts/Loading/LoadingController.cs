using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private bool primeiroUpdate;
    private AsyncOperation operacao;
    [SerializeField] private Image imagemEH;

    void Start()
    {
        primeiroUpdate = true;
    }

    private void Update() {
        if(primeiroUpdate){
            primeiroUpdate = false;
            StartCoroutine(CarregarCena());
            FocarMouse();
        }
        LogoPulsando();
    }

    private IEnumerator CarregarCena(){
        string proximaCena = SceneLoader.InstanciaSceneLoader.GetProximaCena();
        int indiceProximaCena = SceneLoader.InstanciaSceneLoader.GetIndiceProximaCena(proximaCena);

        operacao = SceneManager.LoadSceneAsync(indiceProximaCena,LoadSceneMode.Single);
        operacao.completed += (op) => PosLoading();
        
        //decimal progresso;

        while(!operacao.isDone){
            //progresso = (decimal) (Mathf.Clamp01(operacao.progress / 0.9f) * 100);
            //textProgresso.text =  progresso.ToString("#.##")+ "%";
            yield return null;
        }
    }

    private void PosLoading(){
        Debug.Log("pos loading");
        AudioManager.InstanciaAudioManager.IniciarSonsCenaAtual();
        SceneLoader.InstanciaSceneLoader.SetProximaCena("");
        Debug.Log("acabou");
    }

    private void LogoPulsando(){
        float valor = Mathf.Abs(0.5f + Mathf.Sin(Time.time * 2.0f)/2.0f);
        //Debug.Log(valor);
        imagemEH.color = new Color(1.0f, 1.0f, 1.0f, valor);
    }

    private void FocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.FocarMouseMultiPlat();
    #endif
    }
}
