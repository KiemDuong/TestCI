using UnityEngine;
using System.Collections;

public class RopeLine : MonoBehaviour 
{
	// To be set in editor
	public Transform target;
	public Material material;
	public PhysicMaterial physicMaterial;
	public float ropeWidth		= 0.5f;
	public float resolution		= 0.2f;
	public float ropeDrag		= 0.1f;
	public float ropeMass		= 0.1f;
	public float radialSegments	= 6;
	public bool startRestrained	= true;
	public bool endRestrained	= false;
	public bool useMeshCollision= false;
	
	private Vector3[] segmentPos;
	private GameObject[] joints;
	private GameObject tubeRenderer;
	private TubeRenderer line;
	private int segments 	= 4;
	private bool rope 		= false;

	//Joint Settings
	Vector3 swingAxis 		= new Vector3(0,1,0);
	float lowTwistLimit 	= 0.0f;
	float highTwistLimit 	= 0.0f;
	float swing1Limit 		= 20.0f;

	void  OnDrawGizmos ()
	{
		if(target) 
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine (transform.position, target.position);
			Gizmos.DrawWireSphere ((transform.position+target.position)/2,ropeWidth);
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, ropeWidth);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (target.position, ropeWidth);
		}
		else 
		{
			Gizmos.color = Color.green;
			Gizmos.DrawWireSphere (transform.position, ropeWidth);	
		}
	}

	void  Awake ()
	{
		if(target) 
		{
			BuildRope();
		}
		else 
		{
			D.Log("No target");
		}
	}

	void  LateUpdate ()
	{
		if(target) 
		{
			// Does rope exist? If so, update its position
			if(rope) 
			{
				line.SetPoints(segmentPos, ropeWidth, Color.white);
				line.enabled = true;
				segmentPos[0] = transform.position;

				for(int s=1; s < segments; s++)
				{
					segmentPos[s] = joints[s].transform.position;
				}
			}
		}
	}

	/// <summary>
	/// Builds the rope: add game objects and joints
	/// </summary>
	void  BuildRope ()
	{
		tubeRenderer = new GameObject("TubeRenderer_" + gameObject.name);
		line = tubeRenderer.AddComponent<TubeRenderer>();
		line.useMeshCollision = useMeshCollision;

		// Find the amount of segments based on the distance and resolution
		// Example: [resolution of 1.0f = 1 joint per unit of distance]
		segments = (int)(Vector3.Distance(transform.position, target.position) * resolution);

		if(material) 
		{
			material.SetTextureScale("_MainTex",new Vector2(1,segments+2));
			//		 if(material.GetTexture("_BumpMap"))
			//			material.SetTextureScale("_BumpMap",Vector2(1,segments+2));
		}

		line.vertices = new TubeVertex[segments];
		line.crossSegments = (int)radialSegments;
		line.material = material;
		segmentPos = new Vector3[segments];
		joints = new GameObject[segments];
		segmentPos[0] = transform.position;
		segmentPos[segments-1] = target.position;

		// Find the distance between each segment
		int segs = segments - 1;
		Vector3 seperation = ((target.position - transform.position)/segs);
		for(int s = 0;s < segments;s++)
		{
			// Find the each segments position using the slope from above
			Vector3 vector = (seperation*s) + transform.position;	
			segmentPos[s] = vector;

			//Add Physics to the segments
			AddJointPhysics(s);
		}

		// Attach the joints to the target object and parent it to this object
		CharacterJoint end = target.gameObject.AddComponent<CharacterJoint>();
		end.connectedBody = joints[joints.Length-1].transform.rigidbody;
		end.swingAxis = swingAxis;
//		end.lowTwistLimit.limit = lowTwistLimit;
//		end.highTwistLimit.limit = highTwistLimit;
//		end.swing1Limit.limit	= swing1Limit;
		target.parent = transform;

		if(endRestrained)
		{
			end.rigidbody.isKinematic = true;
		}
		if(startRestrained)
		{
			transform.rigidbody.isKinematic = true;
		}
		// Rope = true, The rope now exists in the scene!
		rope = true;
	}

	/// <summary>
	/// Adds the joints to rope
	/// </summary>
	/// <param name="n">n</param>
	void  AddJointPhysics (int n)
	{
		joints[n] = new GameObject("Joint_" + n);
		joints[n].transform.parent = transform;
		Rigidbody rigid = joints[n].AddComponent<Rigidbody>();

		if(!useMeshCollision) 
		{
			SphereCollider col = joints[n].AddComponent<SphereCollider>();
			col.radius = ropeWidth;
			col.collider.material = physicMaterial;
		}

		CharacterJoint ph = joints[n].AddComponent<CharacterJoint>();
		ph.swingAxis = swingAxis;
//		ph.lowTwistLimit.limit = lowTwistLimit;
//		ph.highTwistLimit.limit = highTwistLimit;
//		ph.swing1Limit.limit	= swing1Limit;
		//ph.breakForce = ropeBreakForce;
		joints[n].transform.position = segmentPos[n];
		rigid.drag = ropeDrag;
		rigid.mass = ropeMass;

		if(n == 0)
		{			
			ph.connectedBody = transform.rigidbody;
		} 
		else
		{
			ph.connectedBody = joints[n-1].rigidbody;	
		}
	}
}