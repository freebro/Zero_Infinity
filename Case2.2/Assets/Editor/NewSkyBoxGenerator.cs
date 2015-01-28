	using UnityEngine;
	using UnityEditor;
	using System;
	using System.IO;
	
	class NewSkyBoxGenerator
	{
		static int faceSize = 512;
		static string directory = "Assets/Skyboxes";
		static string skyboxShader = "RenderFX/Skybox";
		
		static string[] skyBoxImage = new string[]{"front", "right", "back", "left", "up", "down"};
		static string[] skyBoxProps = new string[]{"_FrontTex", "_RightTex", "_BackTex", "_LeftTex", "_UpTex", "_DownTex"};
		
		static Vector3[] skyDirection = new Vector3[]{new Vector3(0,0,0), new Vector3(0,-90,0), new Vector3(0,180,0), new Vector3(0,90,0), new Vector3(-90,0,0), new Vector3(90,0,0)};
		
		[MenuItem("GameObject/Custom/Bake Skybox", false, 4)]
		static void Bake()
		{
			if (Selection.transforms.Length == 0)
			{
				Debug.LogWarning ("Select at least one scene object as a skybox center!");
				return;
			}
			
			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);
			
			foreach(Transform t in Selection.transforms)
				RenderSkyboxTo6PNG(t);
		}
		
		static void RenderSkyboxTo6PNG(Transform t)
		{
			GameObject go = new GameObject("SkyboxCamera", typeof(Camera));
			
			go.camera.backgroundColor = Color.black;
			go.camera.clearFlags = CameraClearFlags.Skybox;
			go.camera.fieldOfView = 90;    
			go.camera.aspect = 1.0f;
			
			go.transform.position = t.position;
			go.transform.rotation = Quaternion.identity;
			
			//Render skybox        
			for (int orientation = 0; orientation < skyDirection.Length ; orientation++)
			{
				string assetPath = Path.Combine(directory, t.name + "_" + skyBoxImage[orientation] + ".png");
				RenderSkyBoxFaceToPNG(orientation, go.camera, assetPath);
			}
			GameObject.DestroyImmediate(go);
			
			//Wire skybox material
			AssetDatabase.Refresh();
			
			Material skyboxMaterial = new Material(Shader.Find(skyboxShader));        
			for (int orientation = 0; orientation < skyDirection.Length ; orientation++)
			{
				string texPath = Path.Combine(directory, t.name + "_" + skyBoxImage[orientation] + ".png");
				Texture2D tex = (Texture2D)AssetDatabase.LoadAssetAtPath(texPath, typeof(Texture2D));
				tex.wrapMode = TextureWrapMode.Clamp;
				skyboxMaterial.SetTexture(skyBoxProps[orientation], tex);
			}
			
			//Save material
			string matPath = Path.Combine(directory, t.name + "_skybox" + ".mat");
			AssetDatabase.CreateAsset(skyboxMaterial, matPath);
		}
		
		static void RenderSkyBoxFaceToPNG(int orientation, Camera cam, string assetPath)
		{
			cam.transform.eulerAngles = skyDirection[orientation];
			RenderTexture rt = new RenderTexture(faceSize, faceSize, 24);
			cam.camera.targetTexture = rt;
			cam.camera.Render();
			RenderTexture.active = rt;
			
			Texture2D screenShot = new Texture2D(faceSize, faceSize, TextureFormat.RGB24, false);
			screenShot.ReadPixels(new Rect(0, 0, faceSize, faceSize), 0, 0); 
			
			RenderTexture.active = null;
			GameObject.DestroyImmediate(rt);
			
			byte[] bytes = screenShot.EncodeToPNG(); 
			File.WriteAllBytes(assetPath, bytes);
			
			AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
		}
	}

