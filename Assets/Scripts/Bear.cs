using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Bear : MonoBehaviour, IPredator
{
    [SerializeField] private float speed;

    private Transform target;

    public float alcanceDaCaca;

    Rigidbody body;

    public void Hunt(IPrey prey)
    {
        target = prey.transform; // Define a presa como o alvo
    }

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        procurarPresa();


        if (target != null)
        {
            // Calcula a nova posição do lobo, movendo-se gradualmente em direção à presa
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Atualiza a posição do lobo
            body.MovePosition(newPosition);

            onRotate();

            // Se o lobo alcançou a presa
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(target.gameObject); // Destruir a presa quando o lobo a alcançar
                target = null; // Resetar o alvo para evitar interações repetidas
            }
        }
    }

    private void procurarPresa()
    {
        Collider[] areaAtaque = Physics.OverlapSphere(this.transform.position, alcanceDaCaca);

        float shortestDistance = Mathf.Infinity;
        IPrey closestPrey = null;

        foreach (Collider c in areaAtaque)
        {
            IPrey prey = c.GetComponent<IPrey>();
            if (prey != null)
            {
                float distanceToPrey = Vector3.Distance(transform.position, c.transform.position);
                if (distanceToPrey < shortestDistance)
                {
                    shortestDistance = distanceToPrey;
                    closestPrey = prey;
                }
            }
        }

        if (closestPrey != null)
        {
            Hunt(closestPrey);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, alcanceDaCaca);
    }

    private void onRotate()
    {
        if (target != null)
        {
            // Calcula a direção do alvo a partir da posição atual do lobo
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0; // Garante que a rotação seja apenas horizontal

            if (directionToTarget != Vector3.zero)
            {
                // Calcula a rotação desejada para olhar na direção do alvo
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                // Adiciona 180 graus à rotação desejada para corrigir a orientação
                //targetRotation *= Quaternion.Euler(0, 180, 0);

                // Rotaciona gradualmente o lobo em direção à rotação desejada
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
            }
        }
    }

}


