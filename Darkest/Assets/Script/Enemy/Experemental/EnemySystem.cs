using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem
{
    [System.Serializable]
    public class Vision
    {

        public Collider[] visibleThigs;

        public LayerMask visible;

        public void Sight(Vector3 transf_pos)
        {
            visibleThigs = Physics.OverlapSphere(transf_pos, 5, visible);
        }

       public Transform findObject(string name)
        {
            Transform desired_obj = null;

            foreach(Collider obj in visibleThigs)
            {
                if(obj.name == name)
                {
                    desired_obj = obj.transform;

                    return desired_obj;
                }
            }
            return null;
        }

        public bool see(Transform desired,Transform transform, bool const_distance, float vision_distance)
        {
            if (desired == null)
            {
                Debug.Log("Nothing Founded");
                return false;
            }
            Ray vis_ray;
            RaycastHit hit;
            float see_range;
            if (const_distance)
                see_range = vision_distance;

            vis_ray = new Ray(transform.position, (desired.position - transform.position).normalized);

            if (Physics.Raycast(vis_ray.origin, vis_ray.direction * vision_distance, out hit, vision_distance, visible))
                return true;
            else return false;
        }

    }


    [System.Serializable]
    public class ActNfeel
    {
        public Patrolling patrol;

        public ExtinctTorch extinctTorch;

        public Chaese chaese;

        public GoToBox goToBox;
    }
}
