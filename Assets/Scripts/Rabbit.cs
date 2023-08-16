using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour, IPrey
{
    [SerializeField] private float speed;


    Rigidbody body;

    public AudioClip deathSound; // Audio clip a ser tocado quando o jogador coleta a po��o
    private AudioSource audioSource;

    public void Flee(IPredator predator)
    {
        Debug.Log("O coelho est� fugindo do predador!");
        // L�gica de fuga aqui
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lobo") || collision.gameObject.CompareTag("urso"))
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position); // Toca o �udio da po��o
            Destroy(this.gameObject);
        }
    }
}
