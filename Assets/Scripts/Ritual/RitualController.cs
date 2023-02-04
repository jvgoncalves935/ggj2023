using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RitualController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ScenesData scenesData;
    [SerializeField] private ItemColetável[] itensColetaveisObj;
    [SerializeField] private ItemColetado[] itensColetadosObj;
    [SerializeField] private bool[] itensColetaveis;
    [SerializeField] private bool[] itensPosicionados;

    [SerializeField] private TMP_Text textoUI;

    [SerializeField] public static GameObject instanciaRitualController;
    private static RitualController _instanciaRitualController;
    public static RitualController InstanciaRitualController {
        get {
            if(_instanciaRitualController == null) {
                _instanciaRitualController = instanciaRitualController.GetComponent<RitualController>();
            }
            return _instanciaRitualController;
        }
    }
    void Awake() {
        instanciaRitualController = FindObjectOfType<RitualController>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
        IniciarItensColetaveis();
        MusicaInicio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void IniciarCenaCutsceneFinal() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("CutsceneFinal");
        GerenciadorCena.CarregarCena("Loading");
    }

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            Instantiate(scenesData);
            //DontDestroyOnLoad(sceneLoader);
            //Debug.Log("SceneData criado em EventHorizon");
        } else {
            //Debug.Log("SceneData anteriormente criado");
        }
    }

    private void IniciarItensColetaveis() {
        itensColetaveis = new bool[3];
        itensPosicionados = new bool[3];
        for(int i = 0;i < itensColetaveis.Length;i++) {
            itensColetaveis[i] = false;
            itensPosicionados[i] = false;
        }

        for(int i = 0;i < itensColetaveisObj.Length;i++) {
            itensColetadosObj[i].SetId(i);
            itensColetaveisObj[i].SetId(i);
        }
    }

    public void SetItemColetado(int id) {
        itensColetaveis[id] = true;
        itensColetadosObj[id].SetColetado();

    }

    public void SetItemPosicionado(int id) {
        itensPosicionados[id] = true;

        if(IsTodosItensPosicionados()) {
            StartCoroutine(ContagemFinal());
        }
    }

    private bool IsTodosItensPosicionados() {
        for(int i = 0;i < itensColetaveis.Length;i++) {
            if(!itensPosicionados[i]) {
                return false;
            }
        }

        return true;
    }

    private IEnumerator ContagemFinal() {
        //Debug.Log("contagem final");
        yield return new WaitForSeconds(5f);
        IniciarCenaCutsceneFinal();
    }

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Ritual de Redenção");
    }
}
