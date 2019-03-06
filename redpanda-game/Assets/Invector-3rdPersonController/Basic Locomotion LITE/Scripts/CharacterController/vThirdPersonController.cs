using UnityEngine;
using System.Collections;

namespace Invector.CharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        protected virtual void Start()
        {
#if !UNITY_EDITOR
                Cursor.visible = false;
#endif
        }

        // rpg custom
        public virtual void Dash(bool value)
        {
            isDashing = value;
        }

        public virtual void Sprint(bool value)
        {                                   
            isSprinting = value;
        }

        public virtual void Strafe()
        {
            if (locomotionType == LocomotionType.OnlyFree) return;
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {

            // conditions to do this action

            bool fromGround = (isGrounded && !isJumping);
            bool fromAir = !isGrounded;
            bool jumpConditions = (fromGround || fromAir) && jumpsLeft > 0; // rpg custom
            // return if jumpCondigions is false
            if (!jumpConditions) return;
            // rpg custom
            JumpsLeft -= 1;

            // trigger jump behaviour
            jumpCounter = jumpTimer;
            isJumping = true;

            // rpg custom

            if (fromAir)
            {
                GameObject sparks = Instantiate(doubleJumpSparks, playerFeet.transform.position, doubleJumpSparks.transform.rotation);
            }


            // trigger jump animations            
            if (_rigidbody.velocity.magnitude < 1)
                animator.CrossFadeInFixedTime("Jump", 0.1f);
            else
                animator.CrossFadeInFixedTime("JumpMove", 0.2f);
        }

        public virtual void RotateWithAnotherTransform(Transform referenceTransform)
        {
            var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), strafeRotationSpeed * Time.fixedDeltaTime);
            targetRotation = transform.rotation;
        }
    }
}