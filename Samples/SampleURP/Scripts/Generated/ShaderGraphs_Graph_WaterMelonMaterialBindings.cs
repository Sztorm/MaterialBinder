using System;
using UnityEngine;
using Sztorm.MaterialBinder;

public struct ShaderGraphs_Graph_WaterMelonMaterialBindings
{
    public const string MainColorPropertyName = "_MainColor";
    public const string AdditionalColorPropertyName = "_AdditionalColor";
    public const string DensityPropertyName = "_Density";
    public const string WidthPropertyName = "_Width";
    public const string Name = "Graph_WaterMelon";
    public const string FullName = "Shader Graphs/Graph_WaterMelon";
    public static readonly int MainColorPropertyId = Shader.PropertyToID("_MainColor");
    public static readonly int AdditionalColorPropertyId = Shader.PropertyToID("_AdditionalColor");
    public static readonly int DensityPropertyId = Shader.PropertyToID("_Density");
    public static readonly int WidthPropertyId = Shader.PropertyToID("_Width");
    
    private Material material;
    
    public Material Material => material;
    
    public bool IsBound => !(material is null);
    
    public VectorBinding MainColor
        => new VectorBinding(material, MainColorPropertyId);
    
    public VectorBinding AdditionalColor
        => new VectorBinding(material, AdditionalColorPropertyId);
    
    public ScalarBinding Density
        => new ScalarBinding(material, DensityPropertyId);
    
    public ScalarBinding Width
        => new ScalarBinding(material, WidthPropertyId);
    
    /// <summary>
    /// Binds material which contains Graph_WaterMelon.
    /// </summary>
    /// <param name="material">Material containing Graph_WaterMelon.</param>
    public void Bind(Material material)
    {
        if (material.shader.name != FullName)
        {
            throw new ArgumentException(
                "Material must contain shader that match binder.");
        }
        this.material = material;
    }
    
    /// <summary>
    /// Unbinds material. <see cref="ShaderGraphs_Graph_WaterMelonMaterialBindings"/> 
    /// shader properties will not usable until material is bound again.
    /// </summary>
    public void Unbind()
    {
        material = null;
    }
}
