using System;

namespace Qengu.GodotUtils.Extensions;

/// <summary>
/// Extension methods related to numbers
/// </summary>
public static class NumberExtensions
{
    public const int FLOATCOMPARISONDECIMALPLACES = 6;


    /// <summary>
    /// Checks whether the two given numbers are approximately equals
    /// </summary>
    /// <param name="left">the first <see langword="float"/></param>
    /// <param name="right">the second <see langword="float"/></param>
    /// <param name="decimalPlaces">The number of decimal places to compare</param>
    /// <returns>Whether or not the numbers are approximately equal</returns>
    public static bool ApproxEquals(this float left, float right, int decimalPlaces = FLOATCOMPARISONDECIMALPLACES)
    {
        if (float.IsNaN(left) || float.IsNaN(right)) return false;
        if (left == right) return true;
        return float.Abs(left - right) < 0.5f * MathF.Pow(10f, -decimalPlaces);
    }

    /// <summary>
    /// Checks whether the given number is approximately zero
    /// </summary>
    /// <param name="left">The <see langword="float"/>to compare</param>
    /// <param name="decimalPlaces">The number of decimal places to check</param>
    /// <returns>Whether or not the <see langword="float"/>is approximately zero</returns>
    public static bool ApproxZero(this float left, int decimalPlaces = FLOATCOMPARISONDECIMALPLACES) => ApproxEquals(left, 0f, decimalPlaces);
}
