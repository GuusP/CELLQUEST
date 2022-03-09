using System.Collections;
using System.Collections.Generic;
using UnityEngine;





#region Explica��o

/* 
� usado para tocar os sons no jogo. ficou meio confuso pq eu queria fazer algo com scriptableObjects mas n�o tive tempo para pensar direito. 
tem dois audioSource, um para tocar sons em loop (as m�sicas de fundo) e um para tocar sons q s� tocam uma vez (efeitos de feedback)

tem uma fun��o chamada PlayAudio() para tocar o som e essa fun��o pede por par�metro um scriptableObject chamado AudioToPlay. se for um loop,
vai ser tocado no audioSource para loops. Se n�o, vai ser no normal. 
 
*/

#endregion


public class AudioManager : MonoBehaviour
{
    public AudioSource NoLoopAudioSource;
    public AudioSource LoopAudioSource;

    public AudioToPlay mainSong;
    // Start is called before the first frame update
    void Start()
    {
        PlayAudio(mainSong);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(AudioToPlay audioToPlay)
    {
        if (audioToPlay.loop)
        {
            LoopAudioSource.loop = true;
            LoopAudioSource.clip = audioToPlay.audioClip;
            LoopAudioSource.volume = audioToPlay.volume;
            LoopAudioSource.Play();
        }
        else
        {
            NoLoopAudioSource.loop = false;
            NoLoopAudioSource.clip = audioToPlay.audioClip;
            NoLoopAudioSource.volume = audioToPlay.volume;
            NoLoopAudioSource.Play();
        }
    }

    public void MuteAudio()
    {
        LoopAudioSource.mute = true;
        NoLoopAudioSource.mute = true;
    }

    public void UnmuteAudio()
    {
        LoopAudioSource.mute = false;
        NoLoopAudioSource.mute = false;
    }
}
