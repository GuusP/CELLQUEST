using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Explica��o

/* 
 * ScriptableObject de um �udio a ser tocado. Define o volume do som, se � loop e qual � o audio
 * queria colocar um bot�o no inspector para dar play no audio para conseguir ver se o volume ta
 * bom sem precisar dar play mas acabei n fazendo 
*/

#endregion

[CreateAssetMenu(fileName = "AudioClip", menuName = "Audio/AudioClip")]
public class AudioToPlay : ScriptableObject
{
    public AudioClip audioClip;
    public bool loop;
    [Range(0f, 1f)]
    public float volume;


}
