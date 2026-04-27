using UnityEngine;

public class SaveService
{
    private SaveData saveData;

    public SaveService(SaveData saveData)
    {
        this.saveData = saveData;
    }

    public void save()
    {
        SaveSystem.Save(saveData);
    }
}
