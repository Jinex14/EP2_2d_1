using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Vector2 pos;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int life = 10;
    public static Tower instance;
    public Transform itemContainer;
    private int itemToSpawn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnItem(30);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }

    public void loseLife()
    {
        life--;
        if (life <= 0)
            StartCoroutine(killTower());
    }

    IEnumerator killTower()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Application.LoadLevel(Application.loadedLevel);
    }

    void SpawnItem(int numberEnemy)
    {
        for (int a = 0; a < numberEnemy; a++)
        {
                GameObject item = Instantiate(prefab, pos, Quaternion.identity);
                item.transform.parent = itemContainer;
                item.SetActive(false);
        }
    }

    void selectItem()
    {
        //decidir que item al azar se va a generar
        itemToSpawn = Random.Range(0, itemContainer.childCount);
    }

    public void shoot()
    {
        selectItem();
        itemContainer.GetChild(itemToSpawn).gameObject.SetActive(true);
        itemContainer.GetChild(itemToSpawn).transform.position = pos;
    }
}
