using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JogadorEstado
{
    parado  = 0, 
    andando = 1,
    pulando = 2,
    caindo  = 3,
    morot   = 4,
}


public class jogador : MonoBehaviour
{
    public JogadorEstado jogadorEstado;
    public Rigidbody2D rgJogador;
    public float dir;
    public float velocidade;
    public float forcaPulo;
    public bool estaChao;
    public Transform posicaoPe;
    public LayerMask chao;
    public float raio;
    public Animator jogadorAnimacao;
    public SpriteRenderer jogadorSprite;
    private void Awake()
    {
        rgJogador = GetComponent<Rigidbody2D>(); 
        jogadorAnimacao = GetComponent<Animator>();
        jogadorSprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        
    }

    void correr()
    {
        rgJogador.velocity = new Vector2(velocidade * dir, rgJogador.velocity.y);
    }
    void Update()
    {
        dir = Input.GetAxis("Horizontal");
        estaChao = Physics2D.OverlapCircle(posicaoPe.position,raio, chao);
        AtualizaEstado();
        AtualizaAnimacao();
        Flip();
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& estaChao)
            Pulo();
        correr();
       
    }
    void Pulo()
    {
        rgJogador.velocity = Vector2.up * forcaPulo;
        jogadorAnimacao.SetTrigger("Pulo");
    }

    void AtualizaEstado()
    {
        jogadorAnimacao.SetInteger("State", (int) jogadorEstado);
    }

    void AtualizaAnimacao()
    {
        if (dir != 0)
        {
            jogadorEstado = JogadorEstado.andando;
        }

        if (rgJogador.velocity.magnitude < 0.2f)
        {
            jogadorEstado = JogadorEstado.parado;
        }

        if (rgJogador.velocity.y < -1)
        {
            jogadorEstado = JogadorEstado.caindo;
        }
    }
    
    void Flip()
    {
        if (dir != 0)
        {
            jogadorSprite.flipX = dir < 0;
        }
    }

    private void OnDrawGizmos()
    {
       // Gizmos.color = Color.red;
        Gizmos.DrawSphere(posicaoPe.position, raio);
    }



}
