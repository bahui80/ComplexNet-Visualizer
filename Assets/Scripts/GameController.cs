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
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;

namespace Topology {

	public class GameController : MonoBehaviour {

		public Node nodePrefab;
		public Link linkPrefab;

		private Hashtable nodes;
		private Hashtable links;
		private GUIText statusText;
		private int nodeCount = 0;
		private int linkCount = 0;
		private GUIText nodeCountText;
		private GUIText linkCountText;

		private string colorrx = "([0-9]+),([0-9]+),([0-9]+)" ;

		//Method for loading the GraphML layout file
		private IEnumerator LoadLayout(){

			string sourceFile = Application.dataPath + "/Data/layout.xml";
			statusText.text = "Loading file: " + sourceFile;

			//determine which platform to load for
			string xml = null;
			if(Application.isWebPlayer){
				WWW www = new WWW (sourceFile);
				yield return www;
				xml = www.text;
			}
			else{
				StreamReader sr = new StreamReader(sourceFile);
				xml = sr.ReadToEnd();
				sr.Close();
			}

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml);

			statusText.text = "Loading Topology";


			XmlElement root = xmlDoc.FirstChild as XmlElement;
			int nodeid = 0;
			int lineid = 0;
			for(int i=0; i<root.ChildNodes.Count; i++){
				XmlElement xmlGraph = root.ChildNodes[i] as XmlElement;

				for(int j=0; j<xmlGraph.ChildNodes.Count; j++){
					XmlElement xmlNode = xmlGraph.ChildNodes[j] as XmlElement;

					if(xmlNode.Name == "circle"){
						float x1 = float.Parse(xmlNode.Attributes["cx"].Value);
						float y1 = float.Parse (xmlNode.Attributes["cy"].Value);
						float z1 = 0;
						float radius = float.Parse (xmlNode.Attributes["r"].Value);

						string color = xmlNode.Attributes["fill"].Value;
						MatchCollection matches = Regex.Matches(color, colorrx, 
						                                        RegexOptions.IgnorePatternWhitespace);
						float r = float.Parse(matches[0].Groups[1].Value)/255;
						float g = float.Parse(matches[0].Groups[2].Value)/255;
						float b = float.Parse(matches[0].Groups[3].Value)/255;
						float max = Mathf.Max(r,Mathf.Max(g,b));
						if(max > 1f){
							r /= max; g /= max; b /= max;
						}
						Color finalColor = new Color(r,g,b);
						Node nodeObject = Instantiate(nodePrefab, new Vector3(x1,y1,0), Quaternion.identity) as Node;
						nodeObject.transform.localScale = new Vector3(radius,radius,radius);
						nodeObject.position = new Vector3(x1,y1,z1);
						nodeObject.id = "Node: " + nodeid++;

						nodeObject.GetComponent<Renderer>().material.color = finalColor;
						nodes.Add(nodeObject.id, nodeObject);
						
						statusText.text = "Loading Topology: Node " + nodeObject.id;
						nodeCount++;
						nodeCountText.text = "Nodes: " + nodeCount;
					}


					//create links
					if(xmlNode.Name == "line"){
						string color = xmlNode.Attributes["stroke"].Value;
						MatchCollection matches = Regex.Matches(color, colorrx, 
						                                        RegexOptions.IgnorePatternWhitespace);
						float r = float.Parse(matches[0].Groups[1].Value)/255;
						float g = float.Parse(matches[0].Groups[2].Value)/255;
						float b = float.Parse(matches[0].Groups[3].Value)/255;
						float max = Mathf.Max(r,Mathf.Max(g,b));
						if(max > 1f){
							r /= max; g /= max; b /= max;
						}


						Color finalColor = new Color(r,g,b);

						Link linkObject = Instantiate(linkPrefab, new Vector3(0,0,0), Quaternion.identity) as Link;

						linkObject.id = "Line: " + lineid++;
						Vector3 src = new Vector3(float.Parse(xmlNode.Attributes["x1"].Value),float.Parse(xmlNode.Attributes["y1"].Value),0);
						Vector3 dest = new Vector3(float.Parse(xmlNode.Attributes["x2"].Value),float.Parse(xmlNode.Attributes["y2"].Value),0);
						linkObject.width = float.Parse(xmlNode.Attributes["stroke-width"].Value);
						linkObject.source = src;
						linkObject.target = dest;
						linkObject.color = finalColor;
						linkObject.opacity = float.Parse(xmlNode.Attributes["opacity"].Value);
						links.Add(linkObject.id, linkObject);

						statusText.text = "Loading Topology: Edge " + linkObject.id;
						linkCount++;
						linkCountText.text = "Edges: " + linkCount;
					}
				
//					//every 100 cycles return control to unity
					if(j % 100 == 0)
						yield return true;
				}
			}
			statusText.text = "";
		}


		void Start () {
			nodes = new Hashtable();
			links = new Hashtable();

			nodeCountText = GameObject.Find("NodeCount").GetComponent<GUIText>();
			nodeCountText.text = "Nodes: 0";
			linkCountText = GameObject.Find("LinkCount").GetComponent<GUIText>();
			linkCountText.text = "Edges: 0";
			statusText = GameObject.Find("StatusText").GetComponent<GUIText>();
			statusText.text = "";


			StartCoroutine( LoadLayout() );
		}

	}

}
