using UnityEngine;

public class TriggerFinal : MonoBehaviour {
    [SerializeField] private bool ativado;

    private static TriggerFinal _instanciaTriggerFinal;
    public static GameObject instanciaTriggerFinal;
    public static TriggerFinal InstanciaTriggerFinal {
        get {
            if(_instanciaTriggerFinal == null) {
                _instanciaTriggerFinal = instanciaTriggerFinal.GetComponent<TriggerFinal>();
            }
            return _instanciaTriggerFinal;
        }
    }

    private void Awake() {
        instanciaTriggerFinal = FindObjectOfType<TriggerFinal>().gameObject;
        //gameObject.GetComponent<Renderer>().enabled = false;
    }

    void Start () {
		ativado = true;
	}

	void OnTriggerStay(Collider collider){
		if(!ativado){
			return;
		}
		if(IsCorpoJogador(collider)){
			
            ativado = false;
            EncerrarFase();

        }
	}

    public void ToggleTrigger(bool flag) {
        gameObject.SetActive(flag);
        ativado = flag;
        gameObject.GetComponent<Renderer>().enabled = true;
    }

    public void AtivarFinal(){
        ToggleTrigger(true);
	}

	public bool IsCorpoJogador(Collider collider){
		if(collider.tag == "Player"){
			return true;
		}
		return false;
	}

    private void EncerrarFase() {
        SceneLoader.InstanciaSceneLoader.SetProximaCena("Ritual");
        GerenciadorCena.CarregarCena("Loading");
    }
}
