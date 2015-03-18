using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager> {
    #region enum publics
    public enum SoundName {
        BACKGROUND_MUSIC,
        SHOOT
    };
    #endregion

    #region Attribut privés
    private AudioSource[] sounds;
    #endregion

    #region Méthodes publiques
    public void PlaySound (SoundName soundName) {
        ToggleSound (soundName, true);
    }

    public void StopSound (SoundName soundName) {
        ToggleSound (soundName, false);
    }
    #endregion

    #region Méthodes privées
    void Awake () {
        sounds = GetComponents<AudioSource> ();
    }

    void ToggleSound (SoundName soundName, bool play) {
        int soundIndex = -1;

        switch (soundName) {
            case SoundName.BACKGROUND_MUSIC:
                soundIndex = 0;
                break;
            case SoundName.SHOOT:
                soundIndex = 1;
                break;
        }

        if (play) {
            sounds[soundIndex].Play ();
        }
        else {
            sounds[soundIndex].Stop ();
        }
    }
    #endregion
}
