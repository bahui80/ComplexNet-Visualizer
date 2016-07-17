using UnityEngine;

public class CameraPositionSave {
	private Vector3 position;
	private Vector3 rotation;
	private Vector3 scale;

	public CameraPositionSave(Vector3 pos, Vector3 rot, Vector3 sca) {
		this.position = pos;
		this.rotation = rot;
		this.scale = sca;
	}

	public Vector3 getPosition() {
		return position;
	}

	public Vector3 getRotation() {
		return rotation;
	}

	public Vector3 getScale() {
		return scale;
	}
		
}