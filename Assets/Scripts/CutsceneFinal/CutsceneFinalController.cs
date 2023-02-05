using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneFinalController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    private static int NUM_IMAGENS = 5;
    [SerializeField] private Image[] imagensCutscene;
    [SerializeField] private TMP_Text textoUI;

    private string[] textosCutscenes = { "[Tokonimé]\n:\"Seu sacrifício foi recebido! Porém, o sangue ofertado não condiz com o sangue derramado.\"",
                                         "[Manuel]\n:\"Como assim? Eu fiz tudo que me pediu no ritual. Até fiquei ouvindo aquela velha rabugenta falando na minha cabeça.\"",
                                         "[Tokonimé]\n:\"Sua alma ficará presa na floresta até que você descubra suas raízes e pague sua dívida com sangue. Seus atos profanos serão punidos.\"",
                                         "[Manuel]\n:\"Aaarrrghhh!\"",
                                         "[Indígenas]\n:\"Levem ele.\""};

    [SerializeField] public static GameObject instanciaCutsceneFinalController;
    private static CutsceneFinalController _instanciaCutsceneFinalController;
    public static CutsceneFinalController InstanciaCutsceneFinalController {
        get {
            if(_instanciaCutsceneFinalController == null) {
                _instanciaCutsceneFinalController = instanciaCutsceneFinalController.GetComponent<CutsceneFinalController>();
            }
            return _instanciaCutsceneFinalController;
        }
    }
    void Awake() {
        instanciaCutsceneFinalController = FindObjectOfType<CutsceneFinalController>().gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        IniciarCoresImagens();
        VerificarSceneLoaderInstanciado();
        StartCoroutine(CutsceneFinal());
        MusicaInicio();
        IniciarCoresImagens();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSkipCutscene();
    }

    private IEnumerator CutsceneFinal() {
        yield return new WaitForSeconds(2f);

        for(int i = 0;i < NUM_IMAGENS;i++) {

            StartCoroutine(FadeIn(imagensCutscene[i], 0.6f));
            yield return new WaitForSeconds(0.6f);
            SetText(textosCutscenes[i]);
            yield return new WaitForSeconds(4.4f);
            StartCoroutine(FadeOut(imagensCutscene[i], 0.6f));
            yield return new WaitForSeconds(0.6f);
            SetText("");
            yield return new WaitForSeconds(0.6f);
        }

        yield return new WaitForSeconds(3f);
        IniciarCenaCreditos();
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

    public void MusicaInicio() {
        AudioManager.InstanciaAudioManager.Play("Nami Budi");
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

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    private void CheckSkipCutscene() {
        if(Input.GetButtonDown("Escape")) {
            IniciarCenaCreditos();
        }
    }
}
