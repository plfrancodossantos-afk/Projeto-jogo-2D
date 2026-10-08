using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Zumbi")]
    public GameObject zombiePrefab;

    [Header("Limite")]
    public int maxZombies = 4;

    [Header("Spawn")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    private int quantidadeParaSpawnar = 2;

    private bool usarSpawnPoint1 = true;

    void Start()
    {
        GameObject ponto1 =
            GameObject.Find("SpawnPoint1");

        GameObject ponto2 =
            GameObject.Find("SpawnPoint2");

        if (ponto1 != null)
        {
            spawnPoint1 =
                ponto1.transform;
        }
        else
        {
            Debug.LogError(
                "Não encontrei o SpawnPoint1!"
            );
        }

        if (ponto2 != null)
        {
            spawnPoint2 =
                ponto2.transform;
        }
        else
        {
            Debug.LogError(
                "Não encontrei o SpawnPoint2!"
            );
        }
    }

    public void SpawnWave()
    {
        if (zombiePrefab == null)
        {
            Debug.LogError(
                "Zombie Prefab não foi colocado!"
            );

            return;
        }

        int zombiesVivos =
            GameObject.FindGameObjectsWithTag(
                "Enemy"
            ).Length;

        int espacosDisponiveis =
            maxZombies -
            zombiesVivos;

        if (espacosDisponiveis <= 0)
            return;

        int quantidade =
            Mathf.Min(
                quantidadeParaSpawnar,
                espacosDisponiveis
            );

        for (int i = 0; i < quantidade; i++)
        {
            Transform ponto;

            if (usarSpawnPoint1)
            {
                ponto = spawnPoint1;
            }
            else
            {
                ponto = spawnPoint2;
            }

            if (ponto != null)
            {
                Instantiate(
                    zombiePrefab,
                    ponto.position,
                    Quaternion.identity
                );
            }

            // Alterna entre os dois lados
            usarSpawnPoint1 =
                !usarSpawnPoint1;
        }

        // Aumenta o tamanho das próximas ondas
        if (quantidadeParaSpawnar < 4)
        {
            quantidadeParaSpawnar++;
        }
    }
}