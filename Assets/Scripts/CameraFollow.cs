
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public Transform alvo; // define o objeto que a câmera deve acompanhar
    public Vector3 distancia = new Vector3(0f, 0f, -10f); //define a distância da câmera do jogador
    public bool movimentoSuave = true; // permite escolher se a câmera vai ter movimento suave 
    public float velocidade = 5f; // define a velocidade do acompanhamento da câmera

    private void LateUpdate() // executa depois que os outros objetos terminam de se movimentar
    {
        Vector3 posicaoDesejada = alvo.position + distancia; // ve onde a câmera deve ficar

        if (movimentoSuave) // verifica se o movimento suave está ativado
        {
            transform.position = Vector3.Lerp(transform.position, posicaoDesejada, velocidade * Time.deltaTime); // mover a câmera suave até a posição desejada
        }
        else // executar quando o movimento suave tiver desativado
        {
            transform.position = posicaoDesejada; //move a câmera para a posição desejada
        }
    }
}
