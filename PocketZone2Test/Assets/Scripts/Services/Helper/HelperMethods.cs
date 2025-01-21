using Services.Logger;
using UnityEngine;

namespace Services.Helper
{
    public static class HelperMethods
    {
        public static Sprite LoadIcon(string path)
        {
            Sprite icon = Resources.Load<Sprite>(path);
            if (icon == null)
            {
                LoggerService.LogError($"Не удалось загрузить иконку по пути: {path}");
            }
            return icon;
        }
    }
}
