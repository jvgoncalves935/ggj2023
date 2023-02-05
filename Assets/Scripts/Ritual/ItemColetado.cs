using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColetado : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private bool coletado;
    [SerializeField] private bool posicionado;
    [SerializeField] private GameObject item;
    private bool isCorpoJogador;
    public AudioClip audio;
    // Start is called before the first frame update
    void Start()
    {
        item.SetActive(false);
        coletado = false;
        posicionado = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetUseButton();
    }
    private void OnTriggerEnter(Collider other) {
        if(IsCorpoJogador(other)) {
            isCorpoJogador = true;
            if(!coletado) {
                RitualController.InstanciaRitualController.SetText("Tokonimé aguarda sua oferenda.");
            } else {
                if(!posicionado) {
                    RitualController.InstanciaRitualController.SetText("Aperte \"F\" para realizar sua oferenda.");
                }
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if(!posicionado && IsCorpoJogador(other)) {
            isCorpoJogador = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        isCorpoJogador = false;
        RitualController.InstanciaRitualController.SetText("");
    }
    public void SetId(int _id) {
        id = _id;
    }
    public void SetColetado() {
        coletado = true;
    }
    public void SetItemPosicionado() {
        posicionado = true;
        AtivarItem();
        RitualController.InstanciaRitualController.SetItemPosicionado(id);
        RitualController.InstanciaRitualController.SetText("");

    }
    public void AtivarItem() {
        item.SetActive(true);
    }
    public bool IsCorpoJogador(Collider collider) {
        if(collider.tag == "Player") {
            return true;
        }
        return false;
    }

    private void GetUseButton() {
        if(Input.GetButtonDown("Use") && coletado && !posicionado && isCorpoJogador) {
            SetItemPosicionado();
            TocarAudio();
        }
    }

    private void TocarAudio() {
        ImageCutsceneUIController.InstanciaImageCutsceneUIController.PlayAudio(audio);
    }
}
