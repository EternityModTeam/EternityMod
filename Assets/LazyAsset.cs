using System;
using ReLogic.Content;
using Terraria.ModLoader;

namespace EternityMod.Assets;

public readonly struct LazyAsset<T>(Func<Asset<T>> assetLoadFunction) where T : class
{
    private readonly Lazy<Asset<T>> asset = new(assetLoadFunction);

    public Asset<T> Asset => asset.Value;

    public bool Uninitialized => asset is null;

    public T Value => asset.Value.Value;

    public static LazyAsset<T> FromPath(string path, AssetRequestMode requestMode = AssetRequestMode.AsyncLoad)
    {
        return new LazyAsset<T>(() => ModContent.Request<T>(path, requestMode));
    }

    public static implicit operator T(LazyAsset<T> asset) => asset.Value;
}
