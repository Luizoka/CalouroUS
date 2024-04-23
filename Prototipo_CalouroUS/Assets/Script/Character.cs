using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;
using System.Data.Common;

public class Character : MonoBehaviour
{
    Rigidbody2D body;
    float horizontal;
    float vertical;
    [SerializeField] public float runSpeed = 5.0f;

    public bool fantasma = true;
    public Animator animator;
    private bool viradoParaEsquerda = false;

    [SerializeField] private GameObject cenario;
    public float alfaBloqueado = 0f;
    private float alfaNormal = 1f;
    private Renderer cenarioRenderer;

    void Start()
    {
        this.body = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.cenarioRenderer = cenario.GetComponent<Renderer>();
        SetAlfa(alfaNormal);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        float inputAxis = Input.GetAxis("Horizontal");

        if (inputAxis < 0 && !this.viradoParaEsquerda)
        {
            Flip();
        }
        else if (inputAxis > 0 && this.viradoParaEsquerda)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
        viradoParaEsquerda = !viradoParaEsquerda;
    }

    public void Mudanca()
    {
        if (this.fantasma == false)
        {
            Morto();
        }
        else if (this.fantasma == true)
        {
            Vivo();
        }
    }

    public void Morto()
    {
        animator.SetBool("Death", true);
        fantasma = true;
    }

    public void Vivo()
    {
        animator.SetBool("Death", false);
        fantasma = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "locked")
        {
            // Define o alfa para 0 quando o objeto estiver bloqueado
            SetAlfa(alfaBloqueado);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "locked")
        {
            // Retorna o alfa ao valor normal quando o objeto não estiver mais bloqueado
            SetAlfa(alfaNormal);
        }
    }

     private void SetAlfa(float alfa)
    {
        // Verifica se o componente de renderização existe
        if (cenarioRenderer != null)
        {
            // Define o alfa do material do cenario
            Material material = cenarioRenderer.material;
            Color cor = material.color;
            cor.a = alfa;
            material.color = cor;
        }
        else
        {
            Debug.LogWarning("O componente de renderização do cenario não foi encontrado!");
        }
    }
}
