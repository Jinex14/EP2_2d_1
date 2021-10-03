using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb2d;
    private bool canMove = false;
    [SerializeField] private int lifeI;
    private int life;
    private Text points;
    private float distance;
    private float aux = 0;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        life = lifeI;
        points = GameObject.Find("puntaje").GetComponent<Text>();
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(starMove());
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) rb2d.velocity = new Vector2(speed, 0);
    }

    IEnumerator starMove()
    {
        yield return new WaitForSeconds(1.5f);
        canMove = true;
    }
    private void FixedUpdate()
    {
        distance = Vector2.Distance(Tower.instance.transform.position, transform.position);

        if (distance <= 3)
        {
            aux += Time.deltaTime;
            if(aux >= 0.4f)
            {
                Tower.instance.shoot();
                aux = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tower"))
        {
            gameObject.SetActive(false);
            int random = Random.Range(3, 6);
            for (int a = 0; a < random; a++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }

            transform.position = Vector2.zero;
            Tower.instance.loseLife();
          
            int lastpoint = int.Parse(points.text);

            if (lastpoint + random > 999)
            {
                points.text = $"{999}";
            }
            else
            {
                points.text = $"{lastpoint + random}";
            }
        }
        if (collision.CompareTag("bullet"))
        {
            collision.gameObject.SetActive(false);
            life--;
            if (life <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
    private void OnDisable()
    {
        life = lifeI;
        aux = 0;
    }
}
