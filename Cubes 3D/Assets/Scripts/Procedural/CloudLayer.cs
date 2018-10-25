using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour {

	public int gridExtents;

	public int gridSize { get {return gridExtents * 2 + 1;} }
	
	public int gridCount { get {return gridSize * gridSize;} }

	public float spacing;

	[NonNegative]
	public float minDistancefromCentre;

	public float maxSize;

	public float perlinScale;

	public Vector2 wind;

	Vector2 windOffset;

	[Space]
	public Mesh cloudMesh;
	public Material cloudMaterial;

	//[y,x]
	GameObject[,] clouds;

	const int maxInstancesPerCall = 1023;
	Matrix4x4[][] cloudMatrices;

	Vector3 lastPosition;
	bool firstUpdate;

	// else circle
	readonly bool square = true;

	public void Start ()
	{
		int drawBatches = (gridCount + maxInstancesPerCall - 1) / maxInstancesPerCall;
		
		// init matrices with values that wont change
		cloudMatrices = new Matrix4x4[drawBatches][];
		for (int batch = 0; batch < drawBatches; batch++)
		{
			cloudMatrices[batch] = new Matrix4x4[maxInstancesPerCall];
			for (int instance = 0; instance < maxInstancesPerCall; instance++)
			{
				cloudMatrices[batch][instance] = new Matrix4x4();
				cloudMatrices[batch][instance].m33 = 1; // affine extension
				cloudMatrices[batch][instance].m13 = transform.position.y; // height
			}
		}

		windOffset = new Vector2(1234, 4321);
		lastPosition = transform.position;
		firstUpdate = true;

		// set initial positions
		Vector3 centre = transform.position;
		for (int batch = 0; batch < cloudMatrices.Length; batch++)
		{
			for (int instance = 0; instance < maxInstancesPerCall; instance++)
			{
				int flatIndex = batch * maxInstancesPerCall + instance;
				float posX, posZ;

				// square grid
				if(square)
				{
					int x = flatIndex % gridSize - gridExtents;
					int y = flatIndex / gridSize - gridExtents;
					posX = centre.x + x * spacing;
					posZ = centre.z + y * spacing;
				}
				else // circle
				{
					int x = flatIndex % gridSize;
					int y = flatIndex / gridSize;
					float radius = x * spacing + minDistancefromCentre;
					float angle = y * 2 * Mathf.PI / gridSize;
					angle += flatIndex; // break alignment

					posX = centre.x + radius * Mathf.Cos(angle);
					posZ = centre.z + radius * Mathf.Sin(angle);
				}

				cloudMatrices[batch][instance].m03 = posX;
				cloudMatrices[batch][instance].m23 = posZ;
			}
		}

	}
	
	public void Update () {
		windOffset += wind * Time.deltaTime;
		
		Vector3 deltaPosition = transform.position - lastPosition;
		float dX, dZ;
		if(square)
		{
			// move only in steps to reduce jitter
			dX = Mathf.Floor(deltaPosition.x / spacing) * spacing;
			dZ = Mathf.Floor(deltaPosition.z / spacing) * spacing;
		}
		else
		{
			dX = deltaPosition.x;
			dZ = deltaPosition.z;
		}
		lastPosition.x += dX;
		lastPosition.z += dZ;

		for (int batch = 0; batch < cloudMatrices.Length; batch++)
		{
			for (int instance = 0; instance < maxInstancesPerCall; instance++)
			{
				cloudMatrices[batch][instance].m03 += dX;
				cloudMatrices[batch][instance].m23 += dZ;

				var perl = Mathf.PerlinNoise(
					windOffset.x + cloudMatrices[batch][instance].m03 * perlinScale,
					windOffset.y + cloudMatrices[batch][instance].m23 * perlinScale);
				perl = (perl - 0.5f) * 2; // [0,1] -> [-1,1]
				float scale = Mathf.Max(0, maxSize * perl);
				if(!firstUpdate && !square)
					scale = Mathf.MoveTowards(cloudMatrices[batch][instance].m00, scale, 0.01f);
					
				cloudMatrices[batch][instance].m00 = scale;
				cloudMatrices[batch][instance].m11 = scale * 0.3f;
				cloudMatrices[batch][instance].m22 = scale;
			}
			
			Graphics.DrawMeshInstanced(cloudMesh, 0, cloudMaterial, cloudMatrices[batch]);
		}

		firstUpdate = false;
	}
}
