
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound manager;

    private void Awake()
    {
        //obtenemos nuestra propia referencia y la almacenamos en una estatica
        if (manager == null) manager = this;
    }
    public void Play(string clipPath , float volume = 1f , bool loop = false)
    {
        //buscamos el clip en cuestion
        AudioClip audioClip = Resources.Load(clipPath) as AudioClip;
        //si el clip fue encontrado procedemos
        if(audioClip != null)
        {
            //Buscamos la existencia del clip
            AudioSource focusSource = FindClip(audioClip);
            //Si el clip ya existe lo reutilizamos en vez de crear otro
            if (focusSource != null)
            {
                //ejecutamos el audio
                focusSource.volume = volume;
                focusSource.loop = loop;
                focusSource.Play();
            }
            //Si el clip no existe lo creamos
            else
            {
                //ejecutamos el audio
                focusSource = gameObject.AddComponent<AudioSource>();
                focusSource.clip = audioClip;
                focusSource.volume = volume;
                focusSource.loop = loop;
                focusSource.Play();
            }
        }
        //si el clip no fue encontrado
        else
        {
            Debug.LogError($"[!]El clip de audio en la direccion ({clipPath}) no fue encontrada");
        }
    }
    public void Pause(string clipPath)
    {
        //buscamos el clip en cuestion
        AudioClip audioClip = Resources.Load(clipPath) as AudioClip;
        AudioSource focusSource = FindClip(audioClip);
        //Si el clip existe lo pausamos
        if (focusSource != null) focusSource.Pause();
        else Debug.LogWarning($"[!]El clip de audio en la direccion ({clipPath}) no se puede pausar por que no existe");
    }
    AudioSource FindClip(AudioClip audioClip)
    {
        //obtenemos todos los componentes de audio
        AudioSource[] sourceList = GetComponents<AudioSource>();
        //verificamos si el clip que buscamos ya esta en uso
        for (int i = 0; i < sourceList.Length; i++)
        {
            //si el clip ya existe retornamos su componente de audip
            if (audioClip == sourceList[i].clip) return sourceList[i];
        }
        return null;
    }
    //====Script por JuanGameDev=====
}
