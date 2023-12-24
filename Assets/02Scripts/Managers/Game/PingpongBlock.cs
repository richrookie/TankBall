using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PingpongBlock : Block
{
    public enum eMoveDirection
    {
        x,
        y,
        None
    }

    public Define.ePingpongType _pingpongType = Define.ePingpongType.None;
    public eMoveDirection _moveDirectionType = eMoveDirection.None;
    public int _value = 1;
    public bool _isMoving = false;
    private GameObject _glowObj = null;

    private bool _plusDir = true;
    private float _startPos = 0f;
    private float _endPos = 0f;
    private float _speed = 3f;
    private Rigidbody2D _rigid2D = null;


    public override void Init()
    {
        _glowObj = this.transform.GetChild(0).gameObject;
        _glowObj.SetActive(false);

        switch (_pingpongType)
        {
            case Define.ePingpongType.Plus:
                this.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("PlusBlock");
                break;

            case Define.ePingpongType.Muliply:
                this.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("MultiplyBlock");
                break;
        }

        if (_rigid2D == null)
        {
            _rigid2D = this.GetComponent<Rigidbody2D>();
            _rigid2D.simulated = true;
            _rigid2D.mass = 1f;
            _rigid2D.drag = 0f;
            _rigid2D.angularDrag = 0f;
            _rigid2D.gravityScale = 0f;
            _rigid2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (_isMoving)
        {
            switch (_moveDirectionType)
            {
                case eMoveDirection.x:
                    if (this.transform.position.x < 0)
                        _plusDir = true;
                    else
                        _plusDir = false;

                    _rigid2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                    _startPos = this.transform.position.x;
                    _endPos = -this.transform.position.x;

                    _rigid2D.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    break;

                case eMoveDirection.y:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Managers.Game.GameStatePlay)
        {
            if (_isMoving)
            {
                switch (_moveDirectionType)
                {
                    case eMoveDirection.x:
                        if (_plusDir)
                        {
                            if (this.transform.position.x < _endPos)
                                _rigid2D.velocity = Vector2.right * _speed;
                            else
                                _plusDir = false;
                        }
                        else
                        {
                            if (this.transform.position.x > _startPos)
                                _rigid2D.velocity = Vector2.left * _speed;
                            else
                                _plusDir = true;
                        }
                        break;

                    case eMoveDirection.y:
                        break;
                }
            }
        }
        else
        {
            _rigid2D.velocity = Vector2.zero;
        }
    }


    public void Reset()
    {
        switch (_moveDirectionType)
        {
            case eMoveDirection.x:
                this.transform.position = new Vector3(_startPos, this.transform.position.y, 0);
                break;

            case eMoveDirection.y:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            _glowObj.SetActive(true);
            _glowObj?.GetComponent<PingpongBlockGlow>().Timer(.1f);

            Ball ball = other.transform.GetComponent<Ball>();

            switch (_pingpongType)
            {
                case Define.ePingpongType.Plus:
                    ball.IncreaseNumber(ball.Number + _value);
                    break;

                case Define.ePingpongType.Muliply:
                    ball.IncreaseNumber(ball.Number * _value);
                    break;
            }
        }
    }
}
