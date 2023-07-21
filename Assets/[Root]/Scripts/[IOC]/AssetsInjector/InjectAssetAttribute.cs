using System;

[AttributeUsage(AttributeTargets.Field)]
public class InjectAssetAttribute : Attribute
{
    private string _nameAsset;

    public InjectAssetAttribute(string nameAsset) => _nameAsset = nameAsset;

    public string NameAsset { get => _nameAsset; }
}
