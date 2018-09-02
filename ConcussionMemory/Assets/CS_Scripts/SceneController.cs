using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    [SerializeField] private MemoryCard originalCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreText;

    public const int gridRows = 2;
    public const int gridCols = 4;
    public const float offSetX = 2f;
    public const float offSetY = 2.5f;

    private MemoryCard _firstReveal;
    private MemoryCard _secondReveal;

    private int _score = 0;


	// Use this for initialization
	void Start () {
        Vector3 startingPos = originalCard.transform.position;

        int[] shuffleIndices = { 0, 0, 1, 1, 2, 2, 3, 3 };
        shuffleIndices = Shuffle(shuffleIndices);

        for (int col = 0; col < gridCols; col++) {
            for (int row = 0; row < gridRows; row++) {
                // Set the card
                MemoryCard card;
                if (row == 0 && col == 0) {
                    card = originalCard;
                }
                else {
                    card = Instantiate(originalCard) as MemoryCard;
                }

                int index = row * gridCols + col;
                int id = shuffleIndices[index];
                card.SetCard(id, images[id]);

                // Position the card
                float posX = (offSetX * col) + startingPos.x;
                float posY = -(offSetY * row) + startingPos.y;
                card.transform.position = new Vector3(posX, posY, startingPos.z);
            }
        }
	}

    public bool canReveal {
        get {return _secondReveal == null;}
    }

    public void CardRevealed(MemoryCard card) {
        if (_firstReveal == null) {
            _firstReveal = card;
        }
        else {
            _secondReveal = card;
            StartCoroutine(CheckMatch());
        }
    }

    public void Restart() {
        SceneManager.LoadScene("SampleScene");
    }

    private int[] Shuffle(int[] arr) {
        int[] newArray = arr.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++) {
            int temp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = temp;
        }
        return newArray;
    }

    private IEnumerator CheckMatch() {
        if (_firstReveal.id == _secondReveal.id) {
            _score += 1;
            scoreText.text = "Score: " + _score;

        }
        else {
            yield return new WaitForSeconds(1.0f);
            _firstReveal.Unreveal();
            _secondReveal.Unreveal();
        }
        _firstReveal = null;
        _secondReveal = null;
    }
	
}
