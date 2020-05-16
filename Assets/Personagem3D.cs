using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personagem3D : MonoBehaviour
{
    private Animation anim;
    public GameObject character;
    private Vector3 moveDirector;

    public float speed = 6.0f;          //velocidade
    public float jumpSpeed =8.0f;       //velocidade do pulo
    public float gravity =20.0f;        //gravidade
    private CharacterController cc;     //recebe os valores de XYZ
    
    void Start()
    {
        //chamando os componentes
        cc = GetComponent < CharacterController > ();
        anim = character.GetComponent<Animation>();
    }

    void Update()
    {   //verifica colisao com o chao
        if (cc.isGrounded)
        {
            //input mover
            moveDirector = new Vector3(Input.GetAxis("Horizontal"),0.0f,Input.GetAxis("Vertical"));
        }
        
        //atualiza o character controller
        moveDirector = transform.TransformDirection(moveDirector);
        moveDirector = moveDirector * speed;

        //input pulo
        if (Input.GetButtonDown("Jump"))
        {
            moveDirector.y = jumpSpeed;
        }
        
        //gravidade
        moveDirector.y -= gravity * Time.deltaTime;

        //atribuir valores construidos dos inputs (pulo mover gravidade) no character controller
        cc.Move(moveDirector * Time.deltaTime);

        //orientacao personagem
        if(Input.GetAxis("Horizontal") < 0.0f)
        {
            character.transform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        }
        else if(Input.GetAxis("Horizontal") > 0.0f)
        {
            character.transform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
        }
        else 
        {
            character.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
        }

        //animacao
        if(moveDirector.x == 0.0f && moveDirector.z == 0.0f)
        {
            anim.CrossFade("IDLE");
        }
        else if(Mathf.Abs(moveDirector.x) > 0.0f || Mathf.Abs(moveDirector.y) > 0.0f)
        {
            anim.CrossFade("RUN");
        }
    }
}

