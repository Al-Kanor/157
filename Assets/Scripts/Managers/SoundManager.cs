using UnityEngine;
using System.Collections;

public class SoundManager : Singleton<SoundManager> {
    #region enum publics
    public enum SoundName {
        BACKGROUND_MUSIC,
        CAISSE,
		EXPLOSION,
		FOREUSE_LOOP,
		MINERAI,
		GAMESTART,
		BUTTON,
		HITROCK1,
		HITROCK2,
		HITROCK3,
		HITROCK4,
		HITROCK5,
		HITROCK6,
		HITROCK7,
		VOICE1,
		VOICE2,
		VOICE3,
		VOICE4,
		VOICE5,
		VOICE6,
		VOICE7,
		VOICE8,
		VOICE9,
		VOICE10,
		VOICE11,
		VOICE12,
		VOICE13,
		VOICE14,
		VOICE15,
		VOICE16,
		VOICE17,
		VOICE18
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

            case SoundName.CAISSE:
                soundIndex = 1;
            break;

			case SoundName.EXPLOSION:
				soundIndex = 2;
			break;

			case SoundName.FOREUSE_LOOP:
				soundIndex = 3;
			break;

			case SoundName.MINERAI:
				soundIndex = 4;
			break;
			case SoundName.GAMESTART:
				soundIndex = 5;
			break;
			case SoundName.BUTTON:
				soundIndex = 6;
			break;
			case SoundName.HITROCK1:
				soundIndex = 7;
			break;
			case SoundName.HITROCK2:
				soundIndex = 8;
			break;
			case SoundName.HITROCK3:
				soundIndex = 9;
			break;
			case SoundName.HITROCK4:
				soundIndex = 10;
			break;
			case SoundName.HITROCK5:
				soundIndex = 11;
			break;
			case SoundName.HITROCK6:
				soundIndex = 12;
			break;
			case SoundName.HITROCK7:
				soundIndex = 13;
			break;
			case SoundName.VOICE1:
				soundIndex = 14;
			break;
			case SoundName.VOICE2:
				soundIndex = 15;
			break;
			case SoundName.VOICE3:
				soundIndex = 16;
			break;
			case SoundName.VOICE4:
				soundIndex = 17;
			break;
			case SoundName.VOICE5:
				soundIndex = 18;
			break;
			case SoundName.VOICE6:
				soundIndex = 19;
			break;
			case SoundName.VOICE7:
				soundIndex = 20;
			break;
			case SoundName.VOICE8:
				soundIndex = 21;
			break;
			case SoundName.VOICE9:
				soundIndex = 22;
			break;
			case SoundName.VOICE10:
				soundIndex = 23;
			break;
			case SoundName.VOICE11:
				soundIndex = 24;
			break;
			case SoundName.VOICE12:
				soundIndex = 25;
			break;
			case SoundName.VOICE13:
				soundIndex = 26;
			break;
			case SoundName.VOICE14:
				soundIndex = 27;
			break;
			case SoundName.VOICE15:
				soundIndex = 28;
			break;
			case SoundName.VOICE16:
				soundIndex = 29;
			break;
			case SoundName.VOICE17:
				soundIndex = 30;
			break;
			case SoundName.VOICE18:
				soundIndex = 31;
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
