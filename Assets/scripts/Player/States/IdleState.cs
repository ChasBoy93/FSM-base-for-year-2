
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
namespace Player
{
    public class IdleState : State
    {
        // constructor
        public IdleState(PlayerScript player, StateMachine sm) : base(player, sm)
        {
        }

        public override void Enter()
        {
            base.Enter();

            //change sprite frame to idle

            //player.animator.Play("arthur_stand");
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void HandleInput()
        {
            base.HandleInput();
        }

        public override void LogicUpdate()
        {

            if( player.CheckForRun() == true )
            {
                sm.ChangeState(player.runningState);
            }

            if (player.CheckForJump() == true)
            {
                sm.ChangeState(player.jumpingState);
            }
            //Debug.Log("checking for jump");

            //player.CheckForAttack();
            //Debug.Log("checking for attack");

            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            player.rb.linearVelocity = new Vector2(0, player.rb.linearVelocity.y);
        }

    }
}