using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class katakuriController : MonoBehaviour
{ 
      
    //---------------------
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private float vidaKatakuri, vidaKatakuriActual = 1;
    private float tiempoDetectar = 4,cuentaBajo;
    private float tiempoTeleport = 3, cuentaBajoTeleport;
    private luffyController luffy;
    private float tiempoAtacar=0;
    private int contador = 0;

    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_LANZA = 1;
    private const int ANIMATION_PISOTON = 2;
    private const int ANIMATION_ATTACKDISTANCIA= 3;
    private const int ANIMATION_PUÑO = 4;
    private const int ANIMATION_RAFAGA_PUÑO=5;

 
    public Image barraSaludKatakuriImg;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    private bool estaSaltando=false;
    private bool retroceder=false;
    private bool estagolpeando=false;
    private bool golpe1=false;
    private bool golpe2 = false;
    private bool golpe3 = false;
    public string DirecJugador;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        luffy = luffyController.instance;
        cuentaBajo = tiempoDetectar;
        cuentaBajoTeleport = tiempoTeleport;
        ubicarPlayer();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        contar_ataque();

        if (golpe1==true)
        {
            CambiarAnimacion(ANIMATION_PUÑO);
            audioSource.PlayOneShot(audioClips[0]);
        }
        if (golpe2==true)
        {
            CambiarAnimacion(ANIMATION_RAFAGA_PUÑO);
        }
        if (golpe3==true)
        {
            CambiarAnimacion(ANIMATION_ATTACKDISTANCIA);
        }
        if (retroceder==true)
        {
            rb.velocity = new Vector2(2,rb.velocity.y);
        }
        if (estagolpeando== false)
        {
            CambiarAnimacion(ANIMATION_QUIETO);
        }
         else
        {   
            CambiarAnimacion(ANIMATION_LANZA); 
           audioSource.PlayOneShot(audioClips[0]);
            rb.velocity = new Vector2(-1,rb.velocity.y);
           
        }

        //if (contador==3)
        //{
        //    CambiarAnimacion(ANIMATION_PISOTON);
        //    rb.velocity = new Vector2(-1,rb.velocity.y);
        //    audioSource.PlayOneShot(audioClips[0]);
        //}
        //if (contador == 5)
        //{
        //   CambiarAnimacion(ANIMATION_ATTACKDISTANCIA);
        //    audioSource.PlayOneShot(audioClips[0]);
        //    rb.velocity = new Vector2(-1, rb.velocity.y);
        //}
        //if (contador==7)
        //{
        //    CambiarAnimacion(ANIMATION_PUÑO);
        //    audioSource.PlayOneShot(audioClips[0]);
        //    rb.velocity = new Vector2(-1, rb.velocity.y);
        //}

        //if (Input.GetKey(KeyCode.J)  )
        //        {
        //            rb.velocity = Vector2.up * 5;
                    
                    //StartCoroutine("Esperar");
                   // CambiarAnimacion(ANIMATION_PISOTON);
                 //   estaSaltando=true;
                //} 
    }

    private void contar_ataque()
    {
        cuentaBajo -= Time.deltaTime;
        cuentaBajoTeleport -= Time.deltaTime;

        if (cuentaBajo<=0f)
        {
            KaatakuriAtaca();
            ubicarPlayer();
            cuentaBajo = tiempoDetectar;
           
        }
        if (cuentaBajoTeleport<=0f)
        {
            ubicarPlayer();
            cuentaBajoTeleport = tiempoTeleport;
        }
    }

    public void DañoRecibeKatakuri()
    {
        vidaKatakuri = 0.2f;
        vidaKatakuriActual = vidaKatakuriActual - vidaKatakuri;
        barraSaludKatakuriImg.fillAmount = vidaKatakuriActual;
    }
    IEnumerator Esperar(){
        yield return new WaitForSecondsRealtime((3/2));
    }
    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }

    public void KaatakuriAtaca()
    {
        var distancia = transform.position.x - luffyController.instance.transform.position.x;
        
        if (distancia < 3)
        {
            golpe1 = true;
            Debug.Log("golpe1");
        }
        else if (distancia < 6)
        {
            golpe2 = true;
            Debug.Log("golpe2");
        }
        else if (distancia < 9)
        {
            golpe3 = true;
            Debug.Log("golpe3");
        }
        else if (distancia > 12)
        {
            estagolpeando = true;
            Debug.Log("golpe distancias");
        }
        else
        {
            estagolpeando = false;
        }
    }
    private void ubicarPlayer()
    {
        if (transform.position.x > luffyController.instance.transform.position.x)
        {
            transform.localScale = new Vector3(1.6678f, 1.0157f, 1);
            Debug.Log("esta a la izquierda");
            DirecJugador = "izquierdo";
        }
        else
        {
            transform.localScale = new Vector3(-1.6678f, 1.0157f, 1);
            Debug.Log("esta a la derecha");
           DirecJugador = "derecha";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "A")
        {
          //  estagolpeando = true;
          //  contador++;
          //  if (contador==2 || contador==4 ||contador==6 || contador==8)
          //  {
         //       estagolpeando = false;
         //       retroceder = true;

          //      if (contador==9)
          //      {
           //         contador--;
           //         Debug.Log(contador);
           //     }
          //  }
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="A")
        {
            Debug.Log("collision_personaje");
            estagolpeando = false;
            DañoRecibeKatakuri();

            if (vidaKatakuriActual<=0)
            {
                Destroy(this.gameObject);
                Debug.Log("muerte");
            }
        }
    }
}
