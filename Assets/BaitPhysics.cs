using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaitPhysicsRigid : MonoBehaviour
{
    public Transform rodTip;             // Tip of the fishing rod
    public float ropeLength = 0.3f;      // Fixed length of the "line"

    private ConfigurableJoint joint;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        joint = gameObject.AddComponent<ConfigurableJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = rodTip.position;
        joint.connectedBody = null; // connected to world point

        // Lock all motion except swinging at fixed distance
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;

        // No spring — just a hard limit on rope length
        SoftJointLimit limit = new SoftJointLimit();
        limit.limit = ropeLength;
        joint.linearLimit = limit;

        // Remove any spring/damper
        SoftJointLimitSpring noSpring = new SoftJointLimitSpring { spring = 0f, damper = 0f };
        joint.linearLimitSpring = noSpring;

        // Allow free angular movement (swinging)
        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;

        // Stability
        joint.enablePreprocessing = false;
        joint.enableCollision = false;
    }

    void FixedUpdate()
    {
        joint.connectedAnchor = rodTip.position; // Update as the rod moves
    }
}
