using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using EternityMod.Content.Rarities;

namespace EternityMod.Content.Items.Weapons.Melee;

public class EternitySword : ModItem
{
    public override string Texture => "EternityMod/Assets/Textures/Content/Items/Weapons/Melee/EternitySword";

    public override void SetDefaults()
    {
        Item.width = Item.height = 200;
        Item.damage = 25000;
        Item.DamageType = DamageClass.MeleeNoSpeed;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 20f;
        Item.useTime = Item.useAnimation = 20;
        Item.rare = ModContent.RarityType<EternityRarity>();
        //Item.shoot = ModContent.ProjectileType<EternitySwordSwing>();
        Item.shootSpeed = 10f;
        Item.autoReuse = true;
        Item.useTurn = true;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        Item.channel = true;
        Item.value = Item.sellPrice(100);
        Item.UseSound = SoundID.Item1;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float state = 0;
        if (player.altFunctionUse == 2)
            state = 1;

        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, state, 0);

        return false;
    }

    //public override bool CanShoot(Player player) =>
    //    !Main.projectile.Any(p => p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<EternitySwordSwing>());

    public override bool AltFunctionUse(Player player) => true;

    public override bool? CanHitNPC(Player player, NPC target) => false;

    public override bool CanHitPvp(Player player, Player target) => false;

    public override void ModifyWeaponCrit(Player player, ref float crit) => crit = 100;

    // TO-DO: Implement the various items planned that are being used for this recipe.
    // For now, it will just use a Dirt Block as an ingredient (lol).
    public override void AddRecipes()
    {
        CreateRecipe().
            AddIngredient(ItemID.DirtBlock).
            Register();
    }
}
