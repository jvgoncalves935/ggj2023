using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPrincipalController : MonoBehaviour
{
    [Header("Botões Menu Iniciar")]
    [SerializeField] private Button botaoIniciar;
    [SerializeField] private Button botaoOpcoes;
    [SerializeField] private Button botaoCreditos;
    [SerializeField] private Button botaoSair;

    [Header("Botões Menu Opções")]
    [SerializeField] private Button botaoMenor;
    [SerializeField] private Button botaoMaior;
    [SerializeField] private TMP_Text textoIdioma;
    [SerializeField] private TMP_Text textoIdiomaSelect;
    [SerializeField] private TMP_Text textoVoltar;
    [SerializeField] private Button botaoVoltar;

    [Header("Pivot Menus")]
    [SerializeField] private GameObject menuPrincipalObj;
    [SerializeField] private GameObject menuOpcoesObj;

    [Header("Outros")]
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;

    private Dictionary<string, string> stringsOpcoes;

    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
        IniciarListenersBotoes();
        //FocarMouse();
        DesfocarMouse();
        MusicaInicio();

        CarregarStrings();
        CarregarStringsLinguagens();

        AtivarMenuPrincipal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnButtonIniciarClick() {
        IniciarCenaCutsceneInicial();
    }

    private void OnButtonOpcoesClick() {
        AtivarMenuOpcoes();
    }

    private void OnButtonCreditosClick() {
        IniciarCenaCreditos();
    }

    private void OnButtonSairClick() {
        FecharJogo();
    }

    private void OnButtonVoltarOpcoesClick() {
        AtivarMenuPrincipal();
    }

    private void OnButtonMaiorOpcoesClick() {
        AtivarMenuOpcoes();
    }

    private void OnButtonMenorOpcoesClick() {
        AtivarMenuOpcoes();
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

        botaoVoltar.onClick.AddListener(OnButtonVoltarOpcoesClick);
        botaoMaior.onClick.AddListener(OnButtonMaiorOpcoesClick);
        botaoMenor.onClick.AddListener(OnButtonMenorOpcoesClick);
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

    private void FocarMouse() {
    #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            MouseOperations.FocarMouseMultiPlat();
    #endif
    }

    private void CarregarStrings() {
        stringsOpcoes = LocalizationSystem.GetDicionarioStringsCena("MenuPrincipal");
    }

    private void CarregarStringsLinguagens() {
        Dictionary<string, string> stringsLinguagens = LocalizationSystem.GetDicionarioStringsCenaCommon("MenuPrincipalCommon");
        foreach(KeyValuePair<string, string> entrada in stringsLinguagens) {
            stringsOpcoes.Add(entrada.Key, entrada.Value);
        }
    }

    private void ToggleMenuPrincipal(bool flag) {
        menuPrincipalObj.SetActive(flag);
    }

    private void ToggleMenuOpcoes(bool flag) {
        menuOpcoesObj.SetActive(flag);
    }

    private void AtivarMenuPrincipal() {
        ToggleMenuPrincipal(true);
        ToggleMenuOpcoes(false);
    }

    private void AtivarMenuOpcoes() {
        ToggleMenuPrincipal(false);
        ToggleMenuOpcoes(true);
    }

}
