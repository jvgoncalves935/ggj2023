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

    private string[] textosCutscenes = { "[Mayah]\n\"Esta � uma noite muito especial. A lua est� no auge, � o dia exato para conectar o mundo f�sico com o espiritual. Prestar os devidos respeitos aos nossos ancestrais e prestar nossa oferenda para o guardi�o da floresta.\"",
                                         "[Mara]\n\"Mas Vov�, por que tem um monstro desenhado junto com os nossos ancestrais?\"\"",
                                         "[Mayah]\n\"Cuidado menina! Quer que a aldeia seja arrasada por uma maldi��o?!\"\"",
                                         "[Mayah]\n\"Acalme-se, crian�a. N�o tem porque se desesperar. Desde que voc� respeite a floresta e seus antepassados o esp�rito ser� sempre seu protetor.\"",
                                         "[Mara]\n\"Vov�... porque a floresta precisa de um guardi�o? S�o todos t�o fortes, al�m disso o papai � super forte! Vai derrotar qualquer um que nos amea�ar.\"",
                                         "[Mayah]\n\"Voc� ainda � muito jovem para entender, mas os homens brancos t�m monstros enormes que destroem a floresta como se fosse uma lufada de vento nas plan�cies.\"",
                                         "[Manuel]\n\"Que merda de sonho bizarro� mas que velha maldita. V�o se fuder voc�, sua neta e seu cachorro bombado do caralho.\"",
                                         "[Manuel]\n\"O que aqueles animais colocaram na minha bebida? T� me sentindo meio chapado.\"",
                                         "[Manuel]\n\"Pelo menos a gente botou os bichos do mato pra correr.\"",
                                         "[Manuel]\n\"Ainda falaram que haviam indias lindas para se divertir... Enfim, vamos ver o que eu acho por aqui.\""};

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
            if(i == 6) {
                yield return new WaitForSeconds(2f);
            }

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
        AudioManager.InstanciaAudioManager.Play("Lenda do Esp�rito");
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
