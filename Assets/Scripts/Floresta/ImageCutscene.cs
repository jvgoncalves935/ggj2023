using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCutscene : MonoBehaviour
{
    public int id;
    private bool ativado;
    public AudioClip audio;
    [SerializeField] private string[] textosCutscenes;
    // Start is called before the first frame update
    void Start()
    {
        ativado = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(IsCorpoJogador(other) && ativado) {
            ativado = false;
            ImageCutsceneUIController.InstanciaImageCutsceneUIController.CutsceneCoroutine(textosCutscenes,id);
            TocarAudio();
        }
    }

    public bool IsCorpoJogador(Collider collider) {
        if(collider.tag == "Player") {
            return true;
        }
        return false;
    }

    private void TocarAudio() {
        ImageCutsceneUIController.InstanciaImageCutsceneUIController.PlayAudio(audio);
    }
    
}
