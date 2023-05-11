using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Define.eColorType _colorType = Define.eColorType.Red;
    private float _speed = 300f;
    private Vector3 _lastVelocity = Vector3.zero;
    private Vector2 _dir = Vector2.zero;

    private SpriteRenderer _spriteRdr = null;
    private CircleCollider2D _circleCollider2D = null;
    private Rigidbody2D _rigid2D = null;


    public void Init(Define.eColorType colorType)
    {
        _colorType = colorType;

        if (_spriteRdr == null) _spriteRdr = this.GetComponent<SpriteRenderer>();

        if (_circleCollider2D == null)
        {
            _circleCollider2D = this.GetComponent<CircleCollider2D>();
            _circleCollider2D.isTrigger = false;
            _circleCollider2D.usedByEffector = false;
            _circleCollider2D.offset = Vector2.zero;
            _circleCollider2D.radius = .25f;
        }

        if (_rigid2D == null)
        {
            _rigid2D = this.GetComponent<Rigidbody2D>();
            _rigid2D.simulated = true;
            _rigid2D.mass = 1f;
            _rigid2D.drag = 0f;
            _rigid2D.angularDrag = 0f;
            _rigid2D.gravityScale = 0f;
        }

        switch (_colorType)
        {
            case Define.eColorType.Red:
                _dir = this.transform.right;
                _spriteRdr.sprite = Managers.Resource.Load<Sprite>("RedBullet");
                break;

            case Define.eColorType.Blue:
                _dir = -this.transform.right;
                _spriteRdr.sprite = Managers.Resource.Load<Sprite>("BlueBullet");
                break;
        }

        this.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (Managers.Game.GameStatePlay)
        {
            switch (_colorType)
            {
                case Define.eColorType.Red:
                    _rigid2D.velocity = _dir * _speed * Time.fixedDeltaTime;
                    break;

                case Define.eColorType.Blue:
                    _rigid2D.velocity = _dir * _speed * Time.fixedDeltaTime;
                    break;
            }

            _lastVelocity = _rigid2D.velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            _dir = Vector2.Reflect(_lastVelocity.normalized, other.contacts[0].normal);
            // _rigid2D.velocity = _dir * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grid"))
        {
            bool isTrue = other.GetComponent<Grid>().CheckOccupation(_colorType);
            if (isTrue)
                this?.GetComponent<Poolable>().Destroy();
        }
    }
}
