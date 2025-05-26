using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaitPhysicsRigid : MonoBehaviour
{
    /// <summary>
    /// This holds all the physics so the bait can dingle at the top of the fishing rod.
    /// </summary>

    public Transform rodTip;             // Tip of the fishing rod
    public float ropeLength = 0.3f;      // Fixed length of the "line"

    private ConfigurableJoint joint;
    private Rigidbody rb;

    private bool toggleCast = false;
    private bool toggleReel = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CreateJoint();
        PlayerPrefs.SetInt("fishing", 0);
    }

    void FixedUpdate()
    {
        if (joint != null)
        {
            joint.connectedAnchor = rodTip.position; // Update as the rod moves
        }

        if (!toggleCast && PlayerPrefs.GetInt("fishing") == 1)
        {
            toggleCast = true;
            toggleReel = false;
            DisableJoint();
        }

        if (!toggleReel && PlayerPrefs.GetInt("fishing") == 0)
        {
            toggleCast = false;
            toggleReel = true;
            CreateJoint();
        }
    }

    private void CreateJoint()
    {
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

    public void DisableJoint()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }
    }
}
