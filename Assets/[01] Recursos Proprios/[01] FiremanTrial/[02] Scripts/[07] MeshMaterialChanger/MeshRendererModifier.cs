using System.Collections.Generic;
using UnityEngine;

namespace FiremanTrial
{
    public static class MeshRendererModifier
    {
        private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
        private const string IgnoreTag = "IgnoreHighlight";
        public static List<MeshRenderer> GetAllMeshRenderers(GameObject root)
        {
            var meshRenderers = new List<MeshRenderer>();
            CollectMeshRenderers(root.transform, meshRenderers);
            return meshRenderers;
        }

        private static void CollectMeshRenderers(Transform current, List<MeshRenderer> meshRenderers)
        {
            if (!current.CompareTag(IgnoreTag))
            {
                var meshRenderer = current.GetComponent<MeshRenderer>();
                if (meshRenderer != null) meshRenderers.Add(meshRenderer);
            }

            foreach (Transform child in current) CollectMeshRenderers(child, meshRenderers);
        }

        public static List<Material> GetAllMaterialInstances(List<MeshRenderer> meshRenderers)
        {
            var uniqueMaterialInstances = new HashSet<Material>();
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer == null || meshRenderer.sharedMaterials == null) continue;
                var materialInstances = meshRenderer.materials;
                foreach (var materialInstance in materialInstances) 
                    if (materialInstance != null) uniqueMaterialInstances.Add(materialInstance);
            }

            return new List<Material>(uniqueMaterialInstances);
        }


        public static void ApplyEmissionHighlight(List<MeshRenderer> meshRenderers, Color emissionColor)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer == null) continue;
                var materials = meshRenderer.materials;
                foreach (var material in materials)
                {
                    if (material == null || !material.HasProperty(EmissionColor)) continue;
                    material.EnableKeyword("_EMISSION");
                    material.SetColor(EmissionColor, emissionColor);
                }
            }
        }


        public static void RemoveEmissionHighlight(List<MeshRenderer> meshRenderers)
        {
            if (meshRenderers == null || meshRenderers.Count == 0)
            {
                Debug.LogWarning("RemoveEmissionHighlight: meshRenderers is null or empty.");
                return;
            }
            
            foreach (var meshRenderer in meshRenderers)
            {
                if (meshRenderer == null) continue;
                var materials = meshRenderer.materials;
                foreach (var material in materials)
                {
                    if (material == null || !material.HasProperty(EmissionColor)) continue;
                    material.SetColor(EmissionColor, Color.black);
                    material.DisableKeyword("_EMISSION");
                }
            }
        }
    }
}