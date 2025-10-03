using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.VFX;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public SpriteRenderer sr;
        public Animator animator;
        public LayerMask groundLayerMask;


        // variables holding the different player states
        public IdleState idleState;
        public RunningState runningState;
        public JumpingState jumpingState;
        public AttackState attackState;

        public StateMachine sm;

        public PlayerData playerData;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            sr = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            // add new states here
            idleState = new IdleState(this, sm);
            runningState = new RunningState(this, sm);
            jumpingState = new JumpingState(this, sm);
            attackState = new AttackState(this, sm);


            // initialise the statemachine with the default state
            sm.Init(idleState);

            print(playerData.lives);
        }

        // Update is called once per frame
        public void Update()
        {
            sm.CurrentState.LogicUpdate();

            //output debug info to the canvas
            string s;
            s = string.Format("last state={0}\ncurrent state={1}", sm.LastState, sm.CurrentState);

            UIscript.ui.DrawText(s);

            UIscript.ui.DrawText("Press A move left / D move right / SPACE Jump");

        }



        void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }



        public bool CheckForRun()
        {
            if (Input.GetKey("a") || Input.GetKey("d")) // key held down
            {
                return true;
            }
            return false;
        }


        public bool CheckForIdle()
        {
            if (Input.GetKey("a") != true && Input.GetKey("d") != true)
            {
                return true;
            }
            return false;
        }


        public bool CheckForJump()
        {
            if (Input.GetKeyDown("space")) // key held down
            {
                return true;
            }
            return false;

        }

        public void CheckForAttack()
        {
            if (Input.GetKey("e")) // key held down
            {
                sm.ChangeState(attackState);
                return;
            }

        }

        public bool CheckForGround()
        {
            return RayCollisionCheck(0, 0);
        }


        public bool RayCollisionCheck(float xoffs, float yoffs)
        {
            float rayLength = 0.5f; // length of raycast
            bool hitSomething = false;

            // convert x and y offset into a Vector3 
            Vector3 offset = new Vector3(xoffs, yoffs, 0);

            //cast a ray downward starting at the sprite's position
            RaycastHit2D hit;

            hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

            Color hitColor = Color.red;


            if (hit.collider != null)
            {
               // print("Player has collided with Ground layer");
                hitColor = Color.green;
                hitSomething = true;
            }
            // draw a debug ray to show ray's position
            // You need to enable gizmos in th e editor to see these
            Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
            return hitSomething;
        }



    }

}