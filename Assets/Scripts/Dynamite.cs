﻿using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {
    #region Enums publics
    public enum Type {
        LINEAR, // Les blocs adjacents
        CLOSE   // Les blocs ajacents + en diagonale
    }
    #endregion

    #region Attributs publics
    public int countdown = 5;
    public GameObject explosionPrefab;
    public LayerMask layerMaskPlayer;
    public LayerMask layerMaskBlock;
    public AnimationCurve BlinkSpeed;
    public Gradient BlinkGradient;

	float truecooldown=0,maxcooldown=0;
    #endregion

    #region Attributs privés
    public Type type;
    private float blink = 0f;
    #endregion

    #region Méthodes privées
    IEnumerator Boom () {
        yield return new WaitForSeconds (countdown);
		/*Vector3 partpos = new Vector3 (transform.position.x, 1.5f, transform.position.z);
        GameObject explosion = Instantiate (explosionPrefab, partpos, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);*/
        ParticleManager.Instance.Boom (transform.position);
        BlocksManager.Instance.Detonate (transform.position, type);
		SoundManager.Instance.PlaySound (SoundManager.SoundName.EXPLOSION);
        Destroy (gameObject);
    }

	Material matdynamite;

	
    void Start () 
	{
		truecooldown = (float)countdown;
		maxcooldown = truecooldown;

        StartCoroutine ("Boom");
		matdynamite = (Material)Instantiate(transform.GetChild (0).GetComponent<Renderer> ().material);
		transform.GetChild(0).GetComponent<Renderer>().material=matdynamite;
    }

	void FixedUpdate()
	{
		truecooldown = Mathf.Max (0f, truecooldown - Time.fixedDeltaTime);

        blink = (blink + Time.fixedDeltaTime * BlinkSpeed.Evaluate (1f - truecooldown / maxcooldown)) % 1f;

		float lum = 0.5f + Mathf.Sin (blink * Mathf.PI * 2f) * 0.5f;

		matdynamite.SetColor("_EmissionColor",Color.Lerp(Color.black, BlinkGradient.Evaluate(1f-truecooldown/maxcooldown),lum));
	}


    #endregion
}
