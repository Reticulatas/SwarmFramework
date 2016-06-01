using UnityEngine;
using System.Collections;
using System.Linq;

public class AI_Swarm : Entity 
{
    public float AggroDistance = 1000;
    public float RotationSpeed = 100.0f;
    public float RunStopDistance = 10.0f;
    public float AttackDistance = 10.0f;
    public float RunSpeed = 10.0f;
    public float MaxRunTimeBeforeRetarget = 10.0f;
    public float SaltFactor = 10.0f;
    public float AttackTime = 0.5f;
    public GameObject attackHitbox;

    Entity target;

    void Start()
    {
        StartCoroutine(LookForTarget());

        // salt
        MaxRunTimeBeforeRetarget += Random.value * SaltFactor;
        RotationSpeed += Random.value * SaltFactor;
        AggroDistance += Random.value * SaltFactor;
    }

    IEnumerator LookForTarget()
    {
        while (true)
        {
            Retarget();
            if (target != null)
            {
                yield return StartCoroutine(RotateToTarget());
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator RotateToTarget()
    {
        var dir = (target.transform.position - transform.position).normalized;
        dir.y = 0;
        var initialRotation = transform.rotation;
        var wantedRotation = Quaternion.LookRotation(dir, Vector3.up);

        float t = Random.value * 0.1f;
        while (t < 1.0f)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, wantedRotation, t);
            t += Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(RunToTargetPosition(target.transform.position));
    }

    IEnumerator RunToTargetPosition(Vector3 targetPos)
    {
        anim.SetBool("Walking", true);
        float t = 0;
        while (Vector3.Distance(targetPos, transform.position) > RunStopDistance)
        {
            transform.Translate(transform.forward * Time.deltaTime * RunSpeed, Space.World);
            t += Time.deltaTime;
            if (t > MaxRunTimeBeforeRetarget)
                break;
            yield return null;
        }

        anim.SetBool("Walking", false);
        if (Vector3.Distance(target.transform.position, transform.position) <= AttackDistance)
            yield return StartCoroutine(Attack());

        if (Random.value < 0.2f)
            yield return StartCoroutine(Retreat());
    }

    IEnumerator Attack()
    {
        anim.SetBool("Attacking", true);
        attackHitbox.SetActive(true);
        yield return new WaitForSeconds(AttackTime);
        attackHitbox.SetActive(false);
        anim.SetBool("Attacking", false);
        yield return new WaitForSeconds(Random.value);
    }

    IEnumerator Retreat()
    {
        yield return StartCoroutine(RunToTargetPosition(transform.position + MathExt.Vec3From(Random.insideUnitCircle * 20)));
    }

    void Retarget()
    {
        // find closest player
        float MinDist = float.MaxValue;
        Entity closestChar = null;
        foreach (var c in PlayerCharacter.characters)
        {
            var dist = Vector3.Distance(transform.position, c.transform.position);
            if (dist <= AggroDistance && dist <= MinDist)
            {
                MinDist = dist;
                closestChar = c;
            }
        }
        if (closestChar != null)
            target = closestChar;
    }
}
