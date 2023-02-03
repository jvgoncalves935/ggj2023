using UnityEngine;

public class SoundClip : MonoBehaviour
{
    private int indice;
    public string nome;
    [SerializeField] private bool playOnStart;
    [SerializeField] public bool efeitoSonoro;
    [SerializeField] public bool wasPlaying;
    [SerializeField] public bool ignorarPause;

    private void Start(){
        if(!efeitoSonoro) {
            indice = AudioManager.InstanciaAudioManager.Find(nome);
        }

        //Debug.Log("nome: "+nome+", indice: "+indice);
        if(playOnStart){
            Play();
        }
    }

    public void Play(){
        AudioManager.InstanciaAudioManager.Play(nome);
    }

    public void Stop(){
        AudioManager.InstanciaAudioManager.Stop(nome);
    }

    public void SetWasPlaying(bool flag) {
        wasPlaying = flag;
    }

    public bool WasPlaying() {
        return wasPlaying;
    }

    public void SetIgnorarPause(bool flag) {
        ignorarPause = flag;
    }

    public bool IsIgnorarPause() {
        return ignorarPause;
    }

}
