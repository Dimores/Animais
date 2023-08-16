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
            // Calcula a nova posi��o do lobo, movendo-se gradualmente em dire��o � presa
            Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // Atualiza a posi��o do lobo
            body.MovePosition(newPosition);

            onRotate();

            // Se o lobo alcan�ou a presa
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                Destroy(target.gameObject); // Destruir a presa quando o lobo a alcan�ar
                target = null; // Resetar o alvo para evitar intera��es repetidas
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
            // Calcula a dire��o do alvo a partir da posi��o atual do lobo
            Vector3 directionToTarget = target.position - transform.position;
            directionToTarget.y = 0; // Garante que a rota��o seja apenas horizontal

            if (directionToTarget != Vector3.zero)
            {
                // Calcula a rota��o desejada para olhar na dire��o do alvo
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

                // Adiciona 180 graus � rota��o desejada para corrigir a orienta��o
                //targetRotation *= Quaternion.Euler(0, 180, 0);

                // Rotaciona gradualmente o lobo em dire��o � rota��o desejada
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
            }
        }
    }

}


