using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemColetável : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private bool coletado;
    [SerializeField] private GameObject item;
    private bool isCorpoJogador;

    // Start is called before the first frame update
    void Start()
    {
        coletado = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetUseButton();
    }
    public bool IsCorpoJogador(Collider collider) {
        if(collider.tag == "Player") {
            return true;
        }
        return false;
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log(useButton + " " + isCorpoJogador);
        if(IsCorpoJogador(other)) {
            isCorpoJogador = true;
            RitualController.InstanciaRitualController.SetText("Aperte \"F\" para coletar a oferenda.");
        }
    }
    private void OnTriggerStay(Collider other) {
        //Debug.Log(useButton + " " + isCorpoJogador);
        if(IsCorpoJogador(other)) {
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

    private void SetItemColetado() {
        coletado = true;
        RitualController.InstanciaRitualController.SetItemColetado(id);
        gameObject.SetActive(false);
    }

    private void GetUseButton() {
        if(Input.GetButtonDown("Use") && isCorpoJogador) {
            SetItemColetado();
            RitualController.InstanciaRitualController.SetText("");
        } 
    }
}
