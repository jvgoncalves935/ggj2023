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
    [SerializeField] private TMP_Text textoIniciar;
    [SerializeField] private TMP_Text textoOpcoes;
    [SerializeField] private TMP_Text textoCreditos;
    [SerializeField] private TMP_Text textoFugir;

    [Header("Botões Menu Opções")]
    [SerializeField] private Button botaoMenor;
    [SerializeField] private Button botaoMaior;
    [SerializeField] private TMP_Text textLinguagens;
    [SerializeField] private TMP_Text textLinguagensSelect;
    [SerializeField] private TMP_Text textoVoltar;
    [SerializeField] private Button botaoVoltar;

    [Header("Pivot Menus")]
    [SerializeField] private GameObject menuPrincipalObj;
    [SerializeField] private GameObject menuOpcoesObj;

    [Header("Linguagens")]
    private string[] linguagensSiglas;
    private string linguagemAtual;
    private int linguagemAtualIndice;
    private int numLinguagens;
    private Dictionary<int, string> linguagens;
    private SaveData saveData;

    [Header("Outros")]
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private SaveUIController saveUIController;



    private Dictionary<string, string> stringsOpcoes;

    // Start is called before the first frame update
    void Start()
    {
        VerificarSceneLoaderInstanciado();
        IniciarListenersBotoes();
        //FocarMouse();
        DesfocarMouse();
        MusicaInicio();

        IniciarStrings();

        AtivarMenuPrincipal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IniciarStrings() {
        CarregarSaveOpcoes();
        CarregarStrings();
        CarregarStringsLinguagens();
        CarregarLinguagemAtual();
        IniciarStringsLinguagens();
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
        FecharMenuOpcoes();
        AtivarMenuPrincipal();
    }

    private void OnButtonMaiorOpcoesClick() {
        AtualizarLinguagem(1);
    }

    private void OnButtonMenorOpcoesClick() {
        AtualizarLinguagem(-1);
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
            Instantiate(saveUIController);
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
        stringsOpcoes = LocalizationSystem.GetDicionarioStringsCena(GerenciadorCena.NomeCenaAtual());
    }

    private void CarregarStringsLinguagens() {
        Dictionary<string, string> stringsLinguagens = LocalizationSystem.GetDicionarioStringsCenaCommon(GerenciadorCena.NomeCenaAtual()+"Common");
        foreach(KeyValuePair<string, string> entrada in stringsLinguagens) {
            //Debug.Log(entrada.Key + " " + entrada.Value);
            stringsOpcoes.Add(entrada.Key, entrada.Value);
        }
    }

    private void IniciarStringsLinguagens() {
        textLinguagens.text = stringsOpcoes["OPCOES_LINGUAGEM"];
        textLinguagensSelect.text = stringsOpcoes["OPCOES_LINGUAGEM_" + linguagensSiglas[linguagemAtualIndice].ToUpper()];

        textoVoltar.text = stringsOpcoes["MENU_VOLTAR"];
        textoIniciar.text = stringsOpcoes["MENU_INICIAR"];
        textoOpcoes.text = stringsOpcoes["MENU_OPCOES"];
        textoCreditos.text = stringsOpcoes["MENU_CREDITOS"];
        textoFugir.text = stringsOpcoes["MENU_FUGIR"];
    }

    private void ToggleMenuPrincipal(bool flag) {
        menuPrincipalObj.SetActive(flag);
        ResetarBotaoSelecionado();
    }

    private void ToggleMenuOpcoes(bool flag) {
        menuOpcoesObj.SetActive(flag);
        ResetarBotaoSelecionado();
    }

    private void AtivarMenuPrincipal() {
        ToggleMenuPrincipal(true);
        ToggleMenuOpcoes(false);
    }

    private void AtivarMenuOpcoes() {
        ToggleMenuPrincipal(false);
        ToggleMenuOpcoes(true);
    }

    public void AtualizarLinguagem(int add) {
        int novaPosicao = AtualizarPosicaoLinguagem(add);
        TrocarLinguagem(novaPosicao);
        AlterarLinguagemSave();
    }

    private int AtualizarPosicaoLinguagem(int add) {
        int novaPosicao = linguagemAtualIndice + add;
        int posicaoFinal;
        if(novaPosicao == -1) {
            posicaoFinal = numLinguagens - 1;
        } else {
            if(novaPosicao == numLinguagens) {
                posicaoFinal = 0;
            } else {
                posicaoFinal = novaPosicao;
            }
        }

        return posicaoFinal;
    }

    private void CarregarLinguagemAtual() {
        linguagens = LocalizationSystem.DicionarioLinguagens();
        linguagemAtual = LocalizationSystem.LinguagemAtual();
        numLinguagens = linguagens.Count;

        linguagensSiglas = new string[numLinguagens];
        foreach(KeyValuePair<int, string> kvp in linguagens) {
            if(kvp.Value == linguagemAtual) {
                linguagemAtualIndice = kvp.Key;
            }
            linguagensSiglas[kvp.Key] = kvp.Value;
        }
    }

    private void TrocarLinguagem(int pos) {
        linguagemAtualIndice = pos;
        textLinguagensSelect.text = stringsOpcoes["OPCOES_LINGUAGEM_" + linguagensSiglas[linguagemAtualIndice].ToUpper()];
        linguagemAtual = linguagensSiglas[linguagemAtualIndice];
    }

    public void CarregarSaveOpcoes() {
        saveData = SaveSystem.CarregarData();
        //Debug.Log(saveData.Linguagem);
    }

    private void AlterarLinguagemSave() {
        saveData.Linguagem = linguagemAtual;
        //Debug.Log(saveData.Linguagem);
    }

    private void SalvarSaveOpcoes() {
        LocalizationSystem.SetLinguagem(linguagemAtual);
        SaveUIController.InstanciaSaveUIController.ReiniciarDicionario(linguagemAtual);
        SaveSystem.SalvarAlteraçõesSave(saveData, true);
    }

    public void FecharMenuOpcoes() {
        SalvarSaveOpcoes();
        IniciarStrings();

        //Debug.Log(linguagemAtual);
    }

    private void ResetarBotaoSelecionado() {
        BotaoController.InstanciaBotaoController.DeselecionarBotaoMouseEnter();
    }

}
