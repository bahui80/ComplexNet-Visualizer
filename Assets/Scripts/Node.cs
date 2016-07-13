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
		private bool isClicked = false;

		public void Start(){
			renderer = gameObject.GetComponent<Renderer> ();
			this.renderer.enabled = false;
			filter = gameObject.GetComponent<MeshFilter> ();
			setCollider ();
			setTextStyles ();
		}

		private void setTextStyles() {
			guiStyleFore = new GUIStyle();
			guiStyleFore.normal.textColor = Color.white;  
			guiStyleFore.alignment = TextAnchor.UpperCenter ;
			guiStyleFore.wordWrap = true;
			guiStyleBack = new GUIStyle();
			guiStyleBack.normal.textColor = Color.black;  
			guiStyleBack.alignment = TextAnchor.UpperCenter ;
			guiStyleBack.wordWrap = true;
		}

		private void setCollider() {
			CircleCollider2D collider = GetComponent<CircleCollider2D> ();
			collider.radius = filter.mesh.bounds.max.x;
		}


		public void zoomOut() {
			radius += 0.005f;
			renderer.transform.localScale = new Vector3 (radius, radius, radius);
		}

		public void zoomIn() {
			radius -= 0.005f;
			renderer.transform.localScale = new Vector3 (radius, radius, radius);
		}

		public void hide(){
			renderer.enabled = false;
		}

		public void show(){
			renderer.enabled = true;
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
			isClicked = true;
		}

		void OnMouseUp() {
			isClicked = false;
		}

		void OnGUI() {
			if (isClicked) {
				var x = Event.current.mousePosition.x;
				var y = Event.current.mousePosition.y;
				GUI.Label (new Rect (x-149, y+40, 300, 60), "Name: " + name, guiStyleBack);
				GUI.Label (new Rect (x-150, y+40, 300, 60), "Name: " + name, guiStyleFore);
			}
		}
	}
}