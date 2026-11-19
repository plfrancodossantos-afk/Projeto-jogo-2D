
using UnityEngine; 

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // guarda o prefab do raio

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // ve se o jogador apertou a tecla E
        {
            GameObject bala = Instantiate( //cria um novo raio
                bulletPrefab, //define qual raio será criado
                transform.position, //define a posição inicial do raio
                Quaternion.identity // Mantém a rotação padrão do raio
            );

            float direcao = transform.localScale.x >= 0 ? 1f : -1f; // ve o lado q o jogador ta olhando

            bala.GetComponent<Bullet>().SetDirection(direcao); // envia a direção para o script "Bullet"
        }
    }
}
