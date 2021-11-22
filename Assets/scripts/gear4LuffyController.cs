using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gear4LuffyController : MonoBehaviour
{
        public float velocidad = 10;
    private const int ANIMATION_QUIETO = 0;
    private const int ANIMATION_VOLAR = 1;
    private const int ANIMATION_PUÑO = 3;
    private const int ANIMATION_CAE= 2;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKey(KeyCode.D))//Si presiono la tecla rigtharrow voy a ir hacia la derecha
            {
                rb.velocity = new Vector2(velocidad, rb.velocity.y);//velocidad de mi objeto
                CambiarAnimacion(ANIMATION_VOLAR);//Accion correr 
                spriteRenderer.flipX = false;//Que mi objeto mire hacia la derecha
                
                /*if (Input.GetKey(KeyCode.Space) && !EstaSaltando)
                {
                    CambiarAnimacion(ANIMATION_SALTAR);
                    Saltar();
                    EstaSaltando = true;
                }*/
            }
            
            else if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-velocidad, rb.velocity.y);
                CambiarAnimacion(ANIMATION_VOLAR);
                spriteRenderer.flipX = true;
                /*if(Input.GetKey(KeyCode.C))
                {
                    
                    CambiarAnimacion(ANIMATION_SLIDE);
                }
            */ 
            }else if (Input.GetKey(KeyCode.I))
            {
               CambiarAnimacion(ANIMATION_PUÑO);
            }else if (Input.GetKey(KeyCode.O))
            {
               CambiarAnimacion(ANIMATION_CAE);
            } else 
            {
                CambiarAnimacion(ANIMATION_QUIETO);//Metodo donde mi objeto se va a quedar quieto
                rb.velocity = new Vector2(0, rb.velocity.y);//Dar velocidad a nuestro objeto
            }
    }
      private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
}
