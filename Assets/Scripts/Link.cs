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

	public class Link : MonoBehaviour {

		public string id;
		public Vector3 source;
		public Vector3 target;
		public string status;
		public bool loaded = false;
		public Color color;
		public float width;
		public float opacity;

		private LineRenderer lineRenderer;

		void Start () {

		}

		public void reload(){
			lineRenderer = gameObject.AddComponent<LineRenderer>();
			lineRenderer.material = new Material (Shader.Find("Self-Illumin/Diffuse"));
			lineRenderer.material.SetColor ("_Color", color);
			lineRenderer.SetWidth (width, width);
			lineRenderer.SetVertexCount(2);
			lineRenderer.SetPosition (0, source);
			lineRenderer.SetPosition (1, target);
			loaded = true;
		}
	}

}