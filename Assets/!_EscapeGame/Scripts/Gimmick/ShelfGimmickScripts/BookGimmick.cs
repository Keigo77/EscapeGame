using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


public class BookGimmick : MonoBehaviour
{
    [SerializeField] private GameObject[] _particles = new GameObject[6];   // どの本を選択したかわかるように，選択中の本にだけパーティクルを表示
    [SerializeField] private GameObject[] _books = new GameObject[6];
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private BoxCollider _boxCoverCollider;
    private readonly SelectBook[] _selectBooks = new SelectBook[6];
    [Header("正しい色の並び順を入力")] 
    [SerializeField] private BookColors[] _correctColor = new BookColors[6];
    private ShowTextMessage _showTextMessage; 
    private bool _isSelectingBook = false;
    private int _firstIndex, _secondIndex;
    private bool _isSolved = false; // 謎を解いたか
    [SerializeField] private AudioClip _putBookSe;
    [SerializeField] private AudioClip _openBoxSe;
    [SerializeField] private AudioClip _solveSe;
    
    void Awake()
    {
        _showTextMessage = GetComponent<ShowTextMessage>();
        for (int i = 0; i < _books.Length; i++)
        {
            _selectBooks[i] = _books[i].GetComponent<SelectBook>();
        }
    }
    
    public void MoveBook(BookColors color)
    {
        if (_isSolved) { return; }
        if(!_isSelectingBook)
        {
            _isSelectingBook = true;
            _firstIndex = SearchBookIndex(color);
            _particles[_firstIndex].SetActive(true);
        }
        else
        {
            _isSelectingBook = false;
            _particles[_firstIndex].SetActive(false);
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
        (_books[firstIndex], _books[secondIndex]) = (_books[secondIndex], _books[firstIndex]);
        (_particles[firstIndex], _particles[secondIndex])  = (_particles[secondIndex], _particles[firstIndex]);
        (_selectBooks[firstIndex], _selectBooks[secondIndex])  = (_selectBooks[secondIndex], _selectBooks[firstIndex]);
        (_books[_firstIndex].transform.position, _books[_secondIndex].transform.position)  = (_books[secondIndex].transform.position, _books[firstIndex].transform.position);
        SEManager.PlaySe(_putBookSe);
        Check().Forget();
    }

    private async UniTask Check()
    {
        for (int i = 0; i < _selectBooks.Length; i++)
        {
            if (_selectBooks[i].BookColor != _correctColor[i]) { return; }
        }
        SEManager.PlaySe(_openBoxSe);
        await Correct();
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        _showTextMessage.ShowText().Forget();
        SEManager.PlaySe(_solveSe);
    }

    private async UniTask Correct()
    {
        _isSolved = true;
        await _boxCoverPivot.transform.DOLocalRotate(Vector3.zero, 0.5f).AsyncWaitForCompletion();
        _boxCoverCollider.enabled = false;  // 箱の当たり判定を消去
    }

    /// <summary>
    /// セーブデータロード時にこの謎を解き終わった後なら，本の並びを正解の状態にする．
    /// </summary>
    private void BookExchangeCorrect()
    {
        (_books[0].transform.position, _books[5].transform.position)  = (_books[5].transform.position, _books[0].transform.position);
        (_books[1].transform.position, _books[4].transform.position)  = (_books[4].transform.position, _books[1].transform.position);
        (_books[2].transform.position, _books[5].transform.position)  = (_books[5].transform.position, _books[2].transform.position);
        (_books[3].transform.position, _books[5].transform.position)  = (_books[5].transform.position, _books[3].transform.position);
    }
}
