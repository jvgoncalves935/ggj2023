using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageCutsceneUIController : MonoBehaviour
{
    [SerializeField] private Image imagemUI;
    [SerializeField] private List<Image> imagensCutscene;
    [SerializeField] private TMP_Text textoUI;
    private string[] textosCutscene;
    private AudioSource audioSourceSons;

    private Dictionary<string, string> stringsRitual;
    private Dictionary<string, string> stringsPersonagensRitual;

    [SerializeField] public static GameObject instanciaImageCutsceneUIController;
    private static ImageCutsceneUIController _instanciaImageCutsceneUIController;
    public static ImageCutsceneUIController InstanciaImageCutsceneUIController {
        get {
            if(_instanciaImageCutsceneUIController == null) {
                _instanciaImageCutsceneUIController = instanciaImageCutsceneUIController.GetComponent<ImageCutsceneUIController>();
            }
            return _instanciaImageCutsceneUIController;
        }
    }

    void Awake() {
        instanciaImageCutsceneUIController = FindObjectOfType<ImageCutsceneUIController>().gameObject;
        CarregarStrings();
        audioSourceSons = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        IniciarImagens();
        audioSourceSons = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CutsceneCoroutine(string[] textos, int id) {
        imagemUI = imagensCutscene[id];
        textosCutscene = textos;
        StartCoroutine(CutsceneImagem(textos));
    }

    private IEnumerator CutsceneImagem(string[] textos) {
        //textosCutscene = textos;
        
        StartCoroutine(FadeIn(imagemUI, 0.6f));
        yield return new WaitForSeconds(0.6f);

        Player.InstanciaPlayer.ToggleFirstPersonController(false);
        

        SetText(textosCutscene[0]);
        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(6.9f));
        //yield return new WaitForSeconds(7.0f);

        SetText(textosCutscene[1]);
        yield return new WaitForSeconds(0.1f);

        yield return StartCoroutine(SkippableCutscenes.InstanciaSkippableCutscenes.WaitForSecondsCancelavel(5.7f));
        //yield return new WaitForSeconds(5.7f);

        SetText("");
        Player.InstanciaPlayer.ToggleFirstPersonController(true);
        StartCoroutine(FadeOut(imagemUI, 0.6f));
        yield return new WaitForSeconds(0.6f);
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

    public void SetText(string texto) {
        textoUI.text = texto;
    }

    private void IniciarImagens() {
        for(int i = 0;i < imagensCutscene.Count;i++) {
            imagensCutscene[i].color = new Color(1, 1, 1, 0);
        }
    }

    public void PlayAudio(AudioClip audio) {
        audioSourceSons.clip = audio;
        audioSourceSons.loop = false;
        audioSourceSons.Play();
    }

    private void CarregarStrings() {
        LocalizationSystem.GetDicionarioStringsFullCena(GerenciadorCena.NomeCenaAtual(), out stringsRitual, out stringsPersonagensRitual);
    }

    public Dictionary<string,string> GetDicionarioStrings() {
        return stringsRitual;
    }

    public Dictionary<string, string> GetDicionarioStringsPersonagens() {
        return stringsPersonagensRitual;
    }

    public string[] GetStringsHUDRitual() {
        return new string[]{ stringsRitual["RITUAL_ITEM_NAO_COLETADO"],
                             stringsRitual["RITUAL_ITEM_OFERENDA"],
                             stringsRitual["RITUAL_ITEM_COLETAR"]};

    }

}
