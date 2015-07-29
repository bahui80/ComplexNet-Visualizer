﻿/*
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
		public Vector3 position;


		void Update () {
			bool cond = false;
			if (Input.GetKey ("i")) {
				radius += 0.01f;
				cond = true;
			} else if (Input.GetKey ("o")) {
				radius -= 0.01f;
				cond = true;

			}
			if (cond) {
				this.gameObject.GetComponent<Renderer> ().transform.localScale = new Vector3 (radius, radius, radius);
			}
		}
	}

}