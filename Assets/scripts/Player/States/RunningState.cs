using Unity.VisualScripting.FullSerializer;
using UnityEngine;
namespace Player
{
    
        public class RunningState : State
        {
            private float moveSpeed = 5f;

            public RunningState(PlayerScript player, StateMachine sm) : base(player, sm)
            {

            }

            public override void Enter()
            {
                base.Enter();
                player.animator.SetBool("run", true);
            }

            public override void Exit()
            {
                base.Exit();
                player.animator.SetBool("run", false);
            }

            public override void HandleInput()
            {
                base.HandleInput();
            }

            public override void LogicUpdate()
            {
                base.LogicUpdate();

                if (player.CheckForIdle())
                {
                    sm.ChangeState(player.idleState);
                    return;
                }

                if (player.CheckForJump())
                {
                    sm.ChangeState(player.jumpingState);
                    return;
                }

                //player.CheckForAttack();
            }

            public override void PhysicsUpdate()
            {
                base.PhysicsUpdate();

                float direction = 0f;

                if (Input.GetKey("a")) direction = -1f;
                if (Input.GetKey("d")) direction = 1f;

                player.rb.linearVelocity = new Vector2(direction * moveSpeed, player.rb.linearVelocity.y);

                if (direction != 0)
                    player.sr.flipX = direction < 0;
            }
        }
    }
