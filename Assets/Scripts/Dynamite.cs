using UnityEngine;
using System.Collections;

public class Dynamite : MonoBehaviour {
    #region Attributs publics
    public int countdown = 5;
    public GameObject explosionPrefab;
    public LayerMask layerMaskPlayer;
    public LayerMask layerMaskBlock;

	float truecooldown=0,maxcooldown=0;
    #endregion

    #region Méthodes privées
    IEnumerator Boom () {
        yield return new WaitForSeconds (countdown);
        GameObject explosion = Instantiate (explosionPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy (explosion, 2);

        #region Destruction des blocs alentours
        RaycastHit hit;
        if (Physics.Raycast (transform.position, Vector3.forward, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        if (Physics.Raycast (transform.position, -Vector3.forward, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        if (Physics.Raycast (transform.position, -Vector3.right, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        if (Physics.Raycast (transform.position, Vector3.right, out hit, 1, layerMaskBlock)) {
            hit.collider.gameObject.GetComponent<Block> ().Die ();
        }
        #endregion

        #region Player stun
        Debug.Log (Vector3.Distance (transform.position, GameManager.Instance.player.transform.position));
        if (Vector3.Distance (transform.position, GameManager.Instance.player.transform.position) <= 1.5f) {
            GameManager.Instance.player.Stuned = true;
        }
        #endregion

        Destroy (gameObject);
    }

	Material matdynamite;

	float blink=0f;

	public AnimationCurve BlinkSpeed;
	public Gradient BlinkGradient;

    void Start () 
	{
		truecooldown = (float)countdown;
		maxcooldown = truecooldown;

        StartCoroutine ("Boom");
		matdynamite = (Material)Instantiate(transform.GetChild (0).GetComponent<Renderer> ().material);
		transform.GetChild(0).GetComponent<Renderer>().material=matdynamite;
    }

	void Update()
	{
		truecooldown = Mathf.Max (0f, truecooldown - Time.deltaTime);

		blink = (blink + Time.deltaTime*BlinkSpeed.Evaluate(1f-truecooldown/maxcooldown)) % 1f;

		float lum = 0.5f + Mathf.Sin (blink * Mathf.PI * 2f) * 0.5f;

		matdynamite.SetColor("_EmissionColor",Color.Lerp(Color.black, BlinkGradient.Evaluate(1f-truecooldown/maxcooldown),lum));
	}


    #endregion
}
