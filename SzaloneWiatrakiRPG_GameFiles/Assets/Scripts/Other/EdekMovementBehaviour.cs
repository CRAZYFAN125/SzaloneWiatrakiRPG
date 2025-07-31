using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.OtherSystems
{
    public class EdekMovementBehaviour : MonoBehaviour
    {
        [SerializeField]
        Animator animator;
        GameObject MousePointer;
        NavMeshAgent nav;
        [Tooltip("X valuse is base speed, Y is spring mode")]
        [SerializeField]
        Vector2 speeds;
        // Start is called before the first frame update
        void Start()
        {
            MousePointer = GameObject.Find("MousePointer");
            nav = GetComponent<NavMeshAgent>();

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            print(nav.pathStatus);
            if(nav.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                animator.SetBool("Moving", false);
                nav.isStopped = true;
                return;
            }
            if (Vector3.Distance(transform.position, MousePointer.transform.position)>nav.stoppingDistance)
            {
                nav.isStopped = false;
                nav.destination = MousePointer.transform.position;
                animator.SetBool("Moving", true);
                if (Vector3.Distance(transform.position, MousePointer.transform.position) > 8)
                    nav.speed = speeds.y;
                else
                    nav.speed = speeds.x;
            }
            else
            {
                animator.SetBool("Moving", false);
            }

            
        }
    }
}
