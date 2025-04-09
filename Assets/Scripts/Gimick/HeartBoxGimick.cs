using UnityEngine;

public class HeartBoxGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private int _needItemId;
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _purpleHeart;
    [SerializeField] private GameObject _coin;
    private ShowTextMessage[] _showTextMessages; 
    private BoxCollider _boxCollider;
    private bool _isSolved = false;

    void Awake()
    {
        _showTextMessages = this.GetComponents<ShowTextMessage>();  // [0]に鍵ないとき用メッセ．[1]に鍵あるとき用メッセ．
        _boxCollider = this.GetComponent<BoxCollider>();
    }

    public void MoveGimick()
    {
        MoveGimickAsync();
    }
    
    private void MoveGimickAsync()
    {
        if (_isSolved) { return; }
        if (_selectingItem.SelectingItemID.Value != _needItemId){
            _showTextMessages[0].ShowText();
            return;
        }
        _isSolved = true;
        _coin.SetActive(true);
        Correct();
        _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        _showTextMessages[1].ShowText();
    }

    private void Correct()
    {
        _boxCollider.enabled = false;
        _purpleHeart.SetActive(true);
    }
}
