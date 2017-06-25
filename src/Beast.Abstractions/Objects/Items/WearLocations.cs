using System;

namespace Beast.Objects.Items
{
    [Flags]
    public enum WearLocations
    {
        None = 0,
        Head = 1,
        Eyes = 2,
        Neck = 4,
        Chest = 8,
        Back = 16,
        Shoulders = 32,
        Arms = 64,
        Elbows = 128,
        Wrists = 256,
        Hands = 512,
        Fingers = 1024,
        Waist = 2048,
        Legs = 4096,
        Knees = 8192,
        Ankles = 16384,
        Feet = 32768,
        Toes = 65536
    }
}
