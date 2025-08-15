using UnityEngine;

#if UNITY_STANDALONE_WIN
using System;
using System.Runtime.InteropServices;
#endif

public static class StickyKeysBlocker
{
#if UNITY_STANDALONE_WIN
    [StructLayout(LayoutKind.Sequential)]
    private struct STICKYKEYS
    {
        public uint cbSize;
        public uint dwFlags;
    }

    private const uint SPI_GETSTICKYKEYS = 0x003A;
    private const uint SPI_SETSTICKYKEYS = 0x003B;
    private const uint SKF_STICKYKEYSON  = 0x00000001;
    private const uint SKF_HOTKEYACTIVE  = 0x00000004;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref STICKYKEYS pvParam, uint fWinIni);

    private static STICKYKEYS originalStickyKeys;
    private static bool isBlocked = false;
#endif

    /// <summary>
    /// Sticky Keys 팝업 차단
    /// </summary>
    public static void Enable()
    {
#if UNITY_STANDALONE_WIN
        if (isBlocked) return;

        originalStickyKeys = new STICKYKEYS();
        originalStickyKeys.cbSize = (uint)Marshal.SizeOf(typeof(STICKYKEYS));
        SystemParametersInfo(SPI_GETSTICKYKEYS, originalStickyKeys.cbSize, ref originalStickyKeys, 0);

        // 팝업 방지 (핫키 비활성화)
        STICKYKEYS skOff = originalStickyKeys;
        skOff.dwFlags &= ~SKF_HOTKEYACTIVE;
        SystemParametersInfo(SPI_SETSTICKYKEYS, skOff.cbSize, ref skOff, 0);

        isBlocked = true;

        Application.quitting += Disable; // 게임 종료 시 복원
#endif
    }

    /// <summary>
    /// Sticky Keys 원래대로 복원
    /// </summary>
    public static void Disable()
    {
#if UNITY_STANDALONE_WIN
        if (!isBlocked) return;

        SystemParametersInfo(SPI_SETSTICKYKEYS, originalStickyKeys.cbSize, ref originalStickyKeys, 0);
        isBlocked = false;
#endif
    }
}