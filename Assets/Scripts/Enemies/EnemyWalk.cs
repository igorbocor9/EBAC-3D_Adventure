using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        public GameObject[] waypoints;
        public float speed = 1f;
        public float minDistance = 1f;

        private int _index = 0;

        private void Update()
        {
            if (Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistance)
            {
                _index++;
                if (_index >= waypoints.Length)
                {
                    _index = 0;
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, speed * Time.deltaTime);
            transform.LookAt(waypoints[_index].transform.position);
        }
    }
}