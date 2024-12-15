using UnityEditor;
using UnityEngine;
using UnityGLTF;

class Convert_Hidden_InternalErrorShader_to_GLTF
{
	const string shaderName = "Hidden/InternalErrorShader";

	[InitializeOnLoadMethod]
	private static void Register()
	{
		GLTFMaterialHelper.RegisterMaterialConversionToGLTF(ConvertMaterialProperties);
	}

	private static bool ConvertMaterialProperties(Material material, Shader oldShader, Shader newShader)
	{
		if (oldShader.name != shaderName) return false;

		// Reading old shader properties.


		material.shader = newShader;

		// Assigning new shader properties.
		// Uncomment lines you need, and set properties from values from the section above.

		// material.SetFloat("thicknessFactor", insert_value_here); // Thickness
		// material.SetFloat("alphaCutoff", insert_value_here); // Alpha Cutoff
		// material.SetFloat("ior", insert_value_here); // Index of Refraction
		// material.SetFloat("roughnessFactor", insert_value_here); // Roughness
		// material.SetColor("baseColorFactor", insert_value_here); // Base Color
		// material.SetFloat("metallicFactor", insert_value_here); // Metallic
		// material.SetFloat("transmissionFactor", insert_value_here); // Transmission
		// material.SetFloat("attenuationDistance", insert_value_here); // Attenuation Distance
		// material.SetColor("attenuationColor", insert_value_here); // Attenuation Color
		// material.SetTexture("baseColorTexture", insert_value_here); // Base Color Map
		// material.SetVector("baseColorTexture_ST", insert_value_here); // Base Color Map Tiling/Offset
		// material.SetFloat("baseColorTextureRotation", insert_value_here); // Base Color Map Rotation
		// material.SetFloat("baseColorTextureTexCoord", insert_value_here); // Base Color Map UV
		// material.SetTexture("metallicRoughnessTexture", insert_value_here); // Roughness-Metallic Map
		// material.SetVector("metallicRoughnessTexture_ST", insert_value_here); // Roughness-Metallic Map Tilling/Offset
		// material.SetFloat("metallicRoughnessTextureRotation", insert_value_here); // Roughness-Metallic Map Rotation
		// material.SetFloat("metallicRoughnessTextureTexCoord", insert_value_here); // Roughness-Metallic Map UV
		// material.SetTexture("transmissionTexture", insert_value_here); // Transmission Map
		// material.SetVector("transmissionTexture_ST", insert_value_here); // Transmission Map Tiling/Offset
		// material.SetFloat("transmissionTextureRotation", insert_value_here); // Transmission Map Rotation
		// material.SetFloat("transmissionTextureTexCoord", insert_value_here); // Transmission Map UV
		// material.SetTexture("thicknessTexture", insert_value_here); // Thickness Map
		// material.SetVector("thicknessTexture_ST", insert_value_here); // Thickness Map Tiling/Offset
		// material.SetFloat("thicknessTextureRotation", insert_value_here); // Thickness Map Rotation
		// material.SetFloat("thicknessTextureTexCoord", insert_value_here); // Thickness Map UV
		// material.SetColor("emissiveFactor", insert_value_here); // Emissive
		// material.SetTexture("emissiveTexture", insert_value_here); // Emissive Map
		// material.SetVector("emissiveTexture_ST", insert_value_here); // Emissive Map Tiling/Rotation
		// material.SetFloat("emissiveTextureRotation", insert_value_here); // Emissive Map Rotation
		// material.SetFloat("emissiveTextureTexCoord", insert_value_here); // Emissive Map UV
		// material.SetTexture("normalTexture", insert_value_here); // Normal Map
		// material.SetVector("normalTexture_ST", insert_value_here); // Normal Map Tiling/Offset
		// material.SetFloat("normalTextureRotation", insert_value_here); // Normal Map Rotation
		// material.SetFloat("normalTextureTexCoord", insert_value_here); // Normal Map UV
		// material.SetFloat("normalScale", insert_value_here); // Normal Scale
		// material.SetTexture("occlusionTexture", insert_value_here); // Occlusion Map
		// material.SetFloat("occlusionStrength", insert_value_here); // Occlusion Strength
		// material.SetVector("occlusionTexture_ST", insert_value_here); // Occlusion Map Tiling/Offset
		// material.SetFloat("occlusionTextureRotation", insert_value_here); // Occlusion Map Rotation
		// material.SetFloat("occlusionTextureTexCoord", insert_value_here); // Occlusion Map UV
		// material.SetFloat("iridescenceFactor", insert_value_here); // Iridescence
		// material.SetTexture("iridescenceTexture", insert_value_here); // Iridescence Map
		// material.SetFloat("iridescenceIor", insert_value_here); // Iridescence IOR
		// material.SetVector("iridescenceTexture_ST", insert_value_here); // Iridescence Map Tiling/Offset
		// material.SetFloat("iridescenceTextureRotation", insert_value_here); // Iridescence Map Rotation
		// material.SetFloat("iridescenceTextureTexCoord", insert_value_here); // Iridescence Map UV
		// material.SetTexture("iridescenceThicknessTexture", insert_value_here); // Iridescence Thickness Map
		// material.SetFloat("iridescenceThicknessMinimum", insert_value_here); // Iridescence Thickness Min
		// material.SetFloat("iridescenceThicknessMaximum", insert_value_here); // Iridescence Thickness Max
		// material.SetVector("iridescenceThicknessTexture_ST", insert_value_here); // Iridescence Thickness Map Tiling/Offset
		// material.SetFloat("iridescenceThicknessTextureRotation", insert_value_here); // Iridescence Thickness Map Rotation
		// material.SetFloat("iridescenceThicknessTextureTexCoord", insert_value_here); // Iridescence Thickness Map UV
		// material.SetFloat("specularFactor", insert_value_here); // Specular
		// material.SetColor("specularColorFactor", insert_value_here); // Specular Color
		// material.SetTexture("specularTexture", insert_value_here); // Specular Map
		// material.SetVector("specularTexture_ST", insert_value_here); // Specular Map Tiling/Offset
		// material.SetFloat("specularTextureRotation", insert_value_here); // Specular Map Rotation
		// material.SetFloat("specularTextureTexCoord", insert_value_here); // Specular Map UV
		// material.SetTexture("specularColorTexture", insert_value_here); // Specular Color Map
		// material.SetVector("specularColorTexture_ST", insert_value_here); // Specular Color Map TIling/Scale
		// material.SetFloat("specularColorTextureRotation", insert_value_here); // Specular Color Map Rotation
		// material.SetFloat("specularColorTextureTexCoord", insert_value_here); // Specular Color Map UV
		// material.SetFloat("clearcoatFactor", insert_value_here); // Clearcoat
		// material.SetFloat("clearcoatRoughnessFactor", insert_value_here); // Clearcoat Roughness
		// material.SetTexture("clearcoatTexture", insert_value_here); // Clearcoat Mask Map
		// material.SetVector("clearcoatTexture_ST", insert_value_here); // Clearcoat Mask Map Tiling/Offset
		// material.SetFloat("clearcoatTextureRotation", insert_value_here); // Clearcoat Mask Map Rotation
		// material.SetFloat("clearcoatTextureTexCoord", insert_value_here); // Clearcoat Mask Map UV
		// material.SetTexture("clearcoatRoughnessTexture", insert_value_here); // Clearcoat Roughness Map
		// material.SetVector("clearcoatRoughnessTexture_ST", insert_value_here); // Clearcoat Roughness Map Tiling/Offset
		// material.SetFloat("clearcoatRoughnessTextureRotation", insert_value_here); // Clearcoat Roughness Map Rotation
		// material.SetFloat("clearcoatRoughnessTextureTexCoord", insert_value_here); // Clearcoat Roughness Map UV
		// material.SetFloat("dispersion", insert_value_here); // Dispersion
		// material.SetColor("sheenColorFactor", insert_value_here); // Sheen Color
		// material.SetFloat("sheenRoughnessFactor", insert_value_here); // Sheen Roughness
		// material.SetTexture("sheenColorTexture", insert_value_here); // Sheen Color Map
		// material.SetFloat("sheenColorTextureRotation", insert_value_here); // Sheen Color Map Rotation
		// material.SetVector("sheenColorTexture_ST", insert_value_here); // Sheen Color Map Tiling/Scale
		// material.SetFloat("sheenColorTextureTexCoord", insert_value_here); // Sheen Color Map UV
		// material.SetTexture("sheenRoughnessTexture", insert_value_here); // Sheen Roughness Map
		// material.SetVector("sheenRoughnessTexture_ST", insert_value_here); // Sheen Roughness Map TIling/Scale
		// material.SetFloat("sheenRoughnessTextureRotation", insert_value_here); // Sheen Roughness Map Rotation
		// material.SetFloat("sheenRoughnessTextureTexCoord", insert_value_here); // Sheen Roughness Map UV
		// material.SetFloat("_VOLUME_ON", insert_value_here); // Enable Volume
		// material.SetFloat("_CLEARCOAT", insert_value_here); // Enable Clearcoat
		// material.SetFloat("_IRIDESCENCE", insert_value_here); // Enable Iridescence
		// material.SetFloat("_SPECULAR", insert_value_here); // Enable Specular
		// material.SetFloat("_SHEEN", insert_value_here); // Enable Sheen
		// material.SetFloat("_VOLUME_TRANSMISSION", insert_value_here); // EnableTransmissionDispersion
		// material.SetFloat("_TEXTURE_TRANSFORM", insert_value_here); // Enable Texture Transforms
		// material.SetFloat("_OverrideSurfaceMode", insert_value_here); // Override Surface Type and Queue
		// material.SetFloat("_NormalMapFormatXYZ", insert_value_here); // Override Normal Map is XYZ


		// Ensure keywords are correctly set after conversion.
		// Example:
		// if (material.GetFloat("_VERTEX_COLORS") > 0.5f) material.EnableKeyword("_VERTEX_COLORS_ON");

		ShaderGraphHelpers.ValidateMaterialKeywords(material);
		return true;
	}
}
