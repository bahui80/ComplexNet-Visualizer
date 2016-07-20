/*
 * Copyright 2014 Jason Graves (GodLikeMouse/Collaboradev)
 * http://www.collaboradev.com
 *
 * This file is part of Unity - Topology.
 *
 * Unity - Topology is free software: you can redistribute it 
 * and/or modify it under the terms of the GNU General Public 
 * License as published by the Free Software Foundation, either 
 * version 3 of the License, or (at your option) any later version.
 *
 * Unity - Topology is distributed in the hope that it will be useful, 
 * but WITHOUT ANY WARRANTY; without even the implied warranty of 
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License 
 * along with Unity - Topology. If not, see http://www.gnu.org/licenses/.
 */

using UnityEngine;
using System.Collections;

namespace Topology {

	public class Node : MonoBehaviour {

		public string id;
		public float radius;
		public string name;
		public Vector3 position;
		private Renderer renderer;
		private MeshFilter filter;
		private GUIStyle guiStyleFore;
		private GUIStyle guiStyleBack;
		private GameObject text;
		public float ratio = 10;

		public void Start() {
			renderer = gameObject.GetComponent<Renderer> ();
			this.renderer.enabled = false;
			filter = gameObject.GetComponent<MeshFilter> ();
			setCollider ();
		}


		private void setCollider() {
			CircleCollider2D collider = GetComponent<CircleCollider2D> ();
			collider.radius = filter.mesh.bounds.max.x;
		}


		public void zoomOut() {
			if (renderer != null) {
				radius = radius * 1.05f;
				renderer.transform.localScale = new Vector3 (radius, radius, radius);
			}
		}

		public void zoomIn() {
			if (renderer != null) {
				radius = radius * 0.95f;
				renderer.transform.localScale = new Vector3 (radius, radius, radius);
			}
		}

		public void hide() {
			if (renderer != null) {
				renderer.enabled = false;
			}
		}

		public void show() {
			if (renderer != null) {
				renderer.enabled = true;
			}
		}

		public void OnTriggerEnter2D(Collider2D other) {
			if(FullyContains(other)) {
				Destroy (other.gameObject);
			}
		} 

		private bool FullyContains(Collider2D resident){
			CircleCollider2D zone = GetComponent<CircleCollider2D>();
			if(zone == null) {
				return false;
			}
			return zone.bounds.Contains(resident.bounds.max) && zone.bounds.Contains(resident.bounds.min);
		}

		void OnMouseDown() {
			if (text == null) {
				text = (GameObject) Instantiate (Resources.Load("Text"), new Vector3(transform.position.x, transform.position.y - 0.09f, 0), Quaternion.identity);
				text.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
				text.GetComponent<TextMesh>().text = "Name: " + name;
				float finalSize = (float) Screen.width/ratio;
				text.GetComponent<TextMesh>().fontSize = (int) finalSize;
			} else {
				Destroy (text);
				text = null;
			}
		}
	}
}