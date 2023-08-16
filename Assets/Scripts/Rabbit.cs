using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour, IPrey
{
    [SerializeField] private float speed;


    Rigidbody body;

    public AudioClip deathSound; // Audio clip a ser tocado quando o jogador coleta a poção
    private AudioSource audioSource;

    public void Flee(IPredator predator)
    {
        Debug.Log("O coelho está fugindo do predador!");
        // Lógica de fuga aqui
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
            AudioSource.PlayClipAtPoint(deathSound, transform.position); // Toca o áudio da poção
            Destroy(this.gameObject);
        }
    }
}
