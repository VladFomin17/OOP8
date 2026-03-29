namespace Laba1.Src.util;

using System.Runtime.InteropServices;

/// <summary>
/// Класс-обёртка для вызова нативного WinAPI метода MessageBox из user32.dll.
/// Используется для отображения простых диалоговых окон Windows.
/// </summary>
internal static class NativeMessageBox
{
    /// <summary>
    /// Импорт функции MessageBox из библиотеки user32.dll.
    /// </summary>
    /// <param name="hWnd">Дескриптор родительского окна (IntPtr.Zero, если отсутствует).</param>
    /// <param name="lpText">Текст сообщения.</param>
    /// <param name="lpCaption">Заголовок окна.</param>
    /// <param name="uType">Тип окна (кнопки, иконка и т.д.).</param>
    /// <returns>Код нажатой кнопки.</returns>
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(
        IntPtr hWnd,
        string lpText,
        string lpCaption,
        uint uType
    );

    /// <summary>
    /// Отображает кнопку "ОК".
    /// </summary>
    public const uint MB_OK = 0x00000000;

    /// <summary>
    /// Отображает иконку ошибки.
    /// </summary>
    public const uint MB_ICONERROR = 0x00000010;
}
