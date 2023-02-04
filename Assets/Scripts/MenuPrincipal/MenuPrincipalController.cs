using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipalController : MonoBehaviour
{
    [SerializeField] private Button botaoIniciar;
    [SerializeField] private Button botaoOpcoes;
    [SerializeField] private Button botaoCreditos;
    [SerializeField] private Button botaoSair;

    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
        IniciarListenersBotoes();
        DesfocarMouse();
        MusicaInicio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonIniciarClick() {
        IniciarCenaCutsceneInicial();
    }

    private void OnButtonOpcoesClick() {

    }

    private void OnButtonCreditosClick() {
        IniciarCenaCreditos();
    }

    private void OnButtonSairClick() {
        FecharJogo();
    }

    private void DesfocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
        MouseOperations.DestravarCursorMultiPlat();
    #endif
    }

    private void IniciarListenersBotoes() {
        botaoIniciar.onClick.AddListener(OnButtonIniciarClick);
        botaoOpcoes.onClick.AddListener(OnButtonOpcoesClick);
        botaoCreditos.onClick.AddListener(OnButtonCreditosClick);
        botaoSair.onClick.AddListener(OnButtonSairClick);
    }

    private void IniciarCenaCutsceneInicial() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("CutsceneInicial");
        GerenciadorCena.CarregarCena("Loading");
    }

    private void IniciarCenaCreditos() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Creditos");
        GerenciadorCena.CarregarCena("Loading");
    }

    public void VerificarSceneLoaderInstanciado() {
        if(FindObjectOfType<SceneLoader>() == null) {
            Instantiate(sceneLoader);
            Instantiate(audioManager);
            //DontDestroyOnLoad(sceneLoader);
            //Debug.Log("SceneData criado em EventHorizon");
        } else {
            //Debug.Log("SceneData anteriormente criado");
        }
    }

    private void FecharJogo() {
        Application.Quit();
    }

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Profanidade");
    }
}
