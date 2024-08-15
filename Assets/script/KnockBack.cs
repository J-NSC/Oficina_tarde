using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    // Start is called before the first frame update
    public jogador jogador;
    public float knockTempo;
    public float knockBackForca;

    void Awake()
    {
        jogador = GetComponent<jogador>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            var inimigo = other.GetComponent<Inimigo>();

            if (jogador.rgJogador.velocity.y < -1f)
            {
                jogador.rgJogador.AddForce(Vector2.up * knockBackForca, ForceMode2D.Impulse);
                inimigo.Morte();
            }
        }
    }
}
