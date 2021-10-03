using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class monsterSpawn : MonoBehaviour
{
    [SerializeField] private Vector2 pos;
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private List<Button> btn;
    public GameObject[] items;
    public int itemCopies = 5;
    public Transform itemContainer;
    private int itemToSpawn;
    private Text points;
    // Start is called before the first frame update
    void Start()
    {
        SpawnItem(itemCopies);
        points = GameObject.Find("puntaje").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn(int i)
    {
        int lastpoint = int.Parse(points.text);
        if (i == 0 && (lastpoint - 3) < 0)
        {
            btn[i].GetComponent<Image>().color = Color.red;
            btn[i].enabled = false;
            StartCoroutine(restoreButtom(i));
            return;
        }
         if (i == 1 && (lastpoint - 5) < 0)
        {
            btn[i].GetComponent<Image>().color = Color.red;
            btn[i].enabled = false;
            StartCoroutine(restoreButtom(i));
            return;
        }
        if (i == 2 && (lastpoint - 10) < 0)
        {
            btn[i].GetComponent<Image>().color = Color.red;
            btn[i].enabled = false;
            StartCoroutine(restoreButtom(i));
            return;
        }

        selectItem(i);
        itemContainer.GetChild(itemToSpawn).gameObject.SetActive(true);
        itemContainer.GetChild(itemToSpawn).transform.position = pos;
        btn[i].GetComponent<Image>().color = Color.red;
        btn[i].enabled = false;
        StartCoroutine(restoreButtom(i));
        //Instantiate(prefabs[i], pos, prefabs[i].transform.rotation);

    }

    IEnumerator restoreButtom(int i)
    {
        yield return new WaitForSeconds(2.3f);
        btn[i].GetComponent<Image>().color = Color.white;
        btn[i].enabled = true;
    }

    void SpawnItem(int numberEnemy)
    {
        for (int a = 0; a < numberEnemy; a++) { 
        for (int i = 0; i < prefabs.Count; i++)
        {
            GameObject item = Instantiate(prefabs[i], pos, Quaternion.identity);
            item.transform.parent = itemContainer;
            item.SetActive(false);
        }
    }
    }

    void selectItem(int tipo)
    {
        bool aux = true;
        int safe = 0;
        while (aux)
        {
            safe++;
            if (safe >= 80) aux = false;
            itemToSpawn = Random.Range(0, itemContainer.childCount);
            if(tipo == 0) { 
            if (itemContainer.GetChild(itemToSpawn).gameObject.tag == "hongo")
            {
                if (itemContainer.GetChild(itemToSpawn).gameObject.activeSelf == false)
                {
                        aux = false;
                        int lastpoint = int.Parse(points.text);
                        points.text = $"{lastpoint - 3}";
                    }
            }
            }
            else if (tipo == 1)
            {
                if (itemContainer.GetChild(itemToSpawn).gameObject.tag == "camaleon")
                {
                    if (itemContainer.GetChild(itemToSpawn).gameObject.activeSelf == false)
                    {
                        aux = false;
                        int lastpoint = int.Parse(points.text);
                        points.text = $"{lastpoint - 5}";
                    }
                }
            }
            else if (tipo == 2)
            {
                if (itemContainer.GetChild(itemToSpawn).gameObject.tag == "pajaro")
                {
                    if (itemContainer.GetChild(itemToSpawn).gameObject.activeSelf == false)
                    {
                        aux = false;
                        int lastpoint = int.Parse(points.text);
                        points.text = $"{lastpoint - 10}";
                    }
                }
            }
        }
        //decidir que item al azar se va a generar
    }
}
