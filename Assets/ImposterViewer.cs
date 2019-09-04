using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImposterViewer : MonoBehaviour
{
	public ImposterCreator ImposterCreator;
	public Camera UserCamera;

	public Vector3 _OutVectorToCamera;

    // Update is called once per frame
    void Update()
    {
	    _OutVectorToCamera = UserCamera.transform.position - transform.position;
		transform.LookAt(UserCamera.transform);

	    GetComponentInChildren<MeshRenderer>().material.mainTexture = ImposterCreator._OutRenderTexture;
	}
}
