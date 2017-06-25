using System;

namespace Beast.Objects.Mobiles
{
    /// <summary>
    /// Represents actions the mobile can take.
    /// </summary>
    [Flags]
    public enum MobileActions
    {
        /// <summary>
        /// No action set.
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies that the mobile should wander around.
        /// </summary>
        Wander = 1,
        /// <summary>
        /// Specifies an aggressive mobile that will attack any player weaker than them.
        /// </summary>
        Aggressive = 2,
        /// <summary>
        /// Specifies a mobile that will run from a player.
        /// </summary>
        Scared = 4,
        /// <summary>
        /// Specifies that the mobile is a pet for a player.
        /// </summary>
        Pet = 8,
        /// <summary>
        /// Specifies that the mobile can heal a player.
        /// </summary>
        Heal = 16,
        /// <summary>
        /// Specifies that the mobile can repair damaged items.
        /// </summary>
        Repair = 32,
        /// <summary>
        /// Specifies that the mobile can enhance an item.
        /// </summary>
        Enhance = 64,
        /// <summary>
        /// Specifies the mobile is a shopkeeper.
        /// </summary>
        Shopkeeper = 128,

    }
}
