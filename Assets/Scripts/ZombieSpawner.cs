
using UnityEngine; 

public class ZombieSpawner : MonoBehaviour 
{
    public GameObject zombiePrefab; //guarda o prefab usado para criar os zumbis

    public int maxZombies = 4; //define o máximo de zumbis vivos ao mesmo tempo

    public Transform spawnPoint1; // guarda o primeiro ponto de nascimento
    public Transform spawnPoint2; // guarda o segundo ponto de nascimento

    private int quantidadeParaSpawnar = 2; //define quantos zumbis aparecem na primeira onda

    private bool usarSpawnPoint1 = true; //controla qual ponto será usado no próximo nascimento

    void Start() 
    {
        GameObject ponto1 = GameObject.Find("SpawnPoint1"); //procura o primeiro ponto pelo nome

        GameObject ponto2 = GameObject.Find("SpawnPoint2"); //procura o segundo ponto pelo nome

        if (ponto1 != null) //verifica se encontrou o primeiro ponto
        {
            spawnPoint1 = ponto1.transform; //guarda a posição do primeiro ponto
        }
        else //executa se não encontrou o primeiro ponto
        {
            Debug.LogError("Não encontrei o SpawnPoint1!"); //mostra um erro no console
        }

        if (ponto2 != null) //verifica se encontrou o segundo ponto
        {
            spawnPoint2 = ponto2.transform; //guarda a posição do segundo ponto
        }
        else // executa se não encontrou o segundo ponto
        {
            Debug.LogError("Não encontrei o SpawnPoint2!"); //mostra um erro no console
        }
    }

    public void SpawnWave() // cria uma nova onda de zumbis
    {
        if (zombiePrefab == null) // verifica se o prefab foi associado
        {
            Debug.LogError("Zombie Prefab não foi colocado!"); //mostra um erro no console
            return; //cancela o nascimento dos zumbis
        }

        int zombiesVivos = GameObject.FindGameObjectsWithTag("Enemy").Length; //conta os zumbis vivos na cena

        int espacosDisponiveis = maxZombies - zombiesVivos; // calcula quantos zumbis ainda podem aparecer

        if (espacosDisponiveis <= 0) // verifica se o limite já foi atingido
            return; // impede o nascimento de mais zumbis

        int quantidade = Mathf.Min(quantidadeParaSpawnar, espacosDisponiveis); // define quantos zumbis podem aparecer nesta onda

        for (int i = 0; i < quantidade; i++) // repete o processo para cada zumbi da onda
        {
            Transform ponto = usarSpawnPoint1 ? spawnPoint1 : spawnPoint2; // escolhe um dos dois pontos de nascimento

            if (ponto == null) // verifica se o ponto escolhido n existe
            {
                Debug.LogError("Um dos SpawnPoints não foi encontrado!"); // mostra um erro no console
                return; // iterrompe a criação desta onda
            }

            Instantiate(zombiePrefab, ponto.position, Quaternion.identity); //aparece um zumbi na posição escolhida

            usarSpawnPoint1 = !usarSpawnPoint1; //alterna o próximo nascimento para o outro ponto
        }

        if (quantidadeParaSpawnar < 4) // ve se a quantidade da onda ainda pode aumentar
        {
            quantidadeParaSpawnar++; // aumenta os zumbis na proxima onda
        }
    }
}
