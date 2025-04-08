using DG.Tweening;
using UnityEngine;
// ToDo:本の位置を保存．謎を解いたかbool値で保存

public class BookGimick : MonoBehaviour
{
    [SerializeField] private GameObject[] _particles = new GameObject[6];   // どの本を選択したかわかるように，選択中の本にだけパーティクルを表示
    [SerializeField] private GameObject[] _books = new GameObject[6];
    [SerializeField] private GameObject _boxCoverPivot;
    private SelectBook[] _selectBooks = new SelectBook[6];
    private Transform[] _transforms = new Transform[6];
    public enum BookColors
    {
        Pink = 0,
        Purple = 1,
        Green = 2,
        Yellow = 3,
        Red = 4,
        Blue = 5
    }
    [Header("正しい色の並び順を入力")] 
    [SerializeField] private BookColors[] _correctColor = new BookColors[6];
    private ShowTextMessage _showTextMessage;
    private bool _isSelectingBook = false;
    private BookColors _firstBookColors;
    private BookColors _secondBookColors;
    private int _firstIndex, _secondIndex;
    private bool _isSolved = false; // 謎を解いたか
    
    void Awake()
    {
        _showTextMessage = GetComponent<ShowTextMessage>();
        for (int i = 0; i < _books.Length; i++)
        {
            _selectBooks[i] = _books[i].GetComponent<SelectBook>();
            _transforms[i] = _books[i].GetComponent<Transform>();
        }
    }
    
    public void MoveBook(int color, Vector3 position)
    {
        if (_isSolved) {return;}
        if(!_isSelectingBook)
        {
            _isSelectingBook = true;
            _firstBookColors = (BookColors)color;
            _firstIndex = SearchBookIndex((BookColors)color);
            _particles[_firstIndex].SetActive(true);
        }
        else
        {
            _isSelectingBook = false;
            _particles[_firstIndex].SetActive(false);
            _secondBookColors = (BookColors)color;
            _secondIndex = SearchBookIndex((BookColors)color);
            ExChangeArray(_firstIndex, _secondIndex);
        }
    }
    
    private int SearchBookIndex(BookColors bookColors)
    {
        int nowIndex = 0;
        foreach (var selectBook in _selectBooks)
        {
            if (bookColors == (BookColors)selectBook.BookColor) {return nowIndex;}
            nowIndex++;
        }
        return -1;  // エラーを返したい
    }

    private void ExChangeArray(int firstIndex, int secondIndex)
    {
        (_books[firstIndex], _books[secondIndex]) = (_books[secondIndex], _books[firstIndex]);
        (_particles[firstIndex], _particles[secondIndex])  = (_particles[secondIndex], _particles[firstIndex]);
        (_selectBooks[firstIndex], _selectBooks[secondIndex])  = (_selectBooks[secondIndex], _selectBooks[firstIndex]);
        (_transforms[_firstIndex].position, _transforms[_secondIndex].position)  = (_transforms[secondIndex].position, _transforms[firstIndex].position);
        (_transforms[_firstIndex], _transforms[_secondIndex])  = (_transforms[secondIndex], _transforms[firstIndex]);
        Check();
    }

    private void Check()
    {
        bool correct = true;
        int nowIndex = 0;
        foreach (var selectBook in _selectBooks)
        {
            if((BookColors)selectBook.BookColor != _correctColor[nowIndex]) {correct = false;}
            nowIndex++;
        }

        if (correct)
        {
            _showTextMessage.ShowText();
            Correct();
        }
    }

    public void Correct()
    {
        _isSolved = true;
        _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.0f);
    }

    /// <summary>
    /// セーブデータロード時にこの謎を解き終わった後なら，本の並びを正解の状態にする．
    /// </summary>
    private void BookExchangeCorrect()
    {
        (_transforms[0].position, _transforms[5].position)  = (_transforms[5].position, _transforms[0].position);
        (_transforms[1].position, _transforms[4].position)  = (_transforms[4].position, _transforms[1].position);
        (_transforms[2].position, _transforms[5].position)  = (_transforms[5].position, _transforms[2].position);
        (_transforms[3].position, _transforms[5].position)  = (_transforms[5].position, _transforms[3].position);
    }
}
