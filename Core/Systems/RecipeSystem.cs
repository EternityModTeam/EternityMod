using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EternityMod.Core.Systems;

public class RecipeSystem : ModSystem
{
    #region Recipe Groups

    public static int AnyEvilEnemyMaterial;

    public override void AddRecipeGroups()
    {
        AddEvilBiomeItemRecipeGroups();
        // TO-DO: Add more methods for this.
    }

    private static void AddEvilBiomeItemRecipeGroups()
    {
        // Rotten Chunks or Vertebrae.
        RecipeGroup group = new(() => GetTextValue("RecipeGroups.AnyEvilEnemyMaterial"),
        [
            ItemID.RottenChunk,
            ItemID.Vertebrae
        ]);
        AnyEvilEnemyMaterial = RecipeGroup.RegisterGroup("AnyEvilEnemyMaterial", group);
    }

    #endregion Recipe Groups

    #region Recipes

    public override void AddRecipes()
    {
        AddVanillaRecipes();
        EditVanillaRecipes();
    }

    internal static void AddVanillaRecipes()
    {
        // Black Lens.
        Recipe.Create(ItemID.BlackLens).
            AddIngredient(ItemID.Lens).
            AddIngredient(ItemID.BlackDye).
            AddTile(TileID.DyeVat).
            Register();

        // Leather.
        Recipe.Create(ItemID.Leather).
            AddIngredient(AnyEvilEnemyMaterial, 2).
            AddTile(TileID.WorkBenches).
            Register();

        // Life Crystal.
        Recipe.Create(ItemID.LifeCrystal).
            AddIngredient(ItemID.StoneBlock, 5).
            AddIngredient(ItemID.Ruby, 2).
            AddIngredient(ItemID.HealingPotion).
            AddTile(TileID.Anvils).
            Register();
    }

    internal static void EditVanillaRecipes()
    {

    }

    #endregion Recipes
}
