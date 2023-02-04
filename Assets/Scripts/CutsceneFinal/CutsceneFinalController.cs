using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFinalController : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;
    [SerializeField] private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CutsceneFinal());
        VerificarSceneLoaderInstanciado();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CutsceneFinal() {
        yield return new WaitForSeconds(3f);
        IniciarCenaMenuPrincipal();
    }
    private void IniciarCenaMenuPrincipal() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("MenuPrincipal");
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
}
