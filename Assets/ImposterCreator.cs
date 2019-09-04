using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImposterCreator : MonoBehaviour
{
	public GameObject ObjToImposter;
	public ImposterViewer Viewer;

	private Texture2D tex;
	private RenderTexture rt;

	private float DistanceToCamera = 10;

	public Texture2D _OutRenderTexture => tex;

	public float CutoffAngle = 10;

	// Start is called before the first frame update
	void Start()
    {
	    tex = new Texture2D(1024, 1024);
		rt = new RenderTexture(1024, 1024, 0);
	    GetComponent<Camera>().targetTexture = rt;
	    var bounds = GetHierarchyBounds(ObjToImposter);

	    transform.position = ObjToImposter.transform.position + new Vector3(0, 0, DistanceToCamera);
	    transform.LookAt(ObjToImposter.transform);
	}

    // Update is called once per frame
    void Update()
    {
		var desiredPos = ObjToImposter.transform.position + (Viewer._OutVectorToCamera.normalized * DistanceToCamera);

	    var angle = Vector3.Angle(transform.position - ObjToImposter.transform.position, desiredPos - ObjToImposter.transform.position);

		Debug.Log(angle);

	    if (angle > CutoffAngle)
	    {
		    transform.position = desiredPos;
		    transform.LookAt(ObjToImposter.transform);
		}

		RenderTexture.active = rt;
	    tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
	    tex.Apply();
	}

	private Bounds GetHierarchyBounds(GameObject ob)
	{
		Bounds b = new Bounds();
		if (ob.GetComponent<MeshRenderer>() != null)
			b = ob.GetComponent<MeshRenderer>().bounds;

		for (int i = 0; i < ob.GetComponent<Transform>().childCount; i++)
		{
			var childBounds = GetHierarchyBounds(ob.GetComponent<Transform>().GetChild(i).gameObject);
			if(!childBounds.Equals(new Bounds()))
				b.Encapsulate(childBounds);
		}

		return b;
	}
}
