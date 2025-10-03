
using UnityEngine;
namespace Player
{
    public class JumpingState : State
    {
        float jumpDelay;

        // constructor
        public JumpingState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            player.rb.linearVelocity = new Vector2(player.rb.linearVelocity.x, 8);

            player.animator.SetBool("jump_up", true);
            //player.animator.Play("arthur_jump");


            jumpDelay = 0.8f;
        }

        public override void Exit()
        {
            base.Exit();

            player.animator.SetBool("jump_up", false);
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

          //check for hitting the ground (landing)

            if( jumpDelay < 0 )
            {

                if( player.CheckForGround() == true )
                {
                    sm.ChangeState( player.idleState );
                }
            }
            else
            {
                jumpDelay -= Time.deltaTime;
            }

        }

           

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}