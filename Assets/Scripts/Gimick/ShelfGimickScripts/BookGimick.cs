using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

// ToDo:本の位置を保存．謎を解いたかbool値で保存

public class BookGimick : MonoBehaviour
{
    [SerializeField] private GameObject[] particles = new GameObject[6];   // どの本を選択したかわかるように，選択中の本にだけパーティクルを表示
    [SerializeField] private GameObject[] books = new GameObject[6];
    [SerializeField] private GameObject boxCoverPivot;

    [SerializeField] private BoxCollider _boxCoverCollider;
    private SelectBook[] _selectBooks = new SelectBook[6];
    [Header("正しい色の並び順を入力")] 
    [SerializeField] private BookColors[] correctColor = new BookColors[6];
    private ShowTextMessage _showTextMessage;
    private bool _isSelectingBook = false;
    private BookColors _firstBookColors;
    private BookColors _secondBookColors;
    private int _firstIndex, _secondIndex;
    private bool _isSolved = false; // 謎を解いたか
    
    void Awake()
    {
        _showTextMessage = GetComponent<ShowTextMessage>();
        for (int i = 0; i < books.Length; i++)
        {
            _selectBooks[i] = books[i].GetComponent<SelectBook>();
        }
    }
    
    public void MoveBook(BookColors color)
    {
        if (_isSolved) { return; }
        if(!_isSelectingBook)
        {
            _isSelectingBook = true;
            _firstBookColors = color;
            _firstIndex = SearchBookIndex(color);
            particles[_firstIndex].SetActive(true);
        }
        else
        {
            _isSelectingBook = false;
            particles[_firstIndex].SetActive(false);
            _secondBookColors = color;
            _secondIndex = SearchBookIndex(color);
            ExChangeArray(_firstIndex, _secondIndex);
        }
    }
    
    private int SearchBookIndex(BookColors bookColors)
    {
        int nowIndex = 0;
        foreach (var selectBook in _selectBooks)
        {
            if (bookColors == selectBook.BookColor) {return nowIndex;}
            nowIndex++;
        }
        return -1;  // エラーを返したい
    }

    private void ExChangeArray(int firstIndex, int secondIndex)
    {
        (books[firstIndex], books[secondIndex]) = (books[secondIndex], books[firstIndex]);
        (particles[firstIndex], particles[secondIndex])  = (particles[secondIndex], particles[firstIndex]);
        (_selectBooks[firstIndex], _selectBooks[secondIndex])  = (_selectBooks[secondIndex], _selectBooks[firstIndex]);
        (books[_firstIndex].transform.position, books[_secondIndex].transform.position)  = (books[secondIndex].transform.position, books[firstIndex].transform.position);
        Check().Forget();
    }

    private async UniTask Check()
    {
        for (int i = 0; i < _selectBooks.Length; i++)
        {
            if (_selectBooks[i].BookColor != correctColor[i]) { return; }
        }
        Correct();
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        _showTextMessage.ShowText();
    }

    private void Correct()
    {
        _isSolved = true;
        boxCoverPivot.transform.DOLocalRotate(Vector3.zero, 1.0f);
        _boxCoverCollider.enabled = false;  // 箱の当たり判定を消去
    }

    /// <summary>
    /// セーブデータロード時にこの謎を解き終わった後なら，本の並びを正解の状態にする．
    /// </summary>
    private void BookExchangeCorrect()
    {
        (books[0].transform.position, books[5].transform.position)  = (books[5].transform.position, books[0].transform.position);
        (books[1].transform.position, books[4].transform.position)  = (books[4].transform.position, books[1].transform.position);
        (books[2].transform.position, books[5].transform.position)  = (books[5].transform.position, books[2].transform.position);
        (books[3].transform.position, books[5].transform.position)  = (books[5].transform.position, books[3].transform.position);
    }
}
