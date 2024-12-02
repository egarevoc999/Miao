using System;
using System.Threading;
using UnityEngine;

public class JsonParser
{
    private TextAsset jsonFile;
    private SceneInfo info;
    private ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

    public JsonParser(String file)
    {
        // load json file from resources directory
        this.jsonFile = Resources.Load<TextAsset>(file);
        if (this.jsonFile != null)
        {
            Debug.Log("Parse json file " + file + " success");
        } else
        {
            Debug.LogError("Json file " + file + " not found"); ;
        }
    }

    public void Parse()
    {
        Debug.Log("Start parse");

        rwLock.EnterWriteLock();
        try
        {
            this.info = JsonUtility.FromJson<SceneInfo>(this.jsonFile.text);
        }
        finally
        {
            rwLock.ExitWriteLock();
        }
    }

    public SceneInfo GetInfo()
    {
        rwLock.EnterReadLock();
        try
        {
            return this.info;
        }
        finally
        {
            rwLock.ExitReadLock();
        }
    }
}
