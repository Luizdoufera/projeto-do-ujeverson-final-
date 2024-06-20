using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;


public class movimentaçãodacamera : MonoBehaviour
{
   [SerializeField] private float velocidadedeaprocimação;
   [SerializeField] private float velocidadederotação;

   [SerializeField] private Vector3 offset;

   [SerializeField] private Transform seguirobjeto;

   private Vector3 posicaodoobjeto; 

   private void FixedUpdate()
   {
     seguircarro();
   }
   
   private void seguircarro()
   {
    posicaodoobjeto = seguirobjeto.TransformPoint(offset);

    transform.position = Vector3.Lerp(transform.position, posicaodoobjeto, velocidadedeaprocimação * Time.deltaTime);

    Vector3 distaciaateobjeto = seguirobjeto.position - transform.position;
    Quaternion rotaçaodecamera =Quaternion.LookRotation(distaciaateobjeto, Vector3.up);

    transform.rotation= Quaternion.Lerp(transform.rotation, rotaçaodecamera,  velocidadederotação * Time.deltaTime);
   }
   

}
