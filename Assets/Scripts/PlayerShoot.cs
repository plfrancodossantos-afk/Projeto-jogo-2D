using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameObject bala = Instantiate(
                bulletPrefab,
                transform.position,
                Quaternion.identity
            );

            float direcao = transform.localScale.x >= 0 ? 1f : -1f;
            bala.GetComponent<Bullet>().SetDirection(direcao);
        }
    }
}