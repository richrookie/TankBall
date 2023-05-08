using UnityEngine;

public class PingpongBlock : Block
{
    public enum ePingpongType
    {
        Plus,
        Muliply,
        None
    }

    public enum eMoveDirection
    {
        x,
        y,
        None
    }

    public ePingpongType _pingpongType = ePingpongType.None;
    public eMoveDirection _moveDirectionType = eMoveDirection.None;
    public int _value = 1;
    public bool _isMoving = false;
    private GameObject _glowObj = null;

    private bool _plusDir = true;
    private float _startPos = 0f;
    private float _endPos = 0f;
    private float _speed = 3f;
    private Rigidbody2D _rigid = null;


    public override void Init()
    {
        _glowObj = this.transform.GetChild(0).gameObject;
        _glowObj.SetActive(false);

        switch (_pingpongType)
        {
            case ePingpongType.Plus:
                this.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("PlusBlock");
                break;

            case ePingpongType.Muliply:
                this.GetComponent<SpriteRenderer>().sprite = Managers.Resource.Load<Sprite>("MultiplyBlock");
                break;
        }

        if (_isMoving)
        {
            _rigid = Util.GetOrAddComponent<Rigidbody2D>(this.gameObject);

            switch (_moveDirectionType)
            {
                case eMoveDirection.x:
                    if (this.transform.position.x < 0)
                        _plusDir = true;
                    else
                        _plusDir = false;

                    _rigid.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

                    _startPos = this.transform.position.x;
                    _endPos = -this.transform.position.x;
                    break;

                case eMoveDirection.y:
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            switch (_moveDirectionType)
            {
                case eMoveDirection.x:
                    if (_plusDir)
                    {
                        if (this.transform.position.x < _endPos)
                            _rigid.velocity = Vector2.right * _speed;
                        else
                            _plusDir = false;
                    }
                    else
                    {
                        if (this.transform.position.x > _startPos)
                            _rigid.velocity = Vector2.left * _speed;
                        else
                            _plusDir = true;
                    }
                    break;

                case eMoveDirection.y:
                    break;
            }
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
                case ePingpongType.Plus:
                    ball.IncreaseNumber(ball.Number + _value);
                    break;

                case ePingpongType.Muliply:
                    ball.IncreaseNumber(ball.Number * _value);
                    break;
            }
        }
    }
}
