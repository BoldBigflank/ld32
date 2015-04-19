using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MaterialManager : MonoBehaviour {
	public Material[] materials;
	Dictionary<string, Material> materialDict = new Dictionary<string, Material>();

	public static MaterialManager Instance;

	void Awake() {
		if(MaterialManager.Instance != null && MaterialManager.Instance != this) 
			Destroy (gameObject);

		Instance = this;
		DontDestroyOnLoad(gameObject);



		for(int i = 0; i<materials.Length; i++){
			materialDict.Add(materials[i].name, materials[i]);
		}
	}

	public Material GetCellMaterial(string name, string state){
		Material mat = null;
		materialDict.TryGetValue(name + state + "Mat", out mat);
//		Debug.Log (mat);
		return mat;
	}
}
