using System.Collections.Generic;

public class WindowManager : Singleton<WindowManager>
{
    public List<BaseUiWindow> listWindows;

    public void OpenWindow(TypeWindow typeWindow)
    {
        foreach (BaseUiWindow baseUiWindow in listWindows)
        {
            if (baseUiWindow.typeWindow == typeWindow)
            {
                baseUiWindow.Open();
            }
            else
            {
                baseUiWindow.Close();
            }
        }
    }


    public void CloseWindow(TypeWindow typeWindow)
    {
        foreach (BaseUiWindow baseUiWindow in listWindows)
        {
            if (baseUiWindow.typeWindow == typeWindow)
            {
                baseUiWindow.Close();
            }
        }
    }

    public T GetWindow<T>(TypeWindow typeWindow) where T : BaseUiWindow
    {
        foreach (BaseUiWindow baseUiWindow in listWindows)
        {
            if (baseUiWindow.typeWindow == typeWindow)
            {
                return (T) baseUiWindow;
            }
        }

        return null;
    }
}