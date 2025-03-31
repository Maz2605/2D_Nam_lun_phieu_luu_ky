using UnityEngine;


public class Movement : CoreComponent
{
    private Vector2 _workspace;

    public Vector2 CurVelocity { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    
    public int FaceDirection { get; private set; }
    public bool CanSetVelocity { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Rb = GetComponentInParent<Rigidbody2D>();
        FaceDirection = 1;
        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CurVelocity = Rb.velocity;
    }
    
    #region Set Functions

    public void SetVelocityX(float xVelocity)
    {
        _workspace.Set(xVelocity, CurVelocity.y);
        SetFinalVelocity();
    }

    public void SetVelocityY(float yVelocity)
    {
        _workspace.Set(CurVelocity.x, yVelocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetFinalVelocity();
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        _workspace = direction * velocity;
        SetFinalVelocity();
    }

    public void SetVelocityZero()
    {
        _workspace = Vector2.zero;
        SetFinalVelocity();
    }

    public void SetFinalVelocity()
    {
        if (CanSetVelocity)
        {
            Rb.velocity = _workspace;
            CurVelocity = _workspace;
        }
    }

    public void Flip()
    {
        FaceDirection *= -1;
        Rb.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FaceDirection)
        {
            Flip();
        }
    }

    #endregion
}