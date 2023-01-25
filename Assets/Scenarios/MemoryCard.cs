using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{

    [SerializeField] private GameObject cardBack;
    //[SerializeField] private Sprite image;
    [SerializeField] private SceneController controller;

    private int id;
    public int Id
    {
        get { return id; }
    }

    public void SetCard(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.CanReveal)
        {        
            cardBack.SetActive(false);
            controller.CardRevealed(this);
        }
    }


    public void Unreveal()
    {
        cardBack.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
       // GetComponent<SpriteRenderer>().sprite = image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
