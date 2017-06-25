using System;

namespace Beast.Objects.Mobiles
{
    [Flags]
    public enum MobileAffects
    {
        /// <summary>
        /// No affect.
        /// </summary>
        None = 0,
        /// <summary>
        /// Speicifes that the mobile cannot see.
        /// </summary>
        Blind = 1,
        /// <summary>
        /// Specifies the mobile is invisible. Detect invisible is required to see them.
        /// </summary>
        Invisible = 2,
        /// <summary>
        /// For an npc, specifies the ability to curse a player, for a player, specifies they are cursed.
        /// </summary>
        Curse = 4,
        /// <summary>
        /// For an npc, specifies the ability to poison a player, for a player, specifies they are poisoned.
        /// </summary>
        Poison = 8,
        /// <summary>
        /// Specifies that a mobile is hidden.
        /// </summary>
        Hidden = 16,
        /// <summary>
        /// Specifies that a mobile is sneaking.
        /// </summary>
        Sneak = 64,
        /// <summary>
        /// Specifies that a mobile can detect invisible objects.
        /// </summary>
        DetectInvisible = 128,
        /// <summary>
        /// Specifies that a mobile can detect hidden objects.
        /// </summary>
        DetectHidden = 256,
    }
}
