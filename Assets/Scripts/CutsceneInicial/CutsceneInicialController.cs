using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneInicialController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private TMP_Text textoUI;
    private int indiceTextoAtual;
    [SerializeField] private Image[] imagensCutscene;

    private static int NUM_IMAGENS = 10;

    private string[] textosCutscenes = { "Maecenas sed ultricies sapien. Proin vitae libero vitae risus consectetur pellentesque. Sed ac nulla ut nisl cursus luctus a at massa. Curabitur pellentesque vel dolor.",
                                         "Morbi commodo lacinia varius. Aenean ut ipsum lacus. Nunc fringilla a lacus vitae maximus. Etiam a ipsum ac mi laoreet.",
                                         "Fusce faucibus nisi a tempor vulputate. Integer semper ut odio eu bibendum. Donec sit amet augue in ligula vehicula.",
                                         "Sed lacus dui, commodo quis tincidunt eget, cursus efficitur dui. Suspendisse vitae finibus nisl. Vestibulum id ipsum et ex.",
                                         "Sed at urna a leo interdum rhoncus a eu diam. Duis pulvinar sagittis est et finibus. Aenean.",
                                         "Maecenas luctus justo nec enim dignissim, at congue diam pellentesque. Nulla non mattis diam. Cras ac.",
                                         "Ut laoreet nunc eget accumsan ullamcorper. Nullam enim lorem, mattis at sodales ut, efficitur.",
                                         "Fusce ac semper turpis, non dignissim arcu. Nullam porta nisl eu vehicula.",
                                         "Maecenas nunc mi, vulputate vitae massa sit amet, tincidunt volutpat risus.",
                                         "Vestibulum eu imperdiet mauris. Suspendisse convallis nisi sapien, sit amet."};

    [SerializeField] public static GameObject instanciaCutsceneInicialController;
    private static CutsceneInicialController _instanciaCutsceneInicialController;
    public static CutsceneInicialController InstanciaCutsceneInicialController {
        get {
            if(_instanciaCutsceneInicialController == null) {
                _instanciaCutsceneInicialController = instanciaCutsceneInicialController.GetComponent<CutsceneInicialController>();
            }
            return _instanciaCutsceneInicialController;
        }
    }
    void Awake() {
        instanciaCutsceneInicialController = FindObjectOfType<CutsceneInicialController>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        indiceTextoAtual = 0;
        StartCoroutine(CutsceneInicial());
        VerificarSceneLoaderInstanciado();
        MusicaInicio();
        IniciarCoresImagens();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkipCutscene();
    }
    private IEnumerator CutsceneInicial() {
        yield return new WaitForSeconds(2f);

        for(int i = 0;i < NUM_IMAGENS;i++) {
            
            StartCoroutine(FadeIn(imagensCutscene[i], 0.6f));
            yield return new WaitForSeconds(0.6f);
            SetText(textosCutscenes[i]);
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(FadeOut(imagensCutscene[i], 0.6f));
            yield return new WaitForSeconds(0.6f);
            SetText("");
            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(3f);
        IniciarCenaFloresta();
    }
    private void IniciarCenaFloresta() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Ritual");
        //Debug.Log(SceneLoader.InstanciaSceneLoader.GetProximaCena());
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

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Lenda do Espírito");
    }

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    private IEnumerator FadeIn(Image imagem, float tempoFinal) {
        float tempo;
        for(tempo = 0.0f;tempo <= tempoFinal;tempo += Time.deltaTime) {
            imagem.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tempo / tempoFinal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        imagem.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator FadeOut(Image imagem, float tempoFinal) {
        float tempo;
        for(tempo = tempoFinal;tempo >= 0.0f;tempo -= Time.deltaTime) {
            imagem.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), tempo / tempoFinal);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        imagem.color = new Color(1, 1, 1, 0);
    }

    private void IniciarCoresImagens() {
        for(int i = 0;i < NUM_IMAGENS;i++) {
            imagensCutscene[i].color = new Color(1, 1, 1, 0);
        }
    }

    private void CheckSkipCutscene() {
        if(Input.GetButtonDown("Escape")) {
            IniciarCenaFloresta();
        }
    }
}
