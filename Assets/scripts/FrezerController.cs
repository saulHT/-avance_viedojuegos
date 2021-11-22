using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrezerController : MonoBehaviour
{
    private int velocidadmovimiento;
    private Rigidbody2D rb;
    private Vector2 DireccionAttack;
    private luffyController luffy;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform[] transforms;
    private float tiempoDetectar=4, cuentaBajo;
    private float tiempoTeleport=3, cuentaBajoTeleport;
    private float vidaFrezer, VidaActualFrezer;
    private int EstadoNormal=0;
    private int LanzarSupernova=1;
     private int LanzarRayoMortal=2;
    private Image barraSaludFrezerImg;
    public GameObject superN;
    public GameObject superND;
    public GameObject RayoMortalO;
    public GameObject RayoMortalOD;
    public bool EstaAtacando=false;
    public int cantSupernovas=1;
    // Start is called before the first frame update
    public string DirJugador;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        luffy= luffyController.instance;
        cuentaBajo=tiempoDetectar;
        cuentaBajoTeleport=tiempoTeleport;
        ubicarPlayer();
 
//        transform.position=transforms[transforms.Length].position;
        //mover ataque hacia el personaje
        //DireccionAttack =(luffy.transform.position- transform.position).normalized*velocidadmovimiento;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
          //supernova();
           
           EstaAtacando=true;
           if(EstaAtacando==true){
              CambiarAnimacion(LanzarRayoMortal);
              Invoke("RayoMortal", 1.0f/2);
              Invoke("isAttack",1.5f);
        
            
           }
         
        }
        if(Input.GetKeyDown(KeyCode.T)){
          //supernova();
           
           EstaAtacando=true;
           if(EstaAtacando==true){
              CambiarAnimacion(LanzarSupernova);
              Invoke("supernova", 1.0f/2);
              Invoke("isAttack",1f); 
           }
         
        }
        if(EstaAtacando==false){
          CambiarAnimacion(0);
        }
            contador();
       
        
    }
    public void FrezerAttack(){
        var distancia=transform.position.x-luffyController.instance.transform.position.x;
        if(distancia>15){
            EstaAtacando=true;
           if(EstaAtacando==true){
              CambiarAnimacion(LanzarSupernova);
             
                Invoke("supernova", 1.0f/2);
                Invoke("isAttack",1f); 
            
           }
           cantSupernovas=1;
        }else{
                EstaAtacando=true;
            if(EstaAtacando==true){
              CambiarAnimacion(LanzarRayoMortal);
              Invoke("RayoMortal", 1.0f/2);
              Invoke("isAttack",1.5f); 
           }
              cantSupernovas=1;
        }
    }
    public void isAttack(){
        EstaAtacando=false;  
    }
    private float countback=2;

   
    public void contador(){
        
        cuentaBajo-=Time.deltaTime;
        cuentaBajoTeleport-=Time.deltaTime;
        if (cuentaBajo<=0f)
        {
             FrezerAttack();
            ubicarPlayer();
            cuentaBajo=tiempoDetectar;
                cantSupernovas--;

        }
        if (cuentaBajoTeleport<=0f)
        {
             
            ubicarPlayer();
            cuentaBajoTeleport=tiempoTeleport;
 
        }
    
    
    }
    private void CambiarAnimacion(int animacion)
    {
        animator.SetInteger("Estado", animacion);
    }
    public void ubicarPlayer(){
 
        if (transform.position.x> luffyController.instance.transform.position.x)
        {
            transform.localScale= new Vector3(0.6706f,0.6034f,1);
            Debug.Log("esta a la izquierda");
            DirJugador="izquierda";
        
        }else
        {
            transform.localScale= new Vector3(-0.6706f,0.6034f,1);
              Debug.Log("esta a la derecha");
                DirJugador="derecha";
             
         }
 
    }
    public void telepor(){
        var initialPosition= UnityEngine.Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;
        cuentaBajo = tiempoDetectar;
        cuentaBajoTeleport =tiempoTeleport;
    }
    public void RayoMortal(){
       
        if (spriteRenderer.flipX&&DirJugador=="izquierda")
            {
               // DireccionAttack =(luffy.transform.position- transform.position).normalized*velocidadmovimiento;
                var position = new Vector2(transform.position.x -8f , transform.position.y  );
                Instantiate(RayoMortalO, position, RayoMortalO.transform.rotation);
          
            }
            else
            { 
                 //DireccionAttack =(luffy.transform.position- transform.position).normalized*velocidadmovimiento;
                var position = new Vector2(transform.position.x +8f , transform.position.y );
                Instantiate(RayoMortalOD, position, RayoMortalOD.transform.rotation);
              
            }
          
     
    }
    public void supernova(){
        
          if (spriteRenderer.flipX&&DirJugador=="izquierda")
                    {

                        var position = new Vector2(transform.position.x- 2.5f, transform.position.y + .5f);
                        Instantiate(superN, position, superN.transform.rotation);
                    }
                    else
                    {
                        var position = new Vector2(transform.position.x + 2.5f, transform.position.y + .5f);
                        Instantiate(superND, position, superND.transform.rotation);
                    }
    }
    public void DañoRecibeFrezer()
    {
        //vidaFrezer=GetComponent<FrezerController>
        barraSaludFrezerImg.fillAmount = VidaActualFrezer/2;
    }
}
