using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public const int gridRows = 2;
    public const int gridColumns = 4;
    public const float offsetX = 2f;
    public const float offsetY = 2.5f;



    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMeshPro textLabel;

    private MemoryCard firstRevealed;
    private MemoryCard secondRevealed;

    private int score =0;

    public bool CanReveal
    {
        get { return secondRevealed == null; }
    }

    public void CardRevealed(MemoryCard card)
    {
        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        int[] ids = { 0, 0, 1, 1, 2, 2, 3, 3 };
        ids = ShuffleArray(ids);

        Vector3 startPos = originalCard.transform.position;
        for(int i = 0; i < gridColumns; i++)
        {
            for(int j =0; j < gridRows; j++) 
            {
                MemoryCard card;

                if(i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MemoryCard; 
                }
                int index = j * gridColumns + i;
                int id = ids[index];
                card.SetCard(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = -(offsetY * j)+ startPos.y;

                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private int[] ShuffleArray(int[] array)
    {
        int[] result = array.Clone() as int[];

        for(int i=0; i < result.Length; i++)
        {
            int tmp = result[i];
            int rand = Random.Range(i, result.Length);
            result[i] = result[rand];
            result[rand] = tmp;
        }


        return result;
    }

    private IEnumerator CheckMatch()
    {
        if(firstRevealed.Id == secondRevealed.Id)
        {
            score++;
            textLabel.text = "Score:" + score;
        }
        else
        {
            yield return new WaitForSeconds(1.3f);
            firstRevealed.Unreveal();
            secondRevealed.Unreveal();
        }
        firstRevealed= null;
        secondRevealed= null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
