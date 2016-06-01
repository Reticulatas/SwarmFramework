using System;
using System.Collections.Generic;
using UnityEngine;

public class RaycastQueue : Assets.BehaviourSingleton<RaycastQueue>
{
    struct Pack
    {
        public Ray ray;
        public RaycastHit hit;
        public float maxDist;
        public LayerMask mask;
        public Action<RaycastHit, bool> callback;
    }

    Queue<Pack> queue = new Queue<Pack>();
    public int NumberPerFrame = 50;

    public void Queue(Ray ray, float maxDist, Action<RaycastHit, bool> callback, LayerMask mask)
    {
        var pack = new Pack();
        pack.ray = ray;
        pack.maxDist = maxDist;
        pack.mask = mask;
        pack.callback = callback;
        queue.Enqueue(pack);
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < NumberPerFrame; ++i)
        {
            if (queue.Count <= 0)
                break;
            var p = queue.Dequeue();
            Do(p);
        }
    }

    void Do(Pack pack)
    {
        var b = Physics.Raycast(pack.ray, out pack.hit, pack.maxDist, pack.mask);
        pack.callback(pack.hit, b);
    }
}